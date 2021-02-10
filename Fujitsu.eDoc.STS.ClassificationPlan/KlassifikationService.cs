using Fujitsu.eDoc.STS.ClassificationPlan.SF1510_KlassifikationService;
using System;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;

namespace Fujitsu.eDoc.STS.ClassificationPlan
{
    class KlassifikationService : IKlassifikationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CVR"></param>
        /// <param name="klasseServiceEndPoint"></param>
        /// <param name="certificateSerialNumber"></param>
        /// <param name="brugerVendtNoegle"></param>
        /// <returns></returns>
        public async Task<string[]> SearchAllClassificationsAsync(string CVR, string klasseServiceEndPoint, string certificateSerialNumber)
        {
            string[] guid = new string[] { };
            using (KlassifikationPortTypeClient kl = GetKlassifikationPortTypeClient(klasseServiceEndPoint, certificateSerialNumber))
            {
                RequestHeaderType requestHeaderType = GetRequestHeaderType();
                SoegRequestType requestType = new SoegRequestType()
                {
                    AuthorityContext = new AuthorityContextType { MunicipalityCVR = CVR },
                    SoegInput = new SoegInputType1()
                    {
                        AttributListe = new AttributListeType
                        {
                            Egenskab = new EgenskabType[] { new EgenskabType { BrugervendtNoegleTekst = "" } },
                        },
                        TilstandListe = new TilstandListeType { },
                        RelationListe = new RelationListeType { }
                    }
                };


                soegResponse respons = await kl.soegAsync(requestHeaderType, requestType);

                if (respons.SoegResponse1.SoegOutput.StandardRetur.StatusKode == "20")
                {
                    guid = respons.SoegResponse1.SoegOutput.IdListe;
                    await ListClassifications(CVR, klasseServiceEndPoint, certificateSerialNumber, guid);
                }

            }
            return guid;
        }

        public FiltreretOejebliksbilledeType ReadClassification(string CVR, string klasseServiceEndPoint, string certificateSerialNumber, string brugerVendtNoegle)
        {
            FiltreretOejebliksbilledeType filtreretOejebliksbilledeType = new FiltreretOejebliksbilledeType();
            using (KlassifikationPortTypeClient kl = GetKlassifikationPortTypeClient(klasseServiceEndPoint, certificateSerialNumber))
            {
                LaesRequestType laesRequestType = new LaesRequestType { AuthorityContext = new AuthorityContextType { MunicipalityCVR = CVR } };
                RequestHeaderType requestHeaderType = GetRequestHeaderType();
                SoegRequestType requestType = new SoegRequestType()
                {
                    AuthorityContext = new AuthorityContextType { MunicipalityCVR = CVR },
                    SoegInput = new SoegInputType1()
                    {
                        AttributListe = new AttributListeType
                        {
                            Egenskab = new EgenskabType[] { new EgenskabType { BrugervendtNoegleTekst = "" } }

                        },
                        TilstandListe = new TilstandListeType(),
                        RelationListe = new RelationListeType()
                    }
                };

                LaesResponseType respons = kl.laes(ref requestHeaderType, laesRequestType);
                if (respons.LaesOutput.StandardRetur.StatusKode == "20")
                {
                    filtreretOejebliksbilledeType = respons.LaesOutput.FiltreretOejebliksbillede;
                }

            }
            return filtreretOejebliksbilledeType;
        }

        public async Task<listResponse> ListClassifications(string CVR, string caseServiceEndPoint, string certificateSerialNumber, string[] uuid)
        {
            listResponse listResponse = new listResponse();

            using (KlassifikationPortTypeClient client = GetKlassifikationPortTypeClient(caseServiceEndPoint, certificateSerialNumber))
            {
                ListRequestType listRequestType = new ListRequestType()
                {
                    AuthorityContext = new AuthorityContextType()
                    {
                        MunicipalityCVR = CVR
                    },
                    ListInput = new ListInputType()
                    {
                        UUIDIdentifikator = uuid
                    }

                };
                RequestHeaderType requestHeaderType = GetRequestHeaderType();
                listResponse = await client.listAsync(requestHeaderType, listRequestType);

                //using (StreamWriter outputFile = new StreamWriter("C:\\Users\\denumk\\Desktop\\KlassifikationService.txt", true))
                //{
                //    foreach (var l in listResponse.ListResponse1.ListOutput.FiltreretOejebliksbillede)
                //    {
                //        string value = l.Registrering.FirstOrDefault().AttributListe.Egenskab.Select(x => x.BrugervendtNoegleTekst).FirstOrDefault();
                //        string value2 = l.ObjektType.UUIDIdentifikator;


                //        outputFile.WriteLine(value + " " + value2 + Environment.NewLine);
                //    }
                //}

            }
            return listResponse;
        }


        private KlassifikationPortTypeClient GetKlassifikationPortTypeClient(string caseServiceEndPoint, string certificateSerialNumber)
        {
            System.Security.Cryptography.X509Certificates.X509FindType CertificateFindType =
                                        System.Security.Cryptography.X509Certificates.X509FindType.FindBySerialNumber;

            System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding(BasicHttpSecurityMode.Transport);
            binding.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.Certificate;
            binding.MaxReceivedMessageSize = 2147483647;


            System.ServiceModel.EndpointAddress remoteAddress =
                                new System.ServiceModel.EndpointAddress(caseServiceEndPoint);

            KlassifikationPortTypeClient client = new KlassifikationPortTypeClient(binding, remoteAddress);
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
