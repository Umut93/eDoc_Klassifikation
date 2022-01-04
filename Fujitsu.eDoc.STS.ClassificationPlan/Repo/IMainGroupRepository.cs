using Fujitsu.eDoc.STS.ClassificationPlan.Model;
using static Fujitsu.eDoc.STS.ClassificationPlan.KlassifikationSystemService;

namespace Fujitsu.eDoc.STS.ClassificationPlan.Repo
{
    public interface IMainGroupRepository
    {
        void AddMainGroup(ParagraphTitles mainGroup);
        void UpdateMainGroup(KlassifikationSystemService.ParagraphTitles mainGroup, NoarkSubArchive noarkSubArch, string codeAndDescription);
    }
}
