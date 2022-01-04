using Fujitsu.eDoc.STS.ClassificationPlan.Model;
using static Fujitsu.eDoc.STS.ClassificationPlan.KlassifikationSystemService;

namespace Fujitsu.eDoc.STS.ClassificationPlan.Repo
{
    public interface IThirdLevelRepository
    {
        void AddSubGroup(ParagraphTitles thirdGroup, EdocEmnePlan eDocSecondLevelEmnePlan);
        void UpdateSubGroup(ParagraphTitles thirdGroup, EdocEmnePlan eDocSecondLevelEmnePlan);
    }
}
