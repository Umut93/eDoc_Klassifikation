using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Fujitsu.eDoc.STS.ClassificationPlan.BLL;
using Fujitsu.eDoc.STS.ClassificationPlan.Model;
using static Fujitsu.eDoc.STS.ClassificationPlan.KlassifikationSystemService;

namespace Fujitsu.eDoc.STS.ClassificationPlan.Repo
{
    public class MainGroupRepository : Manager, IMainGroupRepository
    {
        private static SqlConnection _connection;

        public MainGroupRepository()
        {
            _connection = new SqlConnection(DBConnection.GetConnectionString());
        }

        public void AddMainGroup(ParagraphTitles mainGroup)
        {
            string codeAndDescription = mainGroup.SParagraphs + " " + mainGroup.Text;

            // Create class type
            SqlCommand cmdClassType = new SqlCommand();
            cmdClassType.CommandText = @"INSERT INTO nop_classtype 
                                            (nop_recno, nop_lan_code, nop_code, nop_desc, nop_label, nop_desccode,
                                            nop_usecaseclass, nop_usecasepart, nop_userdefine, nop_autodefine, nop_secclass, nop_fromdate, 
                                            nop_maxlen, nop_note, nop_insertdate, nop_updatedate, nop_insertby, nop_updateby) 
                                            VALUES(@recno, @language, @nop_code, @nop_desc, @nop_label, 0,
                                            @nop_usecaseclass, 0, 0, 0, -1, @nop_fromdate, 
                                            @nop_maxlen, @nop_note, @nop_insertdate, @nop_updatedate, @nop_insertby, @nop_updateby)";

            cmdClassType.Parameters.Add(new SqlParameter("@recno", 0));
            cmdClassType.Parameters.Add(new SqlParameter("@language", "DAN"));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_code", mainGroup.SParagraphs + ".XX"));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_desc", mainGroup.Text));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_label", "Gruppe"));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_usecaseclass", mainGroup.isExpired == true ? 0 : -1));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_fromdate", DateTime.Now));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_maxlen", 70));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_note", "Vedligeholdes automatisk"));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_insertdate", DateTime.Now));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_insertby", 1));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_updatedate", DateTime.Now));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_updateby", 1));

            // Create subarchive
            SqlCommand cmdSubArchive = new SqlCommand();
            cmdSubArchive.CommandText = @"INSERT INTO nad_subarchive 
                                            (nad_recno, nad_lan_code, nad_code, nad_desc, nad_nar_recno, nad_nap_recno, nad_nas_recno, 
                                            nad_note, nad_denycase, nad_denydoc, nad_closed, nad_paperdoc, nad_elecdoc, nad_nop_prim, 
                                            nad_indsecclass, nad_insertdate, nad_updatedate, nad_insertby, nad_updateby, nad_expired, nad_sts_kle_guid) 
                                            VALUES(@recno, @language, @nad_code, @nad_desc, @nad_nar_recno, @nad_nap_recno, @nad_nas_recno, 
                                            @nad_note, @nad_denycase, @nad_denydoc, @nad_closed, 0, 0, @nad_nop_prim, 
                                            0, @nad_insertdate, @nad_updatedate, @nad_insertby, @nad_updateby, @nad_expired, @nad_sts_kle_guid)";

            cmdSubArchive.Parameters.Add(new SqlParameter("@recno", 0));
            cmdSubArchive.Parameters.Add(new SqlParameter("@language", "DAN"));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_code", (codeAndDescription).Substring(0, codeAndDescription.Length > 40 ? 40 : codeAndDescription.Length)));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_desc", codeAndDescription));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_nar_recno", 200000));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_nap_recno", 200000));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_nas_recno", 1));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_note", "Vedligeholdes automatisk"));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_denycase", false));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_denydoc", false));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_closed", false));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_nop_prim", 0));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_insertdate", DateTime.Now));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_insertby", 1));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_updatedate", DateTime.Now));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_updateby", 1));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_expired", mainGroup.isExpired == true ? 1 : 0));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_sts_kle_guid", mainGroup.UUID));


            SqlTransaction trans = null;
            try
            {
                _connection.Open();
                cmdClassType.Connection = _connection;
                cmdSubArchive.Connection = _connection;

                trans = _connection.BeginTransaction();
                cmdClassType.Transaction = trans;
                cmdSubArchive.Transaction = trans;

                int classTypeRecno = base.ExecuteCreateMultiLanguage("noark classification type", cmdClassType);
                cmdSubArchive.Parameters["@nad_nop_prim"].Value = classTypeRecno;
                int recno = ExecuteCreateMultiLanguage("noark subarchive", cmdSubArchive);
                trans.Commit();

                //mLog.LogMainGroup(pMainGroup, "INSERTED");
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

        public void UpdateMainGroup(KlassifikationSystemService.ParagraphTitles mainGroup, NoarkSubArchive noarkSubArch, string codeAndDescription)
        {
            // Update class type
            SqlCommand cmdClassType = new SqlCommand();
            cmdClassType.CommandText = "UPDATE nop_classtype SET nop_code=@nop_code, nop_desc=@nop_desc, nop_updatedate=@nop_updatedate, nop_updateby=@nop_updateby WHERE nop_recno=@recno";
            cmdClassType.Parameters.Add(new SqlParameter("@recno", noarkSubArch.PrimaryClassType));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_code", mainGroup.SParagraphs + ".XX"));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_desc", mainGroup.Text));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_updatedate", DateTime.Now));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_updateby", 1));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_usecaseclass", mainGroup.isExpired == true ? 0 : 1));


            // Update subarchive
            SqlCommand cmdSubArchive = new SqlCommand();
            cmdSubArchive.CommandText = "UPDATE nad_subarchive SET nad_code=@nad_code, nad_desc=@nad_desc, nad_updatedate=@nad_updatedate, nad_updateby=@nad_updateby, nad_expired=@nad_expired, nad_sts_kle_guid=@nad_sts_kle_guid WHERE nad_recno=@recno";
            cmdSubArchive.Parameters.Add(new SqlParameter("@recno", noarkSubArch.Recno));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_code", (codeAndDescription).Substring(0, codeAndDescription.Length > 40 ? 40 : codeAndDescription.Length)));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_desc", codeAndDescription));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_updatedate", DateTime.Now));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_updateby", 1));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_expired", mainGroup.isExpired == true ? 1 : 0));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_sts_kle_guid", mainGroup.UUID));



            SqlTransaction trans = null;
            try
            {
                _connection.Open();
                cmdClassType.Connection = _connection;
                cmdSubArchive.Connection = _connection;

                trans = _connection.BeginTransaction();
                cmdClassType.Transaction = trans;
                cmdSubArchive.Transaction = trans;

                cmdClassType.ExecuteNonQuery();
                cmdSubArchive.ExecuteNonQuery();
                trans.Commit();

                //mLog.LogMainGroup(pMainGroup, "UPDATED");
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
