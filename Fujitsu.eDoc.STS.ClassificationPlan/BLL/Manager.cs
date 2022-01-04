using System.Data.SqlClient;

namespace Fujitsu.eDoc.STS.ClassificationPlan.BLL
{
    public class Manager
    {
        protected int ExecuteCreateMultiLanguage(string pEntity, SqlCommand pCommand)
        {

            int recno = GetRecno(pEntity, pCommand.Connection, pCommand.Transaction);
            string[] mLanguages = new string[12] { "ARA", "DAN", "DEU", "ENU", "ESN", "FIN", "FRA", "FRT", "ITA", "NLD", "NOR", "SVE" };

            pCommand.Parameters["@recno"].Value = recno;
            if (pCommand.Parameters.IndexOf("@structureno") >= 0)
            {
                pCommand.Parameters["@structureno"].Value = pCommand.Parameters["@structureno"].Value + recno.ToString() + "M";
            }
            for (int i = 0; i < mLanguages.Length; i++)
            {
                pCommand.Parameters["@language"].Value = mLanguages[i];
                pCommand.ExecuteNonQuery();
            }

            return recno;
        }

        protected int GetRecno(string pEntity, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE rec_newrecno SET rec_lastrecno = rec_lastrecno + 1 WHERE rec_entity=@rec_entity; SELECT rec_lastrecno FROM rec_newrecno WHERE rec_entity=@rec_entity;";
            cmd.Parameters.Add(new SqlParameter("@rec_entity", pEntity));

            cmd.Connection = pConnection;
            cmd.Transaction = pTransaction;
            int recno = (int)cmd.ExecuteScalar();
            return recno;
        }
    }
}
