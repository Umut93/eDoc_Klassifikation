using Fujitsu.eDoc.STS.ClassificationPlan.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fujitsu.eDoc.STS.ClassificationPlan
{
    public interface IKlassifikationSystemService
    {
        Task<List<STSKLE>> FremsoegobjekthierarkiAsync(string CVR, string klasseServiceEndPoint, string certificateSerialNumber, string brugerVendtNoegle);
    }
}
