using Fujitsu.eDoc.Organisation.Integration.Models;
using Fujitsu.eDoc.STS.ClassificationPlan.Model;
using System.Collections.Generic;
using System.IdentityModel.Tokens;

namespace Fujitsu.eDoc.STS.ClassificationPlan
{
    public interface IKlassifikationSystemService
    {
        List<STSKLE> Fremsoegobjekthierarki(FKContext fkContext, string brugerVendtNoegle, SecurityToken token);
    }
}
