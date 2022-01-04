//using Digst.OioIdws.Soap.Behaviors;
//using Digst.OioIdws.Soap.Bindings;
//using Fujitsu.eDoc.STS.ClassificationPlan.SF1510_V6_KlasseService;
//using System;
//using System.IdentityModel.Tokens;
//using System.Net.Security;
//using System.ServiceModel;
//using System.Threading.Tasks;

//namespace Fujitsu.eDoc.STS.ClassificationPlan
//{

//    public class KlasseService : IKlasseService
//    {
//        /// <summary>
//        /// Retrieves all Klassifications
//        /// </summary>
//        /// <param name="CVR"></param>
//        /// <param name="klasseServiceEndPoint"></param>
//        /// <param name="certificateSerialNumber"></param>
//        /// <param name="brugerVendtNoegle"></param>
//        /// <returns></returns>
//        public async Task<string[]> SearchAllClassessAsync(string CVR, string klasseServiceEndPoint, string certificateSerialNumber, string brugerVendtNoegle)
//        {
//            string[] guid = new string[] { };
//            //using (KlassePortTypeClient kl = GetKlassePortTypeClient(klasseServiceEndPoint, certificateSerialNumber))
//            //{
//            //    RequestHeaderType requestHeaderType = GetRequestHeaderType();
//            //    SoegRequestType requestType = new SoegRequestType()
//            //    {
//            //        AuthorityContext = new AuthorityContextType { MunicipalityCVR = CVR },
//            //        SoegInput = new SoegInputType1()
//            //        {
//            //            //SoegVirkning = new SoegVirkningType { TilTidspunkt = new TidspunktType { Item = "" } },
//            //            AttributListe = new AttributListeType
//            //            {
//            //                Egenskab = new EgenskabType[] { new EgenskabType { BrugervendtNoegleTekst = brugerVendtNoegle } }
//            //                //Egenskab = new EgenskabType[] { new EgenskabType { BrugervendtNoegleTekst = "00" }, new EgenskabType { BrugervendtNoegleTekst = "27.35.04" } }
//            //            },
//            //            TilstandListe = new TilstandListeType(),
//            //            RelationListe = new RelationListeType()
//            //        }
//            //    };


//            //    soegResponse respons = await kl.soegAsync(requestHeaderType, requestType);

//            //    if (respons.SoegResponse1.SoegOutput.StandardRetur.StatusKode == "20")
//            //    {
//            //        guid = respons.SoegResponse1.SoegOutput.IdListe;
//            //        await ListClassKLEs(CVR, klasseServiceEndPoint, certificateSerialNumber, guid);
//            //    }
//            //}
//            return guid;
//        }


//        public FiltreretOejebliksbilledeType ReadKLEClass(string CVR, string klasseServiceEndPoint, string certificateSerialNumber, string UUIDIdentifikator)
//        {
//            FiltreretOejebliksbilledeType filtreretOejebliksbilledeType = new FiltreretOejebliksbilledeType();
//            //using (KlassePortTypeClient kl = GetKlassePortTypeClient(klasseServiceEndPoint, certificateSerialNumber))
//            //{
//            //    LaesRequestType laesRequestType = new LaesRequestType { AuthorityContext = new AuthorityContextType { MunicipalityCVR = CVR }, LaesInput = new LaesInputType { UUIDIdentifikator = UUIDIdentifikator } };
//            //    RequestHeaderType requestHeaderType = GetRequestHeaderType();
//            //    SoegRequestType requestType = new SoegRequestType()
//            //    {
//            //        AuthorityContext = new AuthorityContextType { MunicipalityCVR = CVR },
//            //        SoegInput = new SoegInputType1()
//            //        {
//            //            AttributListe = new AttributListeType
//            //            {
//            //                Egenskab = new EgenskabType[] { new EgenskabType { } }
//            //            },
//            //            TilstandListe = new TilstandListeType(),
//            //            RelationListe = new RelationListeType()
//            //        }
//            //    };

//            //    LaesResponseType respons = kl.laes(ref requestHeaderType, laesRequestType);
//            //    if (respons.LaesOutput.StandardRetur.StatusKode == "20")
//            //    {
//            //        filtreretOejebliksbilledeType = respons.LaesOutput.FiltreretOejebliksbillede;
//            //    }

//            //}
//            return filtreretOejebliksbilledeType;
//        }

//        public async Task<listResponse> ListClassKLEs(string CVR, string caseServiceEndPoint, string certificateSerialNumber, string[] uuid)
//        {
//            listResponse listResponse = new listResponse();

