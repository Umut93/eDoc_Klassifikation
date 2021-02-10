using Fujitsu.eDoc.STS.ClassificationPlan.SF1510_KlassifikationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fujitsu.eDoc.STS.ClassificationPlan
{
    interface IKlassifikationService
    {
        FiltreretOejebliksbilledeType ReadClassification(string CVR, string klasseServiceEndPoint, string certificateSerialNumber, string brugerVendtNoegle);
        Task<string[]> SearchAllClassificationsAsync(string CVR, string klasseServiceEndPoint, string certificateSerialNumber);
        Task<listResponse> ListClassifications(string CVR, string caseServiceEndPoint, string certificateSerialNumber, string[] uuid);
    }
}
