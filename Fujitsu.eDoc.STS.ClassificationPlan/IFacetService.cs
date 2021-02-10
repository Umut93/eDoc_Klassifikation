using Fujitsu.eDoc.STS.ClassificationPlan.SF1510_FacetService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fujitsu.eDoc.STS.ClassificationPlan
{
    interface IFacetService
    {
        FiltreretOejebliksbilledeType ReadFacet(string CVR, string facetServiceEndPoint, string certificateSerialNumber, string facetUUID);
        Task<string[]> SearhALLFacetsAsync(string CVR, string klasseServiceEndPoint, string certificateSerialNumber);
        Task<listResponse> ListFacets(string CVR, string facetServiceEndPoint, string certificateSerialNumber, string[] UUIDs);
    }
}
