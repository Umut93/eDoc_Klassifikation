using Fujitsu.eDoc.STS.ClassificationPlan.BLL;
using Fujitsu.eDoc.STS.ClassificationPlan.Model;
using System;
using System.Data.SqlClient;
using static Fujitsu.eDoc.STS.ClassificationPlan.KlassifikationSystemService;

namespace Fujitsu.eDoc.STS.ClassificationPlan.Repo
{
    public class SecondLevelRepository : Manager, ISecondLevelRepository
    {
        private static SqlConnection _connection;

        public SecondLevelRepository()
        {
            _connection = new SqlConnection(DBConnection.GetConnectionString());

        }
        public void AddGroupForSecondLevel(ParagraphTitles secondGroup, NoarkSubArchive noarkSub)
        {
            // Create classcode
            SqlCommand cmdClassType = new SqlCommand();
            cmdClassType.CommandText = @"INSERT INTO nop_classtype 
                                                        (nop_recno, nop_lan_code, nop_code, nop_desc, nop_label, nop_desccode, 
                                                        nop_usecaseclass, nop_usecasepart, nop_userdefine, nop_autodefine, nop_secclass, nop_fromdate, 
                                                        nop_maxlen, nop_insertdate, nop_updatedate, nop_insertby, nop_updateby) 
                                                        VALUES(@recno, @language, @nop_code, @nop_desc, @nop_label, 0, 
                                                        @nop_usecaseclass, 0, 0, 0, -1, @nop_fromdate, 
                                                        @nop_maxlen, @nop_insertdate, @nop_updatedate, @nop_insertby, @nop_updateby)";

            cmdClassType.Parameters.Add(new SqlParameter("@recno", 0));
            cmdClassType.Parameters.Add(new SqlParameter("@language", "DAN"));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_code", secondGroup.SParagraphs + ".XX"));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_desc", secondGroup.Text));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_label", "Undergruppe"));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_usecaseclass", secondGroup.isExpired == true ? 0 : -1));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_fromdate", DateTime.Now));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_maxlen", 70));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_note", "Vedligeholdes automatisk"));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_insertdate", DateTime.Now));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_insertby", 1));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_updatedate", DateTime.Now));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_updateby", 1));




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
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_code", secondGroup.SParagraphs));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_desc", secondGroup.Text));
            cmdClasscode.Parameters.Add(new SqlParameter("@structureno", ""));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_allowuse", secondGroup.isExpired == true ? 0 : -1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_secclass", -1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_nop_sec", 0));

            cmdClasscode.Parameters.Add(new SqlParameter("@nov_nop_recno", noarkSub.PrimaryClassType));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_insertdate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_insertby", 1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updatedate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updateby", 1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_sts_kle_guid", secondGroup.UUID));



            SqlTransaction trans = null;
            try
            {
                _connection.Open();
                cmdClassType.Connection = _connection;
                cmdClasscode.Connection = _connection;

                trans = _connection.BeginTransaction();
                cmdClassType.Transaction = trans;
                cmdClasscode.Transaction = trans;

                int classTypeRecno = ExecuteCreateMultiLanguage("noark classification type", cmdClassType);
                cmdClasscode.Parameters["@nov_nop_sec"].Value = classTypeRecno;
                int recno = ExecuteCreateMultiLanguage("noark classification code", cmdClasscode);
                trans.Commit();

                //mLog.LogGroup(pGroup, "INSERTED");
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


        public void UpdateGroup(ParagraphTitles secondGroup, EdocEmnePlan eDocKLESecond)
        {
            // Update class type
            SqlCommand cmdClassType = new SqlCommand();
            cmdClassType.CommandText = "UPDATE nop_classtype SET nop_code=@nop_code, nop_desc=@nop_desc, nop_updatedate=@nop_updatedate, nop_updateby=@nop_updateby, nop_usecaseclass=@nop_usecaseclass WHERE nop_recno=@recno";
            cmdClassType.Parameters.Add(new SqlParameter("@recno", eDocKLESecond.ToSecondaryClassType));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_code", secondGroup.SParagraphs + ".XX"));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_desc", secondGroup.Text));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_updatedate", DateTime.Now));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_updateby", 1));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_usecaseclass", secondGroup.isExpired == true ? 0 : -1));


            // Update classcode
            SqlCommand cmdClasscode = new SqlCommand();
            cmdClasscode.CommandText = "UPDATE nov_classcode SET nov_code=@nov_code, nov_desc=@nov_desc, nov_allowuse=@nov_allowuse, nov_updatedate=@nov_updatedate, nov_updateby=@nov_updateby, nov_sts_kle_guid=@nov_sts_kle_guid WHERE nov_recno=@recno";
            cmdClasscode.Parameters.Add(new SqlParameter("@recno", eDocKLESecond.Recno));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_code", secondGroup.SParagraphs));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_desc", secondGroup.Text));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_allowuse", secondGroup.isExpired == true ? 0 : -1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updatedate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updateby", 1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_sts_kle_guid", secondGroup.UUID));



            SqlTransaction trans = null;
            try
            {
                _connection.Open();
                cmdClassType.Connection = _connection;
                cmdClasscode.Connection = _connection;

                trans = _connection.BeginTransaction();
                cmdClassType.Transaction = trans;
                cmdClasscode.Transaction = trans;

                cmdClassType.ExecuteNonQuery();
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
