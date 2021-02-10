using Fujitsu.eDoc.STS.ClassificationPlan.Model;
using Fujitsu.eDoc.STS.ClassificationPlan.Repo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Fujitsu.eDoc.STS.ClassificationPlan.KlassifikationSystemService;

namespace Fujitsu.eDoc.STS.ClassificationPlan.BLL
{
    public class MainGroupManagerBLL
    {
        /// <summary>
        /// /Create a new main group that does not exists in eDoc
        /// </summary>
        /// <param name="mainChapters"></param>
        /// <param name="eDocEmnePlaner"></param>
        /// <param name="noarkSubs"></param>
        /// <param name="noarkClassificationTypes"></param>
        public void AddMainGroup(List<ParagraphTitles> mainChapters, List<EdocEmnePlan> eDocEmnePlaner, List<NoarkSubArchive> noarkSubs)
        {
            IMainGroupRepository mainGroupRepository = new MainGroupRepository();

            foreach (var mainGroup in mainChapters)
            {
                string codeAndDescription = mainGroup.SParagraphs + " " + mainGroup.Text;
                NoarkSubArchive noarkSubArch = noarkSubs.Find(x => x.Description.TrimEnd() == codeAndDescription);

                if (noarkSubArch == null)
                {
                    mainGroupRepository.AddMainGroup(mainGroup);
                }

                else
                {
                    mainGroupRepository.UpdateMainGroup(mainGroup, noarkSubArch, codeAndDescription);
                }
            }
        }
    }
}
