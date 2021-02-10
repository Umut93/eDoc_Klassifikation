using System;
using System.IO;
using System.ServiceModel;
using System.Threading.Tasks;
using Fujitsu.eDoc.STS.ClassificationPlan.SF1510_FacetService;

namespace Fujitsu.eDoc.STS.ClassificationPlan
{
    class FacetService : IFacetService
    {
        /// <summary>
        /// Finds and returns one facet (latest registration)
        /// </summary>
        /// <param name="CVR"></param>
        /// <param name="facetServiceEndPoint"></param>
        /// <param name="certificateSerialNumber"></param>
        /// <param name="facetUUID"></param>
        /// <returns></returns>
        public FiltreretOejebliksbilledeType ReadFacet(string CVR, string facetServiceEndPoint, string certificateSerialNumber, string facetUUID)
        {
            FiltreretOejebliksbilledeType obj = new FiltreretOejebliksbilledeType();
            using (FacetPortTypeClient ft = GetFacetPortTypeClient(facetServiceEndPoint, certificateSerialNumber))
            {
                RequestHeaderType requestHeaderType = GetRequestHeaderType();

                LaesRequestType laesRequestType = new LaesRequestType()
                {
                    AuthorityContext = new AuthorityContextType { MunicipalityCVR = CVR },
                    LaesInput = new LaesInputType
                    {
                        UUIDIdentifikator = facetUUID
                    }
                };
                LaesResponseType respons = ft.laes(ref requestHeaderType, laesRequestType);
                if (respons.LaesOutput.StandardRetur.StatusKode == "20")
                {
                    obj = respons.LaesOutput.FiltreretOejebliksbillede;
                }
            }
            return obj;
        }

        ///// <summary>
        ///// No criteria. Returns all!
        ///// </summary>
        ///// <param name="CVR"></param>
        ///// <param name="facetServiceEndPoint"></param>
        ///// <param name="certificateSerialNumber"></param>
        ///// <param name="brugerVendtNoegle"></param>
        ///// <returns></returns>
        public async Task<string[]> SearhALLFacetsAsync(string CVR, string facetServiceEndPoint, string certificateSerialNumber)
        {
            string[] guid = new string[] { };
            using (FacetPortTypeClient ft = GetFacetPortTypeClient(facetServiceEndPoint, certificateSerialNumber))
            {
                RequestHeaderType requestHeaderType = GetRequestHeaderType();

                SoegRequestType requestType = new SoegRequestType()
                {
                    AuthorityContext = new AuthorityContextType { MunicipalityCVR = CVR },
                    SoegInput = new SoegInputType1()
                    {
                        AttributListe = new AttributListeType
                        {
                            Egenskab = new EgenskabType[] { new EgenskabType { BrugervendtNoegleTekst = "" } }
                            //Egenskab = new EgenskabType[] { new EgenskabType { BrugervendtNoegleTekst = "00" }, new EgenskabType { BrugervendtNoegleTekst = "27.35.04" } }
                        },
                        TilstandListe = new TilstandListeType(),
                        RelationListe = new RelationListeType()
                    }
                };
                soegResponse respons = await ft.soegAsync(requestHeaderType, requestType);
                if (respons.SoegResponse1.SoegOutput.StandardRetur.StatusKode == "20")
                {
                    guid = respons.SoegResponse1.SoegOutput.IdListe;
                    listResponse result = await ListFacets(CVR, facetServiceEndPoint, certificateSerialNumber, guid);

                    //using (StreamWriter outputFile = new StreamWriter("C:\\Users\\denumk\\Desktop\\FacetService.txt", true))
                    //{
                    //    FiltreretOejebliksbilledeType[] arr = result.ListResponse1.ListOutput.FiltreretOejebliksbillede;

                    //    for (int i = 0; i < arr.Length; i++)
                    //    {
                    //        var value = arr[i].Registrering[0].AttributListe.Egenskab[0].BrugervendtNoegleTekst;
                    //        var value2 = arr[i].ObjektType.UUIDIdentifikator;

                    //        outputFile.WriteLine(value + " " + value2 + Environment.NewLine);
                    //    }
                    //}
                }
            }
            return guid;
        }

        /// <summary>
        /// Retrieve facets by UUIDS
        /// </summary>
        /// <param name="CVR"></param>
        /// <param name="caseServiceEndPoint"></param>
        /// <param name="certificateSerialNumber"></param>
        /// <param name="uuid"></param>
        /// <returns></returns>
        public async Task<listResponse> ListFacets(string CVR, string facetServiceEndPoint, string certificateSerialNumber, string[] UUIDs)
        {
            listResponse listResponseType = new listResponse();

            using (FacetPortTypeClient client = GetFacetPortTypeClient(facetServiceEndPoint, certificateSerialNumber))
            {
                ListRequestType listRequestType = new ListRequestType()
                {
                    AuthorityContext = new AuthorityContextType()
                    {
                        MunicipalityCVR = CVR
                    },
                    ListInput = new ListInputType()
                    {
                        UUIDIdentifikator = UUIDs
                    }
                };
                RequestHeaderType requestHeaderType = GetRequestHeaderType();
                listResponseType = await client.listAsync(requestHeaderType, listRequestType);

            }
            return listResponseType;
        }


        private FacetPortTypeClient GetFacetPortTypeClient(string caseServiceEndPoint, string certificateSerialNumber)
        {
            System.Security.Cryptography.X509Certificates.X509FindType CertificateFindType =
                                        System.Security.Cryptography.X509Certificates.X509FindType.FindBySerialNumber;

            System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding(BasicHttpSecurityMode.Transport);
            binding.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.Certificate;
            binding.MaxReceivedMessageSize = 2147483647;


            System.ServiceModel.EndpointAddress remoteAddress =
                                new System.ServiceModel.EndpointAddress(caseServiceEndPoint);

            FacetPortTypeClient client = new FacetPortTypeClient(binding, remoteAddress);
            client.ChannelFactory.Credentials.ClientCertificate.SetCertificate(System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine,
                                                                               System.Security.Cryptography.X509Certificates.StoreName.My, CertificateFindType,
                                                                               certificateSerialNumber);
            return client;
        }
        public RequestHeaderType GetRequestHeaderType()
        {
            RequestHeaderType requestHeaderType = new RequestHeaderType()
            {
                TransactionUUID = Guid.NewGuid().ToString()
            };

            return requestHeaderType;
        }
    }

}
