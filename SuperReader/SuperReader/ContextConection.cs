using System;
using System.Diagnostics;
using System.Timers;
using System.Configuration;
using System.Threading;
using System.Data.SqlClient;


namespace SuperReader.SuperReader
{
    public class ContextConection
    {
        public ContextConection() {}

        private static SqlConnection connection;
        protected static SqlConnection getConection()
        {
            SqlConnectionStringBuilder build = new SqlConnectionStringBuilder()
            {
                DataSource = ConfigurationSettings.AppSettings["sever"],
                InitialCatalog = ConfigurationSettings.AppSettings["database"],
                PersistSecurityInfo = true,
                UserID = ConfigurationSettings.AppSettings["userName"],
                Password = ConfigurationSettings.AppSettings["password"],
                MultipleActiveResultSets = false,
                Encrypt = false,
                TrustServerCertificate = false,
                ConnectTimeout = 30
            };
            connection = new SqlConnection(build.ToString());
            connection.Open();
            return connection;
        }

        public static SqlDataReader getReader(string sql)
        {
            SqlConnection conection = getConection();
            using (var command = new SqlCommand(sql, conection))
            {
                command.CommandTimeout = 120;
                command.CommandType = System.Data.CommandType.Text;
                return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
        }
    }
}