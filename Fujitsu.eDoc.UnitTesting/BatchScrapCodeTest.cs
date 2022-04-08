using Fujitsu.eDoc.ScrapCode;
using Fujitsu.eDoc.ScrapCode.Ulitity;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Reflection;

namespace Fujitsu.eDoc.UnitTesting
{
    [TestClass]
    public class BatchScrapCodeTest
    {
        [TestMethod, TestCategory("KLE")]
        public void CallEmnePlanAPI_Result_OK()
        {
            //Act
            MethodInfo dynMethod = typeof(WebRequester).GetMethod("GetEmnePlaner", BindingFlags.Static | BindingFlags.Public);
            string result = (string)dynMethod.Invoke(null, null);
            //Assertion
            Assert.IsNotNull(result);
        }


        [TestMethod, TestCategory("KLE")]
        public void CallFacetAPI_Result_OK()
        {
            //Act
            MethodInfo dynMethod = typeof(WebRequester).GetMethod("GetFacets", BindingFlags.Static | BindingFlags.Public);
            string result = (string)dynMethod.Invoke(null, null);
            //Assertion
            Assert.IsNotNull(result);
        }



        [TestMethod, TestCategory("KLE")]
        public void UpdateEmnePlan_Result_OK()
        {
            var isUpdated = false;

            using (ShimsContext.Create())
            {
                var emnePlan = new KLEEmneplanHovedgruppeGruppeEmne();
                emnePlan.BevaringJaevnfoerArkivloven = "B";
                emnePlan.SletningJaevnfoerPersondataloven = "5";
                emnePlan.EmneNr = "01.01.01";

                Fujitsu.eDoc.ScrapCode.Fakes.ShimDbConnection.Constructor = (@this) => { };
                Dapper.Fakes.ShimSqlMapper.ExecuteIDbConnectionStringObjectIDbTransactionNullableOfInt32NullableOfCommandType = (a, b, c, d, e, f) =>
                {
                    isUpdated = true;
                    return 1;
                };

                //Act
                MethodInfo dynMethod = typeof(NovClassCodeRepo).GetMethod("UpdateEmnePlan", BindingFlags.Instance | BindingFlags.Public);
                INovClassCodeRepo novRepo = new NovClassCodeRepo();
                dynMethod.Invoke(novRepo, new object[] { emnePlan });

                //Assertion
                Assert.IsTrue(isUpdated);
            }
        }

        [TestMethod, TestCategory("KLE")]
        public void UpdateFacet_Result_OK()
        {
            var isUpdated = false;

            using (ShimsContext.Create())
            {
                var facet = new KLEHandlingsfacetterHandlingsfacetKategoriHandlingsfacet();
                facet.BevaringJaevnfoerArkivloven = "K10";

                Fujitsu.eDoc.ScrapCode.Fakes.ShimDbConnection.Constructor = (@this) => { };
                Dapper.Fakes.ShimSqlMapper.ExecuteIDbConnectionStringObjectIDbTransactionNullableOfInt32NullableOfCommandType = (a, b, c, d, e, f) =>
                {
                    isUpdated = true;
                    return 1;
                };

                //Act
                MethodInfo dynMethod = typeof(NovClassCodeRepo).GetMethod("UpdateFacet", BindingFlags.Instance | BindingFlags.Public);
                INovClassCodeRepo novRepo = new NovClassCodeRepo();
                dynMethod.Invoke(novRepo, new object[] { facet });

                //Assertion
                Assert.IsTrue(isUpdated);
            }
        }
    }
}
