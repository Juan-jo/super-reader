using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SuperReader.SuperReader
{
    public class ContextConection: IDisposable
    {
        private SqlConnection _MyConnection;

        public ContextConection() {

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
            _MyConnection = new SqlConnection(build.ConnectionString);
        }

        public static ContextConection Instancia
        {
            get { return Singleton<ContextConection>.Instancia; }
        }

        public void OpenConection()
        {
            if (_MyConnection.State == ConnectionState.Open)
                _MyConnection.Close();

            _MyConnection.Open();
        }
        public void CloseConnection()
        {
            if (_MyConnection.State == ConnectionState.Open)
                _MyConnection.Close();
        }
        
        public SqlDataReader getReader(string sql)
        {
            using (var command = new SqlCommand(sql, _MyConnection))
            {
                command.CommandTimeout = 120;
                command.CommandType = System.Data.CommandType.Text;
                return command.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            }
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
