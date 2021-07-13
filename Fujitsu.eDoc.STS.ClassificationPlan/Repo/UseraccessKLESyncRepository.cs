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
    public class UseraccessKLESyncRepository : Manager
    {
        private static SqlConnection _connection;

        public UseraccessKLESyncRepository()
        {
            _connection = new SqlConnection(DBConnection.GetConnectionString());
        }

        public void SyncUserAccessWithKLE()
        {
            SqlCommand cmduseraccessSync = new SqlCommand("fu_sts_useraccess_kle_sync_insert", _connection);

            SqlTransaction trans = null;
            try
            {
                _connection.Open();
    
                trans = _connection.BeginTransaction();
                cmduseraccessSync.Transaction = trans;
                cmduseraccessSync.CommandTimeout = 600;
                cmduseraccessSync.CommandType = System.Data.CommandType.StoredProcedure;

                cmduseraccessSync.ExecuteNonQuery();

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
                _connection.Close();
            }
        }
    }
}
