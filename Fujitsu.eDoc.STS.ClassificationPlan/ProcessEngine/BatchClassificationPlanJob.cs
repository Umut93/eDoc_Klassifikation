using Fujitsu.eDoc.STS.ClassificationPlan;
using Fujitsu.eDoc.STS.ClassificationPlan.BLL;
using Fujitsu.eDoc.STS.ClassificationPlan.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using static Fujitsu.eDoc.STS.ClassificationPlan.KlassifikationSystemService;

namespace Fujitsu.eDoc.STS.ClassificationPlan.ProcessEngine
{
    public class BatchClassificationPlanJob
    {
        IKlassifikationSystemService klassifikationSystemService;
        MainGroupManagerBLL mainGroupMangerBLL = new MainGroupManagerBLL();
        SecondLevelManagerBLL secondLevelManagerBLL = new SecondLevelManagerBLL();
        ThirdLevelManagerBLL thirdLevelManagerBLL = new ThirdLevelManagerBLL();
        FacetManagerBLL facetManagerBLL = new FacetManagerBLL();

        public async Task BatchSyncClassificationPlan()
        {
            string progress = "BatchSyncClassificationPlan - start";
            try
            {
                //Resolves TLS issues when calling WCF webservice
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                klassifikationSystemService = new KlassifikationSystemService();

                progress = "Initializing";
                Task<List<EdocEmnePlan>> eDocEmnePlaner = Task.Run(() => EdocEmnePlan.GetEdocKLes());
                Task<List<EdocEmnePlan>> eDocFacetPlaner = Task.Run(() => EdocEmnePlan.GetEdocFacets());
                Task<List<NoarkSubArchive>> noarkSubArchives = Task.Run(() => NoarkSubArchive.GetNoarkSubArchives());
                Task<List<NoarkClassificationType>> noarkClassTypes = Task.Run(() => NoarkClassificationType.GetGetNoarkClassificationTypes());

                //Get settings
                Dictionary<string, string> codeTableConfigRecords = GetSAPASettings();
                string endpoint = codeTableConfigRecords["ClassificationEndpoint"];
                string certificate = codeTableConfigRecords["ClassificationCertificateSerialNumber"];
                string cvr = Fujitsu.eDoc.Core.eDocSettingInformation.GetSettingValueFromeDoc("fujitsu", "municipalitycvr");
                Task<List<STSKLE>> stsKLEGUIDs = klassifikationSystemService.FremsoegobjekthierarkiAsync(cvr, endpoint, certificate, "KLE");

                //Waiting for completing all tasks
                progress = "Waiting for completing all tasks";
                await Task.WhenAll(eDocEmnePlaner, stsKLEGUIDs, noarkSubArchives, noarkClassTypes);

                //Results
                progress = "Getting Results";
                List<STSKLE> stsKLES = await stsKLEGUIDs;
                List<EdocEmnePlan> eDocKlES = await eDocEmnePlaner;
                List<EdocEmnePlan> eDocFacets = await eDocFacetPlaner;
                List<NoarkSubArchive> noarkSubs = await noarkSubArchives;
                List<NoarkClassificationType> noarkClassificationTypes = await noarkClassTypes;


                // Treestructure of STS KLE emneplan
                // Processing emneFacectter
                progress = "Processing emneFacectter";
                Regex regex = new Regex("^\\d+(\\.?\\d+\\.?\\d+)?$");
                List<ParagraphTitles> stsTitles = stsKLES.Where(y => regex.IsMatch(y.Code)).Select(x => new ParagraphTitles(x.Code, x.TitleText, x.UUID, x.IsExpired)).ToList();
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
                List<STSKLE> facets = stsKLES.Where(y => facetRegex.IsMatch(y.Code)).ToList();
                facetManagerBLL.CreateFacet(facets, eDocFacets);

            }

            catch (Exception ex)
            {
                EventLog.LogToEventLog(progress + Environment.NewLine + ex.Message + Environment.NewLine + ex.StackTrace, System.Diagnostics.EventLogEntryType.Error);
            }

        }


        public static Dictionary<string, string> GetSAPASettings()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            try
            {
                string xmlQuery = Core.Common.GetResourceXml("GetCodeTableValues.xml", typeof(EdocEmnePlan).Namespace.Replace("Model", "XML"), Assembly.GetExecutingAssembly());

                var res = Fujitsu.eDoc.Core.Common.ExecuteQuery(xmlQuery);

                XDocument xDoc = XDocument.Parse(res);

                if (xDoc != null)
                {

                    var RECORD = xDoc.Descendants("RECORD").ToList(); ;


                    foreach (var r in RECORD)
                    {
                        dic.Add(r.Element("Key").Value, r.Element("Value").Value);
                    };

                    return dic;
                }
                return dic;
            }
            catch (Exception ex)
            {
                EventLog.LogToEventLog($"72ab5a9a-76e6-468a-8083-857902e1919f - {System.Environment.NewLine} {ex.ToString()}", System.Diagnostics.EventLogEntryType.Error);
            }
            return dic;
        }
    }
}


