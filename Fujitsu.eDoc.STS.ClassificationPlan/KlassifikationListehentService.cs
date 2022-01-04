//using Digst.OioIdws.Soap.Behaviors;
//using Digst.OioIdws.Soap.Bindings;
//using Fujitsu.eDoc.STS.ClassificationPlan.Model;
//using Fujitsu.eDoc.STS.ClassificationPlan.SF1510_V6_KlassifikationListehent;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens;
//using System.Linq;
//using System.Net.Security;
//using System.ServiceModel;
//using System.Threading.Tasks;

//namespace Fujitsu.eDoc.STS.ClassificationPlan
//{
//    public class KlassifikationListehentService
//    {
//        /// <summary>
//        /// Retrieves all KLEs which either is expired or not.
//        /// </summary>
//        /// <param name="CVR"></param>
//        /// <param name="klasseServiceEndPoint"></param>
//        /// <param name="certificateSerialNumber"></param>
//        /// <param name="brugerVendtNoegle"></param>
//        /// <returns></returns>
//        public async Task<List<STSKLE>> Test(string CVR, string klasseServiceEndPoint, string certificateSerialNumber, string brugerVendtNoegle, SecurityToken token)
//        {
//            List<STSKLE> list = new List<STSKLE>();

//            var client = GetKlassifikationSystemPortTypeClient(klasseServiceEndPoint, certificateSerialNumber, token);

//            KlassifikationListeHentRequest request = new KlassifikationListeHentRequest
//            {

//                KlassifikationListeHent_I = new KlassifikationListeHent_I
//                {
//                    KlassifikationKriterieListe = new KlassifikationListeHent_ITypeIdentifikationKriterie[]
//                    {
//                       new KlassifikationListeHent_ITypeIdentifikationKriterie { Item = "d7b49aa8-e1eb-407b-9bc8-9d91ac270fd", ItemElementName = ItemChoiceType.FacetIdentifikation}
//                    },
//                    HovedOplysninger = new HovedOplysningerType
//                    {
//                        TransaktionsId = Guid.NewGuid().ToString(),
//                    }

//                }
//            };

//            try
//            {
//                KlassifikationListeHentRequest1 respons = client.KlassifikationListeHent(request);


//                if (respons.KlassifikationListeHent_O.KlassifikationListe.Length > 0)
//                {
//                    Dictionary<string, string> stsKLEGUIDPair = new Dictionary<string, string>();

//                    KlassifikationStrukturType[] klasser = respons.KlassifikationListeHent_O.KlassifikationListe;
//                    for (int i = 0; i < klasser.Length; i++)
//                    {

//                    }
//                }
//            }
//            catch (Exception ex)
//            {

//                throw ex;
//            }

//            return list;
//        }

//        public KlassifikationListeHentServicePortType GetKlassifikationSystemPortTypeClient(string caseServiceEndPoint, string certificateSerialNumber, SecurityToken token)
//        {

//            KlassifikationListeHentServicePortTypeClient clientPortType = new KlassifikationListeHentServicePortTypeClient();

//            SoapBinding soapBinding = new SoapBinding();
//            //soapBinding._maxReceivedMessageSize = 2147483647;

//            EndpointIdentity identity = EndpointIdentity.CreateDnsIdentity("Klassifikation_T (funktionscertifikat)");
//            EndpointAddress endpointAddress = new EndpointAddress(clientPortType.Endpoint.ListenUri, identity);


//            KlassifikationListeHentServicePortTypeClient client = new KlassifikationListeHentServicePortTypeClient(soapBinding, endpointAddress);

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
//            System.ServiceModel.Security.X509CertificateValidationMode.ChainTrust;


//            // This sets the MINIMUM level. Since the request header should not be signed, we set it to none.
//            client.Endpoint.Contract.ProtectionLevel = ProtectionLevel.None;

//            // adding the custom beheviors from Digst.OioIdws.Soap
//            ClientMessageInspectorBehavior c = new ClientMessageInspectorBehavior();
//            SoapClientBehavior s = new SoapClientBehavior();
//            client.ChannelFactory.Endpoint.EndpointBehaviors.Add(c);
//            client.ChannelFactory.Endpoint.EndpointBehaviors.Add(s);

//            return client.ChannelFactory.CreateChannelWithIssuedToken(token);

//        }
//    }
//}
