namespace Fujitsu.eDoc.STS.ClassificationPlan
{
    class Program
    {
        static void Main(string[] args)
        {

            //            testAsync();
            //            //testc();

            //            Console.WriteLine("Hello World!");
            //            Console.WriteLine("Hello Umut");
            //            Console.ReadKey();

            //            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
            //        }

            //        private static void testc()
            //        {
            //            IKlasseService klasseService = new KlasseService();

            //            klasseService.ReadKLEClass("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Klasse/5", "5a0c2122", "");
            //        }

            //        public static void testAsync()
            //        {
            //            IFacetService facetService = new FacetService();
            //            IKlasseService klasseService = new KlasseService();
            //            IKlassifikationService klassifikationService = new KlassifikationService();
            //            IKlassifikationSystemService klassifikationSystemService = new KlassifikationSystemService();

            //            //    await klassifikationSystemService.FremsoegobjekthierarkiAsync("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/KlassifikationSystem/5", "5a0c2122", "KLE");


            //            //FACET
            //            //facetService.ReadKLE("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Facet/5", "5a0c2122", "");
            //            //await facetService.SearhALLFacetsAsync("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Facet/5", "5a0c2122");

            //            //Klassifikation
            //            //var result = await klassifikationService.SearchAllClassificationsAsync("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Klassifikation/5", "5a0c2122");
            //            //var result2 = klassifikationService.ReadClassification("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Klassifikation/5", "5a0c2122", "0296de5f-6720-4fac-9a0c-80fe0b062e01");
            //            //try
            //            //{
            //            //    //Klasse
            //            //    //var result2 = await klasseService.SearchAllClassessAsync("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Klasse/5", "5a0c2122", "00");
            //            //}
            //            //catch (Exception ex)
            //            //{

            //            //    throw ex;
            //            //}
            //            //var result11 = klasseService.ReadKLEClass("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Klasse/5", "5a0c2122", "0006cecb-a235-4462-9c8b-627e3661404a");
            //            //KlassifikationService
            //            //var result3 = await klassifikationService.SearchAllClassificationsAsync("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Klassifikation/5", "5a0c2122");


            //            System.IdentityModel.Tokens.SecurityToken token = TokenFetcher.IssueToken(FKContextFetcher.GetSettings());

            //            KlassifikationListehentService s = new KlassifikationListehentService();
            //            var result = s.Test("", "", "", "", token);

            //            //klassifikationSystemService.FremsoegobjekthierarkiAsync("", "", "", "KLE", token);

            //            //klasseService.Soeg("27.35.04", token); //-- works


            //            //facetService.SearhALLFacetsAsync("", "", token);

            //            //SoapBinding soapBinding = new SoapBinding();
            //            //EndpointAddress defaultendpoint = new EndpointAddress(new Uri("https://klassifikation.eksterntest-stoettesystemerne.dk/klassifikationlistehent_1"),
            //            //                                                      EndpointIdentity.CreateDnsIdentity("Klassifikation_T (funktionscertifikat)"));

            //            //SF1510_V6_KlassifikationListehent.KlassifikationListeHentServicePortTypeClient client = new KlassifikationListeHentServicePortTypeClient(soapBinding, defaultendpoint);

            //            //// loads the funtioncertifikat for organisation service
            //            //client.ChannelFactory.Credentials.ServiceCertificate.SetDefaultCertificate(System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine,
            //            //                                                                           System.Security.Cryptography.X509Certificates.StoreName.My,
            //            //                                                                           System.Security.Cryptography.X509Certificates.X509FindType.FindBySerialNumber,
            //            //                                                                           "5baaef71");

            //            //// loads the functioncertificat installed on the anvendersystem
            //            //client.ChannelFactory.Credentials.ClientCertificate.SetCertificate(System.Security.Cryptography.X509Certificates.StoreLocation.LocalMachine,
            //            //                                                                   System.Security.Cryptography.X509Certificates.StoreName.My,
            //            //                                                                   System.Security.Cryptography.X509Certificates.X509FindType.FindBySerialNumber,
            //            //                                                                   "5e1c480e");


            //            ////IN PROD DONT..
            //            //client.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode =
            //            //System.ServiceModel.Security.X509CertificateValidationMode.None;


            //            //// This sets the MINIMUM level. Since the request header should not be signed, we set it to none.
            //            //client.Endpoint.Contract.ProtectionLevel = ProtectionLevel.None;

            //            //// adding the custom beheviors from Digst.OioIdws.Soap
            //            //ClientMessageInspectorBehavior c = new ClientMessageInspectorBehavior();
            //            //SoapClientBehavior s = new SoapClientBehavior();
            //            //client.ChannelFactory.Endpoint.EndpointBehaviors.Add(c);
            //            //client.ChannelFactory.Endpoint.EndpointBehaviors.Add(s);

            //            //client.ChannelFactory.CreateChannelWithIssuedToken(token);

            //            //var res = client.KlassifikationListeHent(new KlassifikationListeHent_I { KunFaelleskommunaleKlasser = true, HovedOplysninger = new HovedOplysningerType { } });

            //            //await klassifikationSystemService.FremsoegobjekthierarkiAsync("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/KlassifikationSystem/5", "5a0c2122", "KLE");


            //            //FACET
            //            //facetService.ReadKLE("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Facet/5", "5a0c2122", "");
            //            //await facetService.SearhALLFacetsAsync("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Facet/5", "5a0c2122");

            //            Console.WriteLine("umut");

            //            Console.WriteLine();

            //            //KlasseService.Read("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Klasse/5", "5a0c2122", "");


            //            //    //Klassifikation
            //            //    //var result = await klassifikationService.SearchAllClassificationsAsync("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Klassifikation/5", "5a0c2122");
            //            //    //var result2 = klassifikationService.ReadClassification("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Klassifikation/5", "5a0c2122", "0296de5f-6720-4fac-9a0c-80fe0b062e01");

            //            //    try
            //            //    {
            //            //        //Klasse
            //            //        //var result2 = await klasseService.SearchAllClassessAsync("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Klasse/5", "5a0c2122", "00");

            //            //    }
            //            //    catch (Exception ex)
            //            //    {

            //            //        throw ex;
            //            //    }
            //            //    var result11 = klasseService.ReadKLEClass("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Klasse/5", "5a0c2122", "0006cecb-a235-4462-9c8b-627e3661404a");

            //            //    //KlassifikationService
            //            //    //var result3 = await klassifikationService.SearchAllClassificationsAsync("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Klassifikation/5", "5a0c2122");



            //            //    Console.WriteLine("umut");

            //            //    Console.WriteLine();

            //            //    //KlasseService.Read("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Klasse/5", "5a0c2122", "");

        }

    }
}
