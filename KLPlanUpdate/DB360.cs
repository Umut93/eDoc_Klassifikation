using System;
using System.Data;
using System.Data.SqlClient;

namespace Fujitsu.Tools.KLPlanUpdate
{
    internal class DB360
    {
        private string mConnectionString = ""; //"Data Source=svd-sql2005;initial catalog=360_KBL1;User ID=edoc;Password=eDocPass;";
        private string[] mLanguages; // = new string[] {"DAN", "ENU", "FIN", "FRC", "FRT", "NLD", "NOR", "SVE"};
        private string mInstalledKLversion;
        private int mUserRecno = 1;
        private int mArchive;         // Archive for KL Arkiv defined in nar_archive          // = 200000;
        private int mArchivePeriod;   // Archiveperiod for KL Arkiv defined in nap_archperiod // = 200000;
        private int mFacetClassType;  // Classtype for facetter defined in nop_classtype - is 203000 in Middelfart // = 203000;
        private int mSubArchiveOffset; // Recno offset for SubArchive - in Middelfart there are testdata below 200000
        private int mClasscodeOffset;  // Recno offset for Classcodes - in Middelfart there are testdata below 200000
        private LogFile mLog;

        public DB360(string pConnectString)
        {
            mConnectionString = pConnectString;
            mUserRecno = GetCurrentUserRecno();
            mLanguages = GetLanguages();

            mArchive = Properties.Settings.Default.Archive;
            mArchivePeriod = Properties.Settings.Default.ArchivePeriod;
            mFacetClassType = Properties.Settings.Default.FacetClassType;
            mSubArchiveOffset = Properties.Settings.Default.SubArchiveOffset;
            mClasscodeOffset = Properties.Settings.Default.ClasscodeOffset;

            mInstalledKLversion = GetInstalledKLverOnTheServer();

            ValidateRecnos();

            mLog = new LogFile(mUserRecno, mArchive, mArchivePeriod, mFacetClassType);
        }

        public LogFile Log { get { return mLog; } }

        public static string[] GetDatabases(string pServer, string pUser, string pPassword, bool pUseWinAuth)
        {
            string connectionString;
            if (pUseWinAuth)
                connectionString = "Data Source=" + pServer + ";Integrated Security=SSPI;Persist Security Info=False;"; // +pUser + ";Password=" + pPassword + ";";
            else
                connectionString = "Data Source=" + pServer + ";initial catalog=master;User ID=" + pUser + ";Password=" + pPassword + ";";
            string sqlSelect = "SELECT name FROM sys.databases ORDER BY name";

            SqlDataAdapter da = new SqlDataAdapter(sqlSelect, connectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);

            string[] databases = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                databases[i] = dt.Rows[i][0].ToString();
            }

            return databases;
        }

        private int GetCurrentUserRecno()
        {
            string userAccount = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string sqlSelect = "SELECT us_recno FROM us_user WHERE us_id='" + userAccount + "'";

            SqlConnection con = new SqlConnection(mConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlSelect, con);

            object userRecno = cmd.ExecuteScalar();
            con.Close();
            if (userRecno == null)
                throw new ApplicationException("Du er ikke oprettet som bruger i eDoc");

            return (int)userRecno;
        }

        private string[] GetLanguages()
        {
            string sqlSelect = "SELECT lan_code FROM lan_language";

            SqlDataAdapter da = new SqlDataAdapter(sqlSelect, mConnectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);

            string[] languages = new string[dt.Rows.Count];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                languages[i] = dt.Rows[i][0].ToString();
            }

