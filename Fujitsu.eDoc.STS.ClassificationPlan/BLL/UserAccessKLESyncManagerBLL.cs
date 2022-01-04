using Fujitsu.eDoc.STS.ClassificationPlan.Repo;

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
