using Fujitsu.eDoc.STS.ClassificationPlan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Fujitsu.eDoc.STS.ClassificationPlan.KlassifikationSystemService;

namespace Fujitsu.eDoc.STS.ClassificationPlan.Repo
{
    public interface ISecondLevelRepository
    {
        void AddGroupForSecondLevel(ParagraphTitles secondGroup, NoarkSubArchive noarkSub);
        void UpdateGroup(ParagraphTitles secondGroup, EdocEmnePlan eDocKLESecond);
    }
}
