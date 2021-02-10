using Fujitsu.eDoc.STS.ClassificationPlan.Model;
using Fujitsu.eDoc.STS.ClassificationPlan.Repo;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Fujitsu.eDoc.STS.ClassificationPlan.KlassifikationSystemService;

namespace Fujitsu.eDoc.STS.ClassificationPlan.BLL
{
    public class SecondLevelManagerBLL
    {

        public void AddGroupForSecondLevel(List<ParagraphTitles> mainChapteres, List<EdocEmnePlan> eDocEmnePlaner, List<NoarkSubArchive> noarkSubs, List<NoarkClassificationType> noarkClassificationTypes)
        {
            ISecondLevelRepository secondLevelRepository = new SecondLevelRepository();
            foreach (var mainGroup in mainChapteres)
            {
                foreach (var secondGroup in mainGroup.Chapters)
                {
                    EdocEmnePlan eDocKLESecond = eDocEmnePlaner.Find(x => x.Code == secondGroup.SParagraphs);

                    if (eDocKLESecond == null)
                    {
                        NoarkSubArchive noarkSub = noarkSubs.Find(x => x.Code.Substring(0, 2) == mainGroup.SParagraphs);

                        if (noarkSub != null)
                        {
                            secondLevelRepository.AddGroupForSecondLevel(secondGroup, noarkSub);
                        }
                    }

                    else
                    {
                        secondLevelRepository.UpdateGroup(secondGroup, eDocKLESecond);
                    }
                }

            }

        }

    }

}