            return languages;
        }

        private void ValidateRecnos()
        {
            string sqlSelect = "select * from nar_archive where nar_recno=" + mArchive.ToString();

            SqlDataAdapter da = new SqlDataAdapter(sqlSelect, mConnectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
                throw new ApplicationException("Archive med recno=" + mArchive.ToString() + " findes ikke i databasen");

            sqlSelect = "select * from nap_archperiod where nap_recno=" + mArchivePeriod.ToString();
            da = new SqlDataAdapter(sqlSelect, mConnectionString);
            dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
                throw new ApplicationException("ArchivePeriod med recno=" + mArchivePeriod.ToString() + " findes ikke i databasen");

            sqlSelect = "select * from nop_classtype where nop_recno=" + mFacetClassType.ToString();
            da = new SqlDataAdapter(sqlSelect, mConnectionString);
            dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count == 0)
                throw new ApplicationException("Classtype med recno=" + mFacetClassType.ToString() + " findes ikke i databasen");
        }

        public string GetInstalledKLverOnTheServer()
        {
            string sqlSelect = "SELECT isnull(nar_note,'') + ' XML-filer version: ' + CASE WHEN ISNULL(nar_releasedate,'') <> '' THEN CONVERT(VARCHAR(10),nar_releasedate,120) ELSE 'Ukendt udgivelsesdato' END FROM nar_archive where nar_lan_code='DAN' and nar_recno=" + mArchive.ToString();

            SqlConnection con = new SqlConnection(mConnectionString);
            con.Open();
            SqlCommand cmd = new SqlCommand(sqlSelect, con);

            object InstalledKLversion = cmd.ExecuteScalar();
            con.Close();

            return InstalledKLversion.ToString();
        }

        public void UpdateKLverOnTheServer(string udgivelsesDato)
        {
            SqlCommand sqlUpdate = new SqlCommand();
            sqlUpdate.CommandText = "update nar_archive set nar_releasedate=@udgivelsesDato where nar_lan_code='DAN' and nar_recno=" + mArchive.ToString();
            sqlUpdate.Parameters.Add(new SqlParameter("@udgivelsesDato", udgivelsesDato));

            SqlConnection con = new SqlConnection(mConnectionString);
            SqlTransaction trans = null;
            try
            {
                con.Open();
                sqlUpdate.Connection = con;

                trans = con.BeginTransaction();
                sqlUpdate.Transaction = trans;

                sqlUpdate.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

                mLog.LogException(ex);
                throw ex;
            }
            finally
            {
                con.Close();
            }

        }

        public void CreateMainGroup(MainGroup pMainGroup)
        {
            string codeAndDescription = pMainGroup.HovedgruppeNr + " " + pMainGroup.HovedgruppeTitel;

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
            cmdClassType.Parameters.Add(new SqlParameter("@nop_code", pMainGroup.HovedgruppeNr + ".XX"));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_desc", pMainGroup.HovedgruppeTitel));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_label", "Gruppe"));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_usecaseclass", pMainGroup.IsExpired ? 0 : -1));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_fromdate", pMainGroup.Oprettet));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_maxlen", 70));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_note", "Vedligeholdes automatisk"));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_insertdate", DateTime.Now));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_insertby", mUserRecno));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_updatedate", DateTime.Now));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_updateby", mUserRecno));

            // Create subarchive
            SqlCommand cmdSubArchive = new SqlCommand();
            cmdSubArchive.CommandText = @"INSERT INTO nad_subarchive 
        (nad_recno, nad_lan_code, nad_code, nad_desc, nad_nar_recno, nad_nap_recno, nad_nas_recno, 
        nad_note, nad_denycase, nad_denydoc, nad_closed, nad_paperdoc, nad_elecdoc, nad_nop_prim, 
        nad_indsecclass, nad_insertdate, nad_updatedate, nad_insertby, nad_updateby, nad_expired) 
        VALUES(@recno, @language, @nad_code, @nad_desc, @nad_nar_recno, @nad_nap_recno, @nad_nas_recno, 
        @nad_note, @nad_denycase, @nad_denydoc, @nad_closed, 0, 0, @nad_nop_prim, 
        0, @nad_insertdate, @nad_updatedate, @nad_insertby, @nad_updateby, @nad_expired)";

            cmdSubArchive.Parameters.Add(new SqlParameter("@recno", 0));
            cmdSubArchive.Parameters.Add(new SqlParameter("@language", "DAN"));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_code", (codeAndDescription).Substring(0, codeAndDescription.Length > 40 ? 40 : codeAndDescription.Length)));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_desc", codeAndDescription));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_nar_recno", mArchive));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_nap_recno", mArchivePeriod));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_nas_recno", 1));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_note", "Vedligeholdes automatisk"));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_denycase", false));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_denydoc", false));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_closed", false));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_nop_prim", 0));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_insertdate", DateTime.Now));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_insertby", mUserRecno));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_updatedate", DateTime.Now));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_updateby", mUserRecno));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_expired", pMainGroup.IsExpired));

            SqlConnection con = new SqlConnection(mConnectionString);
            SqlTransaction trans = null;
            try
            {
                con.Open();
                cmdClassType.Connection = con;
                cmdSubArchive.Connection = con;

                trans = con.BeginTransaction();
                cmdClassType.Transaction = trans;
                cmdSubArchive.Transaction = trans;

                int classTypeRecno = ExecuteCreateMultiLanguage("noark classification type", cmdClassType);
                cmdSubArchive.Parameters["@nad_nop_prim"].Value = classTypeRecno;
                int recno = ExecuteCreateMultiLanguage("noark subarchive", cmdSubArchive);
                trans.Commit();

                pMainGroup.Recno = recno;
                pMainGroup.ClassTypeRecno = classTypeRecno;

                mLog.LogMainGroup(pMainGroup, "INSERTED");
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

                mLog.LogException(ex);
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void UpdateMainGroup(MainGroup pMainGroup)
        {
            string codeAndDescription = pMainGroup.HovedgruppeNr + " " + pMainGroup.HovedgruppeTitel;

            // Update class type
            SqlCommand cmdClassType = new SqlCommand();
            cmdClassType.CommandText = "UPDATE nop_classtype SET nop_code=@nop_code, nop_desc=@nop_desc, nop_updatedate=@nop_updatedate, nop_updateby=@nop_updateby WHERE nop_recno=@recno";
            cmdClassType.Parameters.Add(new SqlParameter("@recno", pMainGroup.ClassTypeRecno));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_code", pMainGroup.HovedgruppeNr));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_desc", pMainGroup.HovedgruppeTitel));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_updatedate", DateTime.Now));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_updateby", mUserRecno));


            // Update subarchive
            SqlCommand cmdSubArchive = new SqlCommand();
            cmdSubArchive.CommandText = "UPDATE nad_subarchive SET nad_code=@nad_code, nad_desc=@nad_desc, nad_updatedate=@nad_updatedate, nad_updateby=@nad_updateby, nad_expired=@nad_expired WHERE nad_recno=@recno";
            cmdSubArchive.Parameters.Add(new SqlParameter("@recno", pMainGroup.Recno));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_code", (codeAndDescription).Substring(0, codeAndDescription.Length > 40 ? 40 : codeAndDescription.Length)));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_desc", codeAndDescription));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_updatedate", DateTime.Now));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_updateby", mUserRecno));
            cmdSubArchive.Parameters.Add(new SqlParameter("@nad_expired", pMainGroup.IsExpired));


            SqlConnection con = new SqlConnection(mConnectionString);
            SqlTransaction trans = null;
            try
            {
                con.Open();
                cmdClassType.Connection = con;
                cmdSubArchive.Connection = con;

                trans = con.BeginTransaction();
                cmdClassType.Transaction = trans;
                cmdSubArchive.Transaction = trans;

                cmdClassType.ExecuteNonQuery();
                cmdSubArchive.ExecuteNonQuery();
                trans.Commit();

                mLog.LogMainGroup(pMainGroup, "UPDATED");
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

                mLog.LogException(ex);
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void CreateGroup(Group pGroup)
        {
            int parentClasstype = ((MainGroup)pGroup.Parent).ClassTypeRecno;

            // Create class type
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
            cmdClassType.Parameters.Add(new SqlParameter("@nop_code", pGroup.GruppeNr + ".XX"));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_desc", pGroup.GruppeTitel));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_label", "Undergruppe"));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_usecaseclass", pGroup.IsExpired ? 0 : -1));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_fromdate", pGroup.Oprettet));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_maxlen", 70));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_note", "Vedligeholdes automatisk"));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_insertdate", DateTime.Now));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_insertby", mUserRecno));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_updatedate", DateTime.Now));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_updateby", mUserRecno));


            // Create classcode
            SqlCommand cmdClasscode = new SqlCommand();
            cmdClasscode.CommandText = @"INSERT INTO nov_classcode 
        (nov_recno, nov_lan_code, nov_code, nov_desc, nov_structureno, 
        nov_allowuse, nov_secclass, nov_nop_sec, nov_nop_recno, 
        nov_insertdate, nov_insertby, nov_updatedate, nov_updateby) 
        VALUES(@recno, @language, @nov_code, @nov_desc, @structureno, 
        @nov_allowuse, @nov_secclass, @nov_nop_sec, @nov_nop_recno,
        @nov_insertdate, @nov_insertby, @nov_updatedate, @nov_updateby)";

            cmdClasscode.Parameters.Add(new SqlParameter("@recno", 0));
            cmdClasscode.Parameters.Add(new SqlParameter("@language", "DAN"));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_code", pGroup.GruppeNr));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_desc", pGroup.GruppeTitel));
            cmdClasscode.Parameters.Add(new SqlParameter("@structureno", ""));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_allowuse", pGroup.IsExpired ? 0 : -1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_secclass", -1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_nop_sec", 0));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_nop_recno", parentClasstype));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_insertdate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_insertby", mUserRecno));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updatedate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updateby", mUserRecno));


            SqlConnection con = new SqlConnection(mConnectionString);
            SqlTransaction trans = null;
            try
            {
                con.Open();
                cmdClassType.Connection = con;
                cmdClasscode.Connection = con;

                trans = con.BeginTransaction();
                cmdClassType.Transaction = trans;
                cmdClasscode.Transaction = trans;

                int classTypeRecno = ExecuteCreateMultiLanguage("noark classification type", cmdClassType);
                cmdClasscode.Parameters["@nov_nop_sec"].Value = classTypeRecno;
                int recno = ExecuteCreateMultiLanguage("noark classification code", cmdClasscode);
                trans.Commit();

                pGroup.Recno = recno;
                pGroup.PrimaryClassTypeRecno = parentClasstype;
                pGroup.SecondaryClassTypeRecno = classTypeRecno;

                mLog.LogGroup(pGroup, "INSERTED");
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

                mLog.LogException(ex);
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void UpdateGroup(Group pGroup)
        {
            // Update class type
            SqlCommand cmdClassType = new SqlCommand();
            cmdClassType.CommandText = "UPDATE nop_classtype SET nop_code=@nop_code, nop_desc=@nop_desc, nop_updatedate=@nop_updatedate, nop_updateby=@nop_updateby WHERE nop_recno=@recno";
            cmdClassType.Parameters.Add(new SqlParameter("@recno", pGroup.SecondaryClassTypeRecno));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_code", pGroup.GruppeNr));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_desc", pGroup.GruppeTitel));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_updatedate", DateTime.Now));
            cmdClassType.Parameters.Add(new SqlParameter("@nop_updateby", mUserRecno));

            // Update classcode
            SqlCommand cmdClasscode = new SqlCommand();
            cmdClasscode.CommandText = "UPDATE nov_classcode SET nov_code=@nov_code, nov_desc=@nov_desc, nov_allowuse=@nov_allowuse, nov_updatedate=@nov_updatedate, nov_updateby=@nov_updateby WHERE nov_recno=@recno";
            cmdClasscode.Parameters.Add(new SqlParameter("@recno", pGroup.Recno));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_code", pGroup.GruppeNr));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_desc", pGroup.GruppeTitel));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_allowuse", pGroup.IsExpired ? 0 : -1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updatedate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updateby", mUserRecno));

            SqlConnection con = new SqlConnection(mConnectionString);
            SqlTransaction trans = null;
            try
            {
                con.Open();
                cmdClassType.Connection = con;
                cmdClasscode.Connection = con;

                trans = con.BeginTransaction();
                cmdClassType.Transaction = trans;
                cmdClasscode.Transaction = trans;

                cmdClassType.ExecuteNonQuery();
                cmdClasscode.ExecuteNonQuery();
                trans.Commit();

                mLog.LogGroup(pGroup, "UPDATED");
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

                mLog.LogException(ex);
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void CreateSubGroup(SubGroup pSubGroup)
        {
            int parentClasstype = ((Group)pSubGroup.Parent).SecondaryClassTypeRecno;

            // Create classcode
            SqlCommand cmdClasscode = new SqlCommand();
            cmdClasscode.CommandText = @"INSERT INTO nov_classcode 
        (nov_recno, nov_lan_code, nov_code, nov_desc, nov_structureno, 
        nov_allowuse, nov_secclass, nov_nop_sec, nov_nop_recno, nov_scv_nkk, nov_preserve,
        nov_insertdate, nov_insertby, nov_updatedate, nov_updateby) 
        VALUES(@recno, @language, @nov_code, @nov_desc, @structureno, 
        @nov_allowuse, @nov_secclass, @nov_nop_sec, @nov_nop_recno, @nov_scv_nkk, @nov_preserve,
        @nov_insertdate, @nov_insertby, @nov_updatedate, @nov_updateby)";

            cmdClasscode.Parameters.Add(new SqlParameter("@recno", 0));
            cmdClasscode.Parameters.Add(new SqlParameter("@language", "DAN"));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_code", pSubGroup.EmneNr));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_desc", pSubGroup.EmneTekst));
            cmdClasscode.Parameters.Add(new SqlParameter("@structureno", parentClasstype.ToString() + "M"));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_allowuse", pSubGroup.IsExpired ? 0 : -1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_secclass", -1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_nop_sec", mFacetClassType));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_nop_recno", parentClasstype));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_scv_nkk", pSubGroup.ScrapCode));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_preserve", pSubGroup.PreserveYears));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_insertdate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_insertby", mUserRecno));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updatedate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updateby", mUserRecno));


            SqlConnection con = new SqlConnection(mConnectionString);
            SqlTransaction trans = null;
            try
            {
                con.Open();
                cmdClasscode.Connection = con;

                trans = con.BeginTransaction();
                cmdClasscode.Transaction = trans;

                int recno = ExecuteCreateMultiLanguage("noark classification code", cmdClasscode);
                trans.Commit();

                pSubGroup.Recno = recno;

                mLog.LogSubGroup(pSubGroup, "INSERTED");
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

                mLog.LogException(ex);
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void UpdateSubGroup(SubGroup pSubGroup)
        {
            // Update classcode
            SqlCommand cmdClasscode = new SqlCommand();
            cmdClasscode.CommandText = @"UPDATE nov_classcode SET nov_code=@nov_code, nov_desc=@nov_desc, nov_allowuse=@nov_allowuse, 
        nov_scv_nkk=@nov_scv_nkk, nov_preserve=@nov_preserve, nov_updatedate=@nov_updatedate, nov_updateby=@nov_updateby WHERE nov_recno=@recno";
            cmdClasscode.Parameters.Add(new SqlParameter("@recno", pSubGroup.Recno));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_code", pSubGroup.EmneNr));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_desc", pSubGroup.EmneTekst));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_allowuse", pSubGroup.IsExpired ? 0 : -1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_scv_nkk", pSubGroup.ScrapCode));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_preserve", pSubGroup.PreserveYears));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updatedate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updateby", mUserRecno));

            SqlConnection con = new SqlConnection(mConnectionString);
            SqlTransaction trans = null;
            try
            {
                con.Open();
                cmdClasscode.Connection = con;

                trans = con.BeginTransaction();
                cmdClasscode.Transaction = trans;

                cmdClasscode.ExecuteNonQuery();
                trans.Commit();

                mLog.LogSubGroup(pSubGroup, "UPDATED");
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

                mLog.LogException(ex);
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void CreateFacetCategory(FacetCategory pFacetCategory)
        {
            // Create classcode
            SqlCommand cmdClasscode = new SqlCommand();
            cmdClasscode.CommandText = @"INSERT INTO nov_classcode 
        (nov_recno, nov_lan_code, nov_code, nov_desc, nov_structureno, nov_allowuse, 
        nov_secclass, nov_nop_recno, nov_insertdate, nov_insertby, nov_updatedate, nov_updateby) 
        VALUES(@recno, @language, @nov_code, @nov_desc, @structureno, 0, 
        @nov_secclass, @nov_nop_recno, @nov_insertdate, @nov_insertby, @nov_updatedate, @nov_updateby)";

            cmdClasscode.Parameters.Add(new SqlParameter("@recno", 0));
            cmdClasscode.Parameters.Add(new SqlParameter("@language", "DAN"));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_code", pFacetCategory.HandlingsfacetKategoriKode));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_desc", pFacetCategory.HandlingsfacetKategoriTekst));
            cmdClasscode.Parameters.Add(new SqlParameter("@structureno", ""));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_secclass", -1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_nop_recno", mFacetClassType));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_insertdate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_insertby", mUserRecno));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updatedate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updateby", mUserRecno));


            SqlConnection con = new SqlConnection(mConnectionString);
            SqlTransaction trans = null;
            try
            {
                con.Open();
                cmdClasscode.Connection = con;

                trans = con.BeginTransaction();
                cmdClasscode.Transaction = trans;

                int recno = ExecuteCreateMultiLanguage("noark classification code", cmdClasscode);
                trans.Commit();

                pFacetCategory.Recno = recno;

                mLog.LogFacetCategory(pFacetCategory, "INSERTED");
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

                mLog.LogException(ex);
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void UpdateFacetCategory(FacetCategory pFacetCategory)
        {
            // Update classcode
            SqlCommand cmdClasscode = new SqlCommand();
            cmdClasscode.CommandText = "UPDATE nov_classcode SET nov_code=@nov_code, nov_desc=@nov_desc, nov_updatedate=@nov_updatedate, nov_updateby=@nov_updateby WHERE nov_recno=@recno";
            cmdClasscode.Parameters.Add(new SqlParameter("@recno", pFacetCategory.Recno));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_code", pFacetCategory.HandlingsfacetKategoriKode));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_desc", pFacetCategory.HandlingsfacetKategoriTekst));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updatedate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updateby", mUserRecno));

            SqlConnection con = new SqlConnection(mConnectionString);
            SqlTransaction trans = null;
            try
            {
                con.Open();
                cmdClasscode.Connection = con;

                trans = con.BeginTransaction();
                cmdClasscode.Transaction = trans;

                cmdClasscode.ExecuteNonQuery();
                trans.Commit();

                mLog.LogFacetCategory(pFacetCategory, "UPDATED");
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

                mLog.LogException(ex);
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void CreateFacet(Facet pFacet)
        {
            int parentRecno = ((FacetCategory)pFacet.Parent).Recno;

            // Create classcode
            SqlCommand cmdClasscode = new SqlCommand();
            cmdClasscode.CommandText = @"INSERT INTO nov_classcode 
        (nov_recno, nov_lan_code, nov_code, nov_desc, nov_structureno, 
        nov_allowuse, nov_secclass, nov_nop_recno, nov_scv_nkk, nov_preserve,
        nov_insertdate, nov_insertby, nov_updatedate, nov_updateby) 
        VALUES(@recno, @language, @nov_code, @nov_desc, @structureno, 
        @nov_allowuse, @nov_secclass, @nov_nop_recno, @nov_scv_nkk, @nov_preserve,
        @nov_insertdate, @nov_insertby, @nov_updatedate, @nov_updateby)";
            cmdClasscode.Parameters.Add(new SqlParameter("@recno", 0));
            cmdClasscode.Parameters.Add(new SqlParameter("@language", "DAN"));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_code", pFacet.HandlingsfacetKode));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_desc", pFacet.HandlingsfacetTekst));
            cmdClasscode.Parameters.Add(new SqlParameter("@structureno", parentRecno.ToString() + "M"));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_allowuse", pFacet.IsExpired ? 0 : -1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_secclass", -1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_nop_recno", mFacetClassType));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_scv_nkk", pFacet.ScrapCode));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_preserve", pFacet.PreserveYears));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_insertdate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_insertby", mUserRecno));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updatedate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updateby", mUserRecno));


            SqlConnection con = new SqlConnection(mConnectionString);
            SqlTransaction trans = null;
            try
            {
                con.Open();
                cmdClasscode.Connection = con;

                trans = con.BeginTransaction();
                cmdClasscode.Transaction = trans;

                int recno = ExecuteCreateMultiLanguage("noark classification code", cmdClasscode);
                trans.Commit();

                pFacet.Recno = recno;

                mLog.LogFacet(pFacet, "INSERTED");
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

                mLog.LogException(ex);
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void UpdateFacet(Facet pFacet)
        {
            // Update classcode
            SqlCommand cmdClasscode = new SqlCommand();
            cmdClasscode.CommandText = @"UPDATE nov_classcode SET nov_code=@nov_code, nov_desc=@nov_desc, nov_allowuse=@nov_allowuse, 
        nov_scv_nkk=@nov_scv_nkk, nov_preserve=@nov_preserve, nov_updatedate=@nov_updatedate, nov_updateby=@nov_updateby WHERE nov_recno=@recno";
            cmdClasscode.Parameters.Add(new SqlParameter("@recno", pFacet.Recno));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_code", pFacet.HandlingsfacetKode));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_desc", pFacet.HandlingsfacetTekst));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_allowuse", pFacet.IsExpired ? 0 : -1));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_scv_nkk", pFacet.ScrapCode));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_preserve", pFacet.PreserveYears));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updatedate", DateTime.Now));
            cmdClasscode.Parameters.Add(new SqlParameter("@nov_updateby", mUserRecno));

            SqlConnection con = new SqlConnection(mConnectionString);
            SqlTransaction trans = null;
            try
            {
                con.Open();
                cmdClasscode.Connection = con;

                trans = con.BeginTransaction();
                cmdClasscode.Transaction = trans;

                cmdClasscode.ExecuteNonQuery();
                trans.Commit();

                mLog.LogFacet(pFacet, "UPDATED");
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

                mLog.LogException(ex);
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void RemoveAllStikord()
        {
            SqlCommand cmdCodeSubject = new SqlCommand();
            cmdCodeSubject.CommandText = "DELETE FROM fu_csi_codesbjidx";

            SqlConnection con = new SqlConnection(mConnectionString);
            SqlTransaction trans = null;
            try
            {
                con.Open();
                cmdCodeSubject.Connection = con;

                trans = con.BeginTransaction();
                cmdCodeSubject.Transaction = trans;

                cmdCodeSubject.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

                mLog.LogException(ex);
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void CreateStikord(Subject pSubject)
        {
            // Create codesubject
            SqlCommand cmdCodeSubject = new SqlCommand();
            cmdCodeSubject.CommandText = "INSERT INTO fu_csi_codesbjidx (fu_csi_recno, fu_csi_desc, fu_csi_code, fu_csi_facet, fu_csi_Expired, fu_insertdate, fu_updatedate, fu_insertby, fu_updateby) VALUES(@recno, @fu_csi_desc, @fu_csi_code, @fu_csi_facet, @fu_csi_Expired, @fu_insertdate, @fu_updatedate, @fu_insertby, @fu_updateby)";
            cmdCodeSubject.Parameters.Add(new SqlParameter("@recno", 0));

            cmdCodeSubject.Parameters.Add(new SqlParameter("@fu_csi_desc", pSubject.Description));
            cmdCodeSubject.Parameters.Add(new SqlParameter("@fu_csi_code", pSubject.EmneNr));
            cmdCodeSubject.Parameters.Add(new SqlParameter("@fu_csi_facet", pSubject.Facet));
            cmdCodeSubject.Parameters.Add(new SqlParameter("@fu_csi_Expired", pSubject.IsExpired ? -1 : 0));
            cmdCodeSubject.Parameters.Add(new SqlParameter("@fu_insertdate", DateTime.Now));
            cmdCodeSubject.Parameters.Add(new SqlParameter("@fu_insertby", mUserRecno));
            cmdCodeSubject.Parameters.Add(new SqlParameter("@fu_updatedate", DateTime.Now));
            cmdCodeSubject.Parameters.Add(new SqlParameter("@fu_updateby", mUserRecno));

            if (pSubject.EmneNr == null) cmdCodeSubject.Parameters["@fu_csi_code"].Value = DBNull.Value;
            if (pSubject.Facet == null) cmdCodeSubject.Parameters["@fu_csi_facet"].Value = DBNull.Value;

            SqlConnection con = new SqlConnection(mConnectionString);
            SqlTransaction trans = null;
            try
            {
                con.Open();
                cmdCodeSubject.Connection = con;

                trans = con.BeginTransaction();
                cmdCodeSubject.Transaction = trans;

                int recno = GetRecno("classification code index", cmdCodeSubject.Connection, cmdCodeSubject.Transaction);
                cmdCodeSubject.Parameters["@recno"].Value = recno;

                cmdCodeSubject.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

                mLog.LogException(ex);
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        public void UpdateStikordReferences()
        {
            SqlCommand cmdCodeSubject = new SqlCommand();
            cmdCodeSubject.CommandText = @"update fu_csi_codesbjidx set fu_csi_nov_coderecno=nov_recno from nov_classcode where nov_code=fu_csi_code and nov_classcode.nov_allowuse=-1 and nov_nop_recno>=200000;
                                     update fu_csi_codesbjidx set fu_csi_nov_facetrecno=nov_recno from nov_classcode where nov_code=fu_csi_facet and nov_classcode.nov_allowuse=-1";

            SqlConnection con = new SqlConnection(mConnectionString);
            SqlTransaction trans = null;
            try
            {
                con.Open();
                cmdCodeSubject.Connection = con;

                trans = con.BeginTransaction();
                cmdCodeSubject.Transaction = trans;

                cmdCodeSubject.ExecuteNonQuery();
                trans.Commit();
            }
            catch (Exception ex)
            {
                if (trans != null)
                    trans.Rollback();

                mLog.LogException(ex);
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }

        private int ExecuteCreateMultiLanguage(string pEntity, SqlCommand pCommand)
        {

            int recno = GetRecno(pEntity, pCommand.Connection, pCommand.Transaction);
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

        private void ExecuteUpdateMultiLanguage(string pEntity, SqlCommand pCommand)
        {
            for (int i = 0; i < mLanguages.Length; i++)
            {
                pCommand.Parameters["@language"].Value = mLanguages[i];
                pCommand.ExecuteNonQuery();
            }
        }

        private int GetRecno(string pEntity, SqlConnection pConnection, SqlTransaction pTransaction)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE rec_newrecno SET rec_lastrecno = rec_lastrecno + 1 WHERE rec_entity=@rec_entity; SELECT rec_lastrecno FROM rec_newrecno WHERE rec_entity=@rec_entity;";
            cmd.Parameters.Add(new SqlParameter("@rec_entity", pEntity));

            cmd.Connection = pConnection;
            cmd.Transaction = pTransaction;
            int recno = (int)cmd.ExecuteScalar();
            return recno;
        }

        public NoarkSubarchiveList GetNoarkSubarchives()
        {
            NoarkSubarchiveList archives = new NoarkSubarchiveList();

            string selectCmd = "select X1.nad_recno, X1.nad_code, X1.nad_desc, X1.nad_nop_prim, X1.nad_expired from dbo.nad_subarchive X1 where X1.nad_lan_code  = N'DAN' and X1.nad_recno>=" + mSubArchiveOffset.ToString();
            SqlDataAdapter da = new SqlDataAdapter(selectCmd, mConnectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                archives.Add(new NoarkSubarchive(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), (int)dr[4] == 0 ? false : true));
            }

            return archives;
        }

        public NoarkClasscodeList GetNoarkClasscodes()
        {
            NoarkClasscodeList classcodes = new NoarkClasscodeList();

            string selectCmd = "select X1.nov_recno, X1.nov_code, X1.nov_desc, X1.nov_nop_recno, X1.nov_nop_sec, X1.nov_allowuse, X1.nov_scv_nkk from dbo.nov_classcode X1 where X1.nov_lan_code  = N'DAN' and X1.nov_recno>=" + mClasscodeOffset.ToString();
            SqlDataAdapter da = new SqlDataAdapter(selectCmd, mConnectionString);
            DataTable dt = new DataTable();
            da.Fill(dt);

            foreach (DataRow dr in dt.Rows)
            {
                classcodes.Add(new NoarkClasscode(dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), (int)dr[5] == 0 ? true : false, dr[6].ToString()));
            }

            return classcodes;
        }
    }
}
