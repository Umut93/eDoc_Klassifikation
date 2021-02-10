using Fujitsu.eDoc.STS.ClassificationPlan.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fujitsu.eDoc.STS.ClassificationPlan
{
    class Program
    {
        static void Main(string[] args)
        {

            testAsync();
            //testc();

            Console.WriteLine("Hello World!");
            Console.WriteLine("Hello Umut");
            Console.ReadKey();

            // Go to http://aka.ms/dotnet-get-started-console to continue learning how to build a console app! 
        }

        private static void testc()
        {
            IKlasseService klasseService = new KlasseService();

            klasseService.ReadKLEClass("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Klasse/5", "5a0c2122", "");
        }

        public static async Task testAsync()
        {
            IFacetService facetService = new FacetService();
            IKlasseService klasseService = new KlasseService();
            IKlassifikationService klassifikationService = new KlassifikationService();
            IKlassifikationSystemService klassifikationSystemService = new KlassifikationSystemService();

            await klassifikationSystemService.FremsoegobjekthierarkiAsync("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/KlassifikationSystem/5", "5a0c2122", "KLE");


            //FACET
            //facetService.ReadKLE("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Facet/5", "5a0c2122", "");
            //await facetService.SearhALLFacetsAsync("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Facet/5", "5a0c2122");


            //Klassifikation
            //var result = await klassifikationService.SearchAllClassificationsAsync("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Klassifikation/5", "5a0c2122");
            //var result2 = klassifikationService.ReadClassification("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Klassifikation/5", "5a0c2122", "0296de5f-6720-4fac-9a0c-80fe0b062e01");

            try
            {
                //Klasse
                //var result2 = await klasseService.SearchAllClassessAsync("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Klasse/5", "5a0c2122", "00");

            }
            catch (Exception ex)
            {

                throw ex;
            }
            var result11 = klasseService.ReadKLEClass("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Klasse/5", "5a0c2122", "0006cecb-a235-4462-9c8b-627e3661404a");

            //KlassifikationService
            //var result3 = await klassifikationService.SearchAllClassificationsAsync("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Klassifikation/5", "5a0c2122");



            Console.WriteLine("umut");

            Console.WriteLine();

            //KlasseService.Read("86631628", "https://exttest.serviceplatformen.dk/service/Klassifikation/Klasse/5", "5a0c2122", "");

        }


    }
}
