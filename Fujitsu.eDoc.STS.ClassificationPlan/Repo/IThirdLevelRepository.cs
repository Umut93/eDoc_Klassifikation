using Fujitsu.eDoc.STS.ClassificationPlan.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Fujitsu.eDoc.STS.ClassificationPlan.KlassifikationSystemService;

namespace Fujitsu.eDoc.STS.ClassificationPlan.Repo
{
    public interface IThirdLevelRepository
    {
        void AddSubGroup(ParagraphTitles thirdGroup, EdocEmnePlan eDocSecondLevelEmnePlan);
        void UpdateSubGroup(ParagraphTitles thirdGroup, EdocEmnePlan eDocSecondLevelEmnePlan);
    }
}
