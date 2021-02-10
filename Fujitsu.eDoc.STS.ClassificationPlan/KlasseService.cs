using Fujitsu.eDoc.STS.ClassificationPlan.SF1510_KlasseService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Fujitsu.eDoc.STS.ClassificationPlan
{
    class KlasseService : IKlasseService
    {
        /// <summary>
        /// Retrieves all Klassifications
        /// </summary>
        /// <param name="CVR"></param>
        /// <param name="klasseServiceEndPoint"></param>
        /// <param name="certificateSerialNumber"></param>
        /// <param name="brugerVendtNoegle"></param>
        /// <returns></returns>
        public async Task<string[]> SearchAllClassessAsync(string CVR, string klasseServiceEndPoint, string certificateSerialNumber, string brugerVendtNoegle)
        {
            string[] guid = new string[] { };
            using (KlassePortTypeClient kl = GetKlassePortTypeClient(klasseServiceEndPoint, certificateSerialNumber))
            {
                RequestHeaderType requestHeaderType = GetRequestHeaderType();
                SoegRequestType requestType = new SoegRequestType()
                {
                    AuthorityContext = new AuthorityContextType { MunicipalityCVR = CVR },
                    SoegInput = new SoegInputType1()
                    {
                        //SoegVirkning = new SoegVirkningType { TilTidspunkt = new TidspunktType { Item = "" } },
                        AttributListe = new AttributListeType
                        {
                            Egenskab = new EgenskabType[] { new EgenskabType { BrugervendtNoegleTekst = brugerVendtNoegle } }
                            //Egenskab = new EgenskabType[] { new EgenskabType { BrugervendtNoegleTekst = "00" }, new EgenskabType { BrugervendtNoegleTekst = "27.35.04" } }
                        },
                        TilstandListe = new TilstandListeType(),
                        RelationListe = new RelationListeType()
                    }
                };


                soegResponse respons = await kl.soegAsync(requestHeaderType, requestType);

                if (respons.SoegResponse1.SoegOutput.StandardRetur.StatusKode == "20")
                {
                    guid = respons.SoegResponse1.SoegOutput.IdListe;
                    await ListClassKLEs(CVR, klasseServiceEndPoint, certificateSerialNumber, guid);
                }
            }
            return guid;
        }


        public FiltreretOejebliksbilledeType ReadKLEClass(string CVR, string klasseServiceEndPoint, string certificateSerialNumber, string UUIDIdentifikator)
        {
            FiltreretOejebliksbilledeType filtreretOejebliksbilledeType = new FiltreretOejebliksbilledeType();
            using (KlassePortTypeClient kl = GetKlassePortTypeClient(klasseServiceEndPoint, certificateSerialNumber))
            {
                LaesRequestType laesRequestType = new LaesRequestType { AuthorityContext = new AuthorityContextType { MunicipalityCVR = CVR }, LaesInput = new LaesInputType { UUIDIdentifikator = UUIDIdentifikator } };
                RequestHeaderType requestHeaderType = GetRequestHeaderType();
                SoegRequestType requestType = new SoegRequestType()
                {
                    AuthorityContext = new AuthorityContextType { MunicipalityCVR = CVR },
                    SoegInput = new SoegInputType1()
                    {
                        AttributListe = new AttributListeType
                        {
                            Egenskab = new EgenskabType[] { new EgenskabType { } }
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

        public async Task<listResponse> ListClassKLEs(string CVR, string caseServiceEndPoint, string certificateSerialNumber, string[] uuid)
        {
            listResponse listResponse = new listResponse();

            using (KlassePortTypeClient client = GetKlassePortTypeClient(caseServiceEndPoint, certificateSerialNumber))
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

                //using (StreamWriter outputFile = new StreamWriter("C:\\Users\\denumk\\Desktop\\umut.txt", true))
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



        private KlassePortTypeClient GetKlassePortTypeClient(string caseServiceEndPoint, string certificateSerialNumber)
        {
            System.Security.Cryptography.X509Certificates.X509FindType CertificateFindType =
                                        System.Security.Cryptography.X509Certificates.X509FindType.FindBySerialNumber;

            System.ServiceModel.BasicHttpBinding binding = new System.ServiceModel.BasicHttpBinding(BasicHttpSecurityMode.Transport);
            binding.Security.Transport.ClientCredentialType = System.ServiceModel.HttpClientCredentialType.Certificate;
            binding.MaxReceivedMessageSize = 2147483647;


            System.ServiceModel.EndpointAddress remoteAddress =
                                new System.ServiceModel.EndpointAddress(caseServiceEndPoint);

            KlassePortTypeClient client = new KlassePortTypeClient(binding, remoteAddress);
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
