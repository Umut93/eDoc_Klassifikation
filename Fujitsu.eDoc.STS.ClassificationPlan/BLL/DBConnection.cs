namespace Fujitsu.eDoc.STS.ClassificationPlan.BLL
{
    public class DBConnection
    {
        public static string GetConnectionString()
        {
            string ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["core"].ConnectionString;
            ConnectionString = ConnectionString.Replace("net-type=mssql;", "");
            return ConnectionString;
        }
    }
}
