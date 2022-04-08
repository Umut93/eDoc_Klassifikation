using Dapper;
using System;
using System.Data.SqlClient;

namespace Fujitsu.eDoc.ScrapCode
{
    public class NovClassCodeRepo : INovClassCodeRepo
    {
        /// <summary>
        /// SletningJaevnfoerPersondataloven
        /// Dataelementet duration. 5 aar noteres som P1825D, hvor P er periode, og 1825 er antal, og D er dage. 
        /// Perioden gælder fra sagafslutning. Anvendelse af dataelementet forudsætter indbygget ESDH funktionalitet, hvor informationen om sagsafslutning kommer fra ESDH systemet
        /// </summary>
        /// <param name="emne"></param>
        public void UpdateEmnePlan(KLEEmneplanHovedgruppeGruppeEmne emne)
        {
            var connString = DbConnection.GetInstance.connString;

            int scrapCode = 0;

            if (emne.BevaringJaevnfoerArkivloven == "B")
            {
                scrapCode = (int)Model.ScrapCode.Bevares;
            }

            else if (emne.BevaringJaevnfoerArkivloven == "K")
            {
                scrapCode = (int)Model.ScrapCode.Kasseres;
            }

            else if (emne.BevaringJaevnfoerArkivloven.ToLower() == "tom")
            {
                scrapCode = (int)Model.ScrapCode.IkkeAfklaret;
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Execute("UPDATE nov_classcode set nov_scv_nkk=@scrapCode, nov_updatedate=@date FROM nov_classcode where nov_code=@EmneNr", new { scrapCode = scrapCode, date= DateTime.Now, EmneNr = emne.EmneNr });
            }
        }

        public void UpdateFacet(KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacet facet)
        {
            var connString = DbConnection.GetInstance.connString;

            int scrapCode = 0;
            int preserveYears = 0;

            if (facet.BevaringJaevnfoerArkivloven.Length == 1)
            {
                if (facet.BevaringJaevnfoerArkivloven == "B")
                {
                    scrapCode = (int)Model.ScrapCode.Bevares;
                }

                else if (facet.BevaringJaevnfoerArkivloven == "K")
                {
                    scrapCode = (int)Model.ScrapCode.Kasseres;
                }
            }

            else if (facet.BevaringJaevnfoerArkivloven.ToLower() == "tom")
            {
                scrapCode = (int)Model.ScrapCode.IkkeAfklaret;
            }

            else if (facet.BevaringJaevnfoerArkivloven.Length > 1)
            {
                preserveYears = int.Parse((facet.BevaringJaevnfoerArkivloven.Substring(1, facet.BevaringJaevnfoerArkivloven.Length - 1)));
                scrapCode = (int)Model.ScrapCode.Kasseres;
            }

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Execute("UPDATE nov_classcode set nov_scv_nkk=@scrapCode, nov_preserve=@preserveYears, nov_updatedate=@date  FROM nov_classcode where nov_code=@HandlingsfacetNr", new { scrapCode = scrapCode, preserveYears = preserveYears, date = DateTime.Now, facet.HandlingsfacetNr });
            }
        }
    }
}