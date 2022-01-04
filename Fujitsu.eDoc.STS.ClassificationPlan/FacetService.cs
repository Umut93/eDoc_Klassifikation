//using System;
//using System.IdentityModel.Tokens;
//using System.IO;
//using System.Net.Security;
//using System.ServiceModel;
//using System.Threading.Tasks;
//using Digst.OioIdws.Soap.Behaviors;
//using Digst.OioIdws.Soap.Bindings;
//using Fujitsu.eDoc.Organisation.FKIntegration;
//using Fujitsu.eDoc.STS.ClassificationPlan.SF1510_V6_FacetService;

//namespace Fujitsu.eDoc.STS.ClassificationPlan
//{
//    class FacetService : IFacetService
//    {
//        /// <summary>
//        /// Finds and returns one facet (latest registration)
//        /// </summary>
//        /// <param name="CVR"></param>
//        /// <param name="facetServiceEndPoint"></param>
//        /// <param name="certificateSerialNumber"></param>
//        /// <param name="facetUUID"></param>
//        /// <returns></returns>
//        public FiltreretOejebliksbilledeType ReadFacet(string CVR, string facetServiceEndPoint, string certificateSerialNumber, string facetUUID)
//        {
//            FiltreretOejebliksbilledeType obj = new FiltreretOejebliksbilledeType();
//            //var client = GetFacetPortTypeClient(facetServiceEndPoint, certificateSerialNumber, tokk))
//            {
//                RequestHeaderType requestHeaderType = GetRequestHeaderType();

//                //LaesRequest laesRequestType = new LaesRequestType()
//                //{
//                //    AuthorityContext = new AuthorityContextType { MunicipalityCVR = CVR },
//                //    LaesInput = new LaesInputType
//                //    {
//                //        UUIDIdentifikator = facetUUID
//                //    }
//                //};
//                //LaesResponseType respons = ft.laes(ref requestHeaderType, laesRequestType);
//                //if (respons.LaesOutput.StandardRetur.StatusKode == "20")
//                //{
//                //    obj = respons.LaesOutput.FiltreretOejebliksbillede;
//                //}
//            }
//            return obj;
//        }
//        //public FiltreretOejebliksbilledeType ReadFacet(string CVR, string facetServiceEndPoint, string certificateSerialNumber, string facetUUID)
//        //{
//        //    FiltreretOejebliksbilledeType obj = new FiltreretOejebliksbilledeType();
//        //    //using (FacetPortTypeClient ft = GetFacetPortTypeClient(facetServiceEndPoint, certificateSerialNumber))
//        //    //{
//        //    //    RequestHeaderType requestHeaderType = GetRequestHeaderType();

//        //    //    //LaesRequest laesRequestType = new LaesRequestType()
//        //    //    //{
//        //    //    //    AuthorityContext = new AuthorityContextType { MunicipalityCVR = CVR },
//        //    //    //    LaesInput = new LaesInputType
//        //    //    //    {
//        //    //    //        UUIDIdentifikator = facetUUID
//        //    //    //    }
//        //    //    //};
//        //    //    //LaesResponseType respons = ft.laes(ref requestHeaderType, laesRequestType);
//        //    //    //if (respons.LaesOutput.StandardRetur.StatusKode == "20")
//        //    //    //{
//        //    //    //    obj = respons.LaesOutput.FiltreretOejebliksbillede;
//        //    //    //}
//        //    //}
//        //    return obj;
//        //}


//        ///// <summary>
//        ///// No criteria. Returns all!
//        ///// </summary>
//        ///// <param name="CVR"></param>
//        ///// <param name="facetServiceEndPoint"></param>
//        ///// <param name="certificateSerialNumber"></param>
//        ///// <param name="brugerVendtNoegle"></param>
//        ///// <returns></returns>
//        public async Task<string[]> SearhALLFacetsAsync(string CVR, string facetServiceEndPoint, SecurityToken token)
//        {
//            string[] guid = new string[] { };
//            FacetPortType client = GetFacetPortTypeClient("", "", token);

//            RequestHeaderType requestHeaderType = GetRequestHeaderType();

//            soegRequest soeg = new soegRequest { SoegInput = new SoegInputType1 { TilstandListe = new PubliceretStatusType[] { }, RelationListe = new RelationListeType { }, AttributListe = new EgenskabType[] { new EgenskabType { BrugervendtNoegleTekst = "*" } } } };

//            var respons = await client.soegAsync(soeg);

//            if (respons.SoegOutput.StandardRetur.StatusKode == "20")
//            {
//                //    //using (StreamWriter outputFile = new StreamWriter("C:\\Users\\denumk\\Desktop\\FacetService.txt", true))
//                //    //{
//                //    //    FiltreretOejebliksbilledeType[] arr = result.ListResponse1.ListOutput.FiltreretOejebliksbillede;

//                //    //    for (int i = 0; i < arr.Length; i++)
//                //    //    {
//                //    //        var value = arr[i].Registrering[0].AttributListe.Egenskab[0].BrugervendtNoegleTekst;
//                //    //        var value2 = arr[i].ObjektType.UUIDIdentifikator;

//                //    //        outputFile.WriteLine(value + " " + value2 + Environment.NewLine);
//                //    //    }
//                //    //}

//            }
//            return guid;

//        }

//        /// <summary>
//        /// Retrieve facets by UUIDS
//        /// </summary>
//        /// <param name="CVR"></param>
//        /// <param name="caseServiceEndPoint"></param>
//        /// <param name="certificateSerialNumber"></param>
//        /// <param name="uuid"></param>
//        /// <returns></returns>
//        //public async Task<listResponse> ListFacets(string CVR, string facetServiceEndPoint, string certificateSerialNumber, string[] UUIDs)
//        //{
//        //    listResponse listResponseType = new listResponse();

//        //    FacetPortType client = GetFacetPortTypeClient("86631628", "https://klassifikation.eksterntest-stoettesystemerne.dk/facet_6", ""))
//        //    {
//        //        ListRequestType listRequestType = new ListRequestType()
//        //        {
//        //            AuthorityContext = new AuthorityContextType()
//        //            {
//        //                MunicipalityCVR = CVR
//        //            },
//        //            ListInput = new ListInputType()
//        //            {
//        //                UUIDIdentifikator = UUIDs
//        //            }
//        //        };
//        //        RequestHeaderType requestHeaderType = GetRequestHeaderType();
//        //        listResponseType = await client.listAsync(requestHeaderType, listRequestType);

//        //    }
//        //    return listResponseType;
//        //}


//        private FacetPortType GetFacetPortTypeClient(string caseServiceEndPoint, string certificateSerialNumber, SecurityToken token)
//        {
//            FacetPortTypeClient clientPortType = new FacetPortTypeClient();
//            SoapBinding soapBinding = new SoapBinding();
//            EndpointIdentity identity = EndpointIdentity.CreateDnsIdentity("Klassifikation_T (funktionscertifikat)");
//            EndpointAddress endpointAddress = new EndpointAddress(clientPortType.Endpoint.ListenUri, identity);


//            FacetPortTypeClient client = new FacetPortTypeClient(soapBinding, endpointAddress);

//            client.Endpoint.Address = endpointAddress;

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
