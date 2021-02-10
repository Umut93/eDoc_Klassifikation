using Fujitsu.eDoc.STS.ClassificationPlan.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fujitsu.eDoc.STS.ClassificationPlan.BLL
{
    public class FacetManagerBLL : Manager
    {
        private int mFacetClassType = 203000;

        public void CreateFacet(List<STSKLE> facets, List<EdocEmnePlan> eDocFacetPlaner)
        {
            //System.Diagnostics.Debugger.Launch();
            List<STSKLE> facetCategories = facets.Where(x => x.Code.Length == 1).ToList();

            foreach (STSKLE facetCategory in facetCategories)
            {
                EdocEmnePlan eDocFacetCategory = eDocFacetPlaner.Find(x => x.Code == facetCategory.Code);
                if (eDocFacetCategory == null)
                {
                    // Create facet top level
                    eDocFacetCategory = new EdocEmnePlan()
                    {
                        ClassType = mFacetClassType,
                        Code = facetCategory.Code,
                        Description = facetCategory.TitleText,
                        UUID = facetCategory.UUID
                    };
                    CreateFacetCategory(eDocFacetCategory);
                }
                else
                {
                    eDocFacetCategory.Code = facetCategory.Code;
                    eDocFacetCategory.Description = facetCategory.TitleText;
                    eDocFacetCategory.UUID = facetCategory.UUID;
                    UpdateFacetCategory(eDocFacetCategory);
                }

                List<STSKLE> factes = facets.Where(x => x.Code.Substring(0, 1) == eDocFacetCategory.Code && x.Code.Length == 3).ToList();
                foreach (STSKLE facet in factes)
                {
                    EdocEmnePlan eDocFacet = eDocFacetPlaner.Find(x => x.Code == facet.Code);
                    if (eDocFacet == null)
                    {
                        // Create facet top level
                        eDocFacet = new EdocEmnePlan()
                        {
                            ClassType = mFacetClassType,
                            Code = facet.Code,
                            Description = facet.TitleText,
                            UUID = facet.UUID
                        };
                        CreateFacet(eDocFacet, eDocFacetCategory.Recno, facet.IsExpired);
                    }
                    else
                    {
                        eDocFacet.Code = facet.Code;
                        eDocFacet.Description = facet.TitleText;
                        eDocFacet.UUID = facet.UUID;
                        UpdateFacet(eDocFacet, facet.IsExpired);
                    }
                }
            }
        }

        public void CreateFacetCategory(EdocEmnePlan eDocFacetCategory)
        {
            SqlCommand cmdClasscode = new SqlCommand();
            cmdClasscode.CommandText = @"INSERT INTO nov_classcode 
        (nov_recno, nov_lan_code, nov_code, nov_desc, nov_structureno, nov_allowuse, 
        nov_secclass, nov_nop_recno, nov_insertdate, nov_insertby, nov_updatedate, nov_updateby, nov_sts_kle_guid) 
        VALUES(@recno, @language, @nov_code, @nov_desc, @structureno, 0, 
        @nov_secclass, @nov_nop_recno, @nov_insertdate, @nov_insertby, @nov_updatedate, @nov_updateby, @nov_sts_kle_guid)";

            cmdClasscode.Parameters.Add(new SqlParameter("@recno", 0));
            cmdClasscode.Parameters.Add(new SqlParameter("@language", "DAN"));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_code", eDocFacetCategory.Code));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_desc", eDocFacetCategory.Description));
            cmdClasscode.Parameters.Add(new SqlParameter("@structureno", ""));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_secclass", -1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_nop_recno", eDocFacetCategory.ClassType));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_insertdate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_insertby", 1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updatedate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updateby", 1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_sts_kle_guid", eDocFacetCategory.UUID));


            SqlConnection con = new SqlConnection(Fujitsu.eDoc.STS.ClassificationPlan.BLL.DBConnection.GetConnectionString());
            SqlTransaction trans = null;
            try
            {
                con.Open();
                cmdClasscode.Connection = con;

                trans = con.BeginTransaction();
                cmdClasscode.Transaction = trans;

                int recno = ExecuteCreateMultiLanguage("noark classification code", cmdClasscode);
                trans.Commit();

                eDocFacetCategory.Recno = recno;

            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void UpdateFacetCategory(EdocEmnePlan eDocFacetCategory)
        {
            // Update classcode
            SqlCommand cmdClasscode = new SqlCommand();
            cmdClasscode.CommandText = "UPDATE nov_classcode SET nov_code=@nov_code, nov_desc=@nov_desc, nov_updatedate=@nov_updatedate, nov_updateby=@nov_updateby, nov_sts_kle_guid=@nov_sts_kle_guid, nov_allowuse=0 WHERE nov_recno=@recno";
            cmdClasscode.Parameters.Add(new SqlParameter("@recno", eDocFacetCategory.Recno));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_code", eDocFacetCategory.Code));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_desc", eDocFacetCategory.Description));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updatedate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updateby", 1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_sts_kle_guid", eDocFacetCategory.UUID));

            SqlConnection con = new SqlConnection(Fujitsu.eDoc.STS.ClassificationPlan.BLL.DBConnection.GetConnectionString());
            SqlTransaction trans = null;
            try
            {
                con.Open();
                cmdClasscode.Connection = con;

                trans = con.BeginTransaction();
                cmdClasscode.Transaction = trans;

                cmdClasscode.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

                throw ex;
            }
            finally
            {
                con.Close();
            }

        }

        public void CreateFacet(EdocEmnePlan eDocFacet, int parentRecno, bool isExpired)
        {
            // Create classcode
            SqlCommand cmdClasscode = new SqlCommand();
            cmdClasscode.CommandText = @"INSERT INTO nov_classcode 
        (nov_recno, nov_lan_code, nov_code, nov_desc, nov_structureno, 
        nov_allowuse, nov_secclass, nov_nop_recno, 
        nov_insertdate, nov_insertby, nov_updatedate, nov_updateby, nov_sts_kle_guid) 
        VALUES(@recno, @language, @nov_code, @nov_desc, @structureno, 
        @nov_allowuse, @nov_secclass, @nov_nop_recno, @nov_scv_nkk, @nov_preserve,
        @nov_insertdate, @nov_insertby, @nov_updatedate, @nov_updateby, @nov_sts_kle_guid)";
            cmdClasscode.Parameters.Add(new SqlParameter("@recno", 0));
            cmdClasscode.Parameters.Add(new SqlParameter("@language", "DAN"));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_code", eDocFacet.Code));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_desc", eDocFacet.Description));
            cmdClasscode.Parameters.Add(new SqlParameter("@structureno", parentRecno.ToString() + "M"));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_allowuse", isExpired == true ? 0 : -1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_secclass", -1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_nop_recno", mFacetClassType));
            //cmdClasscode.Parameters.Add(new SqlParameter("@nov_scv_nkk", pFacet.ScrapCode));
            //cmdClasscode.Parameters.Add(new SqlParameter("@nov_preserve", pFacet.PreserveYears));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_insertdate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_insertby", 1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updatedate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updateby", 1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_sts_kle_guid", eDocFacet.UUID));


            SqlConnection con = new SqlConnection(Fujitsu.eDoc.STS.ClassificationPlan.BLL.DBConnection.GetConnectionString());
            SqlTransaction trans = null;
            try
            {
                con.Open();
                cmdClasscode.Connection = con;

                trans = con.BeginTransaction();
                cmdClasscode.Transaction = trans;

                int recno = ExecuteCreateMultiLanguage("noark classification code", cmdClasscode);
                trans.Commit();

                eDocFacet.Recno = recno;

            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void UpdateFacet(EdocEmnePlan eDocFacet, bool isExpired)
        {
            // Update classcode
            SqlCommand cmdClasscode = new SqlCommand();
            cmdClasscode.CommandText = @"UPDATE nov_classcode SET nov_code=@nov_code, nov_desc=@nov_desc, nov_allowuse=@nov_allowuse, 
            nov_updatedate=@nov_updatedate, nov_updateby=@nov_updateby, nov_sts_kle_guid=@nov_sts_kle_guid WHERE nov_recno=@recno";
            cmdClasscode.Parameters.Add(new SqlParameter("@recno", eDocFacet.Recno));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_code", eDocFacet.Code));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_desc", eDocFacet.Description));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_allowuse", isExpired == true ? 0 : -1));
            //cmdClasscode.Parameters.Add(new SqlParameter("@nov_scv_nkk", pFacet.ScrapCode));
            //cmdClasscode.Parameters.Add(new SqlParameter("@nov_preserve", pFacet.PreserveYears));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updatedate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updateby", 1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_sts_kle_guid", eDocFacet.UUID));

            SqlConnection con = new SqlConnection(Fujitsu.eDoc.STS.ClassificationPlan.BLL.DBConnection.GetConnectionString());
            SqlTransaction trans = null;
            try
            {
                con.Open();
                cmdClasscode.Connection = con;

                trans = con.BeginTransaction();
                cmdClasscode.Transaction = trans;

                cmdClasscode.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

    }
}