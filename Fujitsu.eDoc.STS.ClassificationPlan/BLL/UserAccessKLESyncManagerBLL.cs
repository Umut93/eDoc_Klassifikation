using Fujitsu.eDoc.STS.ClassificationPlan.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fujitsu.eDoc.STS.ClassificationPlan.BLL
{
    public class UserAccessKLESyncManagerBLL
    {
        public void SyncUserAccessWithKLE()
        {
            UseraccessKLESyncRepository useraccessKLESyncRepository = new UseraccessKLESyncRepository();
            useraccessKLESyncRepository.SyncUserAccessWithKLE();
        }
    }
}