//            //using (KlassePortTypeClient client = GetKlassePortTypeClient(caseServiceEndPoint, certificateSerialNumber))
//            //{
//            //    ListRequestType listRequestType = new ListRequestType()
//            //    {
//            //        AuthorityContext = new AuthorityContextType()
//            //        {
//            //            MunicipalityCVR = CVR
//            //        },
//            //        ListInput = new ListInputType()
//            //        {
//            //            UUIDIdentifikator = uuid

//            //        }
//            //    };
//            //    RequestHeaderType requestHeaderType = GetRequestHeaderType();
//            //    listResponse = await client.listAsync(requestHeaderType, listRequestType);

//            //    //using (StreamWriter outputFile = new StreamWriter("C:\\Users\\denumk\\Desktop\\umut.txt", true))
//            //    //{
//            //    //    foreach (var l in listResponse.ListResponse1.ListOutput.FiltreretOejebliksbillede)
//            //    //    {
//            //    //        string value = l.Registrering.FirstOrDefault().AttributListe.Egenskab.Select(x => x.BrugervendtNoegleTekst).FirstOrDefault();
//            //    //        string value2 = l.ObjektType.UUIDIdentifikator;


//            //    //        outputFile.WriteLine(value + " " + value2 + Environment.NewLine);
//            //    //    }
//            //    //}

//            //}
//            return listResponse;
//        }
//        public SoegOutputType Soeg(string brugervendtNoegle, SecurityToken token)
//        {
//            soegRequest request = new soegRequest(
//                GetRequestHeaderType(),
//                new SoegInputType1()
//                {
//                    AttributListe = new EgenskabType[]
//                    {
//                        new EgenskabType
//                        {
//                            BrugervendtNoegleTekst = brugervendtNoegle
//                        }
//                    },
//                    // Check this line after KLA6 is in production
//                    TilstandListe = new PubliceretStatusType[0],
//                    RelationListe = new RelationListeType()
//                }
//            );

//            var Port = GetKlassePortTypeClient("", "", token);
//            soegResponse soegResponse = Port.soeg(request);
//            return soegResponse.SoegOutput;

//        }
//        private KlassePortType GetKlassePortTypeClient(string caseServiceEndPoint, string certificateSerialNumber, SecurityToken token)
//        {
//            KlassePortTypeClient clientPortType = new KlassePortTypeClient();

//            SoapBinding soapBinding = new SoapBinding();
//            EndpointIdentity identity = EndpointIdentity.CreateDnsIdentity("Klassifikation_T (funktionscertifikat)");
//            EndpointAddress endpointAddress = new EndpointAddress(clientPortType.Endpoint.ListenUri, identity);

//            KlassePortTypeClient client = new KlassePortTypeClient(soapBinding, endpointAddress);



//            // loads the funtioncertifikat for klassification service
//            client.ChannelFactory.Credentials.ServiceCertificate.SetDefaultCertificate(System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine,
//                                                                                       System.Security.Cryptography.X509Certificates.StoreName.My,
//                                                                                       System.Security.Cryptography.X509Certificates.X509FindType.FindBySerialNumber,
//                                                                                       "5baaef71");


//            // loads the functioncertificat installed on the anvendersystem
//            client.ChannelFactory.Credentials.ClientCertificate.SetCertificate(System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine,
//                                                                               System.Security.Cryptography.X509Certificates.StoreName.My,
//                                                                               System.Security.Cryptography.X509Certificates.X509FindType.FindBySerialNumber,
//                                                                               "5e1c480e");

//            //IN PROD DONT..
//            client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode =
//            System.ServiceModel.Security.X509CertificateValidationMode.None;


//            // This sets the MINIMUM level. Since the request header should not be signed, we set it to none.
//            client.Endpoint.Contract.ProtectionLevel = ProtectionLevel.None;

//            // adding the custom beheviors from Digst.OioIdws.Soap
//            ClientMessageInspectorBehavior c = new ClientMessageInspectorBehavior();
//            SoapClientBehavior s = new SoapClientBehavior();
//            client.ChannelFactory.Endpoint.EndpointBehaviors.Add(c);
//            client.ChannelFactory.Endpoint.EndpointBehaviors.Add(s);

//            return client.ChannelFactory.CreateChannelWithIssuedToken(token);
//        }

//        public RequestHeaderType GetRequestHeaderType()
//        {
//            RequestHeaderType requestHeaderType = new RequestHeaderType()
//            {
//                TransactionUUID = Guid.NewGuid().ToString()
//            };

//            return requestHeaderType;
//        }

//    }


//}
