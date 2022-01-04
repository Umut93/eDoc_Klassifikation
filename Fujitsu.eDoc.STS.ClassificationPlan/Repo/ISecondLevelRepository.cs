using Fujitsu.eDoc.STS.ClassificationPlan.Model;
using static Fujitsu.eDoc.STS.ClassificationPlan.KlassifikationSystemService;

namespace Fujitsu.eDoc.STS.ClassificationPlan.Repo
{
    public interface ISecondLevelRepository
    {
        void AddGroupForSecondLevel(ParagraphTitles secondGroup, NoarkSubArchive noarkSub);
        void UpdateGroup(ParagraphTitles secondGroup, EdocEmnePlan eDocKLESecond);
    }
}
