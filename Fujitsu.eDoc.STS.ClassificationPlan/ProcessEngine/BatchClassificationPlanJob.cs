using Fujitsu.eDoc.Organisation.FKIntegration;
using Fujitsu.eDoc.Organisation.FKIntegration.Invocation;
using Fujitsu.eDoc.Organisation.Integration.Models;
using Fujitsu.eDoc.STS.ClassificationPlan.BLL;
using Fujitsu.eDoc.STS.ClassificationPlan.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Fujitsu.eDoc.STS.ClassificationPlan.KlassifikationSystemService;

namespace Fujitsu.eDoc.STS.ClassificationPlan.ProcessEngine
{
    public class BatchClassificationPlanJob
    {
        IKlassifikationSystemService klassifikationSystemService;
        private MainGroupManagerBLL mainGroupMangerBLL = new MainGroupManagerBLL();
        private SecondLevelManagerBLL secondLevelManagerBLL = new SecondLevelManagerBLL();
        private ThirdLevelManagerBLL thirdLevelManagerBLL = new ThirdLevelManagerBLL();
        private FacetManagerBLL facetManagerBLL = new FacetManagerBLL();
        private UserAccessKLESyncManagerBLL userAccessKLESyncManagerBLL = new UserAccessKLESyncManagerBLL();
        private SecurityToken token;
        private FKContext fkContext;

        public async Task BatchSyncClassificationPlan()
        {
            string progress = "BatchSyncClassificationPlan - start";
            try
            {
                fkContext = FKContextFetcher.GetSettings();

                //Get token first
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;

                token = TokenFetcher.IssueToken(fkContext);

                //Resolves TLS issues when calling WCF webservice
                klassifikationSystemService = new KlassifikationSystemService();

                progress = "Initializing";
                Task<List<EdocEmnePlan>> eDocEmnePlaner = Task.Run(() => EdocEmnePlan.GetEdocKLes());
                Task<List<EdocEmnePlan>> eDocFacetPlaner = Task.Run(() => EdocEmnePlan.GetEdocFacets());
                Task<List<NoarkSubArchive>> noarkSubArchives = Task.Run(() => NoarkSubArchive.GetNoarkSubArchives());
                Task<List<NoarkClassificationType>> noarkClassTypes = Task.Run(() => NoarkClassificationType.GetGetNoarkClassificationTypes());

                List<STSKLE> stsKLEGUIDs = klassifikationSystemService.Fremsoegobjekthierarki(fkContext, "KLE", token);

                //Waiting for completing all tasks
                progress = "Waiting for completing all tasks";
                await Task.WhenAll(eDocEmnePlaner, noarkSubArchives, noarkClassTypes);

                //Results
                progress = "Getting Results";
                List<EdocEmnePlan> eDocKlES = await eDocEmnePlaner;
                List<EdocEmnePlan> eDocFacets = await eDocFacetPlaner;
                List<NoarkSubArchive> noarkSubs = await noarkSubArchives;
                List<NoarkClassificationType> noarkClassificationTypes = await noarkClassTypes;


                // Treestructure of STS KLE emneplan
                // Processing emneFacectter
                progress = "Processing emneFacectter";
                Regex regex = new Regex("^\\d+(\\.?\\d+\\.?\\d+)?$");
                List<ParagraphTitles> stsTitles = stsKLEGUIDs.Where(y => regex.IsMatch(y.Code)).Select(x => new ParagraphTitles(x.Code, x.TitleText, x.UUID, x.IsExpired)).ToList();
                List<ParagraphTitles> stsSortedTitles = stsTitles.OrderBy(x => x).ToList();
                ParagraphTitles stsRoot = new ParagraphTitles();
                stsRoot.CreateTree(stsSortedTitles, 0);
                ////END

                //Creates a new MainGroup
                progress = "Handling CreateMainGroup";
                mainGroupMangerBLL.AddMainGroup(stsRoot.Chapters, eDocKlES, noarkSubs);

                //SecondLevel
                progress = "Handling CreateGroupForSecondLevel";
                secondLevelManagerBLL.AddGroupForSecondLevel(stsRoot.Chapters, eDocKlES, noarkSubs, noarkClassificationTypes);

                //System.Diagnostics.Debugger.Launch();
                //Third level
                progress = "Handling CreateSubGroupForThirdLevel";
                thirdLevelManagerBLL.AddGroupForThirdLevel(stsRoot.Chapters);

                //HandlingsFacetter
                progress = "Handling HandlingsFacetter";
                Regex facetRegex = new Regex("^[A-ZÆØÅ][0-9]{0,2}$");
                List<STSKLE> facets = stsKLEGUIDs.Where(y => facetRegex.IsMatch(y.Code)).ToList();
                facetManagerBLL.CreateFacet(facets, eDocFacets);

                //Sync STS useraccess
                progress = "Handling Sync STS useraccess";
                userAccessKLESyncManagerBLL.SyncUserAccessWithKLE();
            }

            catch (Exception ex)
            {
                EventLog.LogToEventLog(progress + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace, System.Diagnostics.EventLogEntryType.Error);
            }

        }
    }
}


