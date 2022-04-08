namespace Fujitsu.eDoc.ScrapCode
{
    public sealed class DbConnection
    {
        public static DbConnection instance = null;
        public string connString = string.Empty;

        public static DbConnection GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DbConnection();
                }
                return instance;
            }
        }

        public DbConnection()
        {
            string connString = System.Configuration.ConfigurationManager.ConnectionStrings["core"].ConnectionString;
            connString = connString.Replace("net-type=mssql;", "");
            this.connString = connString;
        }
    }
}
