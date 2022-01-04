using Fujitsu.eDoc.STS.ClassificationPlan.Model;
using Fujitsu.eDoc.STS.ClassificationPlan.Repo;
using System.Collections.Generic;
using static Fujitsu.eDoc.STS.ClassificationPlan.KlassifikationSystemService;

namespace Fujitsu.eDoc.STS.ClassificationPlan.BLL
{
    public class ThirdLevelManagerBLL
    {
        /// <summary>
        /// Creating sub groups (third level)
        /// </summary>
        /// <param name="mainChapteres"></param>
        public void AddGroupForThirdLevel(List<ParagraphTitles> mainChapteres)
        {
            IThirdLevelRepository thirdLevelRepository = new ThirdLevelRepository();

            //System.Diagnostics.Debugger.Launch();

            List<EdocEmnePlan> eDocEmnePlaner = EdocEmnePlan.GetEdocKLes();

            foreach (var mainGroup in mainChapteres)
            {
                foreach (var secondGroup in mainGroup.Chapters)
                {
                    foreach (var thirdGroup in secondGroup.Chapters)
                    {
                        EdocEmnePlan eDocKLESecond = eDocEmnePlaner.Find(x => x.Code == thirdGroup.SParagraphs);

                        if (eDocKLESecond == null)
                        {
                            EdocEmnePlan eDocSecondLevelEmnePlan = eDocEmnePlaner.Find(x => x.Code == thirdGroup.SParagraphs.Substring(0, thirdGroup.SParagraphs.Length - 3));

                            if (eDocSecondLevelEmnePlan != null)
                            {
                                //bool isSecondarytypeExisting = noarkClassificationTypes.Exists(x => x.Recno == eDocSecondLevelEmnePlan.ToSecondaryClassType);

                                //if (isSecondarytypeExisting)
                                //{

                                thirdLevelRepository.AddSubGroup(thirdGroup, eDocSecondLevelEmnePlan);

                                //}

                            }

                        }

                        else
                        {
                            thirdLevelRepository.UpdateSubGroup(thirdGroup, eDocKLESecond);
                        }

                    }
                }

            }
        }

    }
}

