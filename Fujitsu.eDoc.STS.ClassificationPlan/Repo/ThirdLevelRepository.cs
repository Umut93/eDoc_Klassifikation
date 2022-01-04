using Fujitsu.eDoc.STS.ClassificationPlan.BLL;
using Fujitsu.eDoc.STS.ClassificationPlan.Model;
using System;
using System.Data.SqlClient;
using static Fujitsu.eDoc.STS.ClassificationPlan.KlassifikationSystemService;

namespace Fujitsu.eDoc.STS.ClassificationPlan.Repo
{
    public class ThirdLevelRepository : Manager, IThirdLevelRepository
    {
        private static SqlConnection _connection;

        public ThirdLevelRepository()
        {
            _connection = new SqlConnection(DBConnection.GetConnectionString());
        }

        public void AddSubGroup(ParagraphTitles thirdGroup, EdocEmnePlan eDocSecondLevelEmnePlan)
        {
            // Create classcode
            SqlCommand cmdClasscode = new SqlCommand();
            cmdClasscode.CommandText = @"INSERT INTO nov_classcode 
                                                    (nov_recno, nov_lan_code, nov_code, nov_desc, nov_structureno, 
                                                    nov_allowuse, nov_secclass, nov_nop_sec, nov_nop_recno, 
                                                    nov_insertdate, nov_insertby, nov_updatedate, nov_updateby, nov_sts_kle_guid) 
                                                    VALUES(@recno, @language, @nov_code, @nov_desc, @structureno, 
                                                    @nov_allowuse, @nov_secclass, @nov_nop_sec, @nov_nop_recno,
                                                    @nov_insertdate, @nov_insertby, @nov_updatedate, @nov_updateby, @nov_sts_kle_guid)";

            cmdClasscode.Parameters.Add(new SqlParameter("@recno", 0));
            cmdClasscode.Parameters.Add(new SqlParameter("@language", "DAN"));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_code", thirdGroup.SParagraphs));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_desc", thirdGroup.Text));
            cmdClasscode.Parameters.Add(new SqlParameter("@structureno", eDocSecondLevelEmnePlan.ToSecondaryClassType + "M"));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_allowuse", thirdGroup.isExpired == true ? 0 : -1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_secclass", -1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_nop_sec", 203000));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_nop_recno", eDocSecondLevelEmnePlan.ToSecondaryClassType));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_insertdate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_insertby", 1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updatedate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updateby", 1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_sts_kle_guid", thirdGroup.UUID));


            SqlTransaction trans = null;
            try
            {
                _connection.Open();
                cmdClasscode.Connection = _connection;

                trans = _connection.BeginTransaction();
                cmdClasscode.Transaction = trans;

                int recno = base.ExecuteCreateMultiLanguage("noark classification code", cmdClasscode);
                trans.Commit();

                //mLog.LogSubGroup(pSubGroup, "INSERTED");
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

                //mLog.LogException(ex);
                throw ex;
            }
            finally
            {
                _connection.Close();
            }
        }

        public void UpdateSubGroup(ParagraphTitles thirdGroup, EdocEmnePlan eDocKLEThird)
        {
            // Update classcode
            SqlCommand cmdClasscode = new SqlCommand();
            cmdClasscode.CommandText = "UPDATE nov_classcode SET nov_code=@nov_code, nov_desc=@nov_desc, nov_allowuse=@nov_allowuse, nov_updatedate=@nov_updatedate, nov_updateby=@nov_updateby, nov_sts_kle_guid=@nov_sts_kle_guid WHERE nov_recno=@recno";
            cmdClasscode.Parameters.Add(new SqlParameter("@recno", eDocKLEThird.Recno));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_code", thirdGroup.SParagraphs));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_desc", thirdGroup.Text));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_allowuse", thirdGroup.isExpired == true ? 0 : -1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updatedate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updateby", 1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_sts_kle_guid", thirdGroup.UUID));


            SqlTransaction trans = null;
            try
            {
                _connection.Open();
                cmdClasscode.Connection = _connection;

                trans = _connection.BeginTransaction();
                cmdClasscode.Transaction = trans;

                cmdClasscode.ExecuteNonQuery();
                trans.Commit();

                //mLog.LogGroup(pGroup, "UPDATED");
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

                //mLog.LogException(ex);
                throw ex;
            }
            finally
            {
                _connection.Close();
            }
        }

    }
}
