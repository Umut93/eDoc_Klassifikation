using Fujitsu.eDoc.STS.ClassificationPlan.SF1510_KlasseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fujitsu.eDoc.STS.ClassificationPlan
{
    interface IKlasseService
    {
        Task<string[]> SearchAllClassessAsync(string CVR, string klasseServiceEndPoint, string certificateSerialNumber, string brugerVendtNoegle);
        FiltreretOejebliksbilledeType ReadKLEClass(string CVR, string klasseServiceEndPoint, string certificateSerialNumber, string UUIDIdentifikator);
        Task<listResponse> ListClassKLEs(string CVR, string caseServiceEndPoint, string certificateSerialNumber, string[] uuid);

    }
}
