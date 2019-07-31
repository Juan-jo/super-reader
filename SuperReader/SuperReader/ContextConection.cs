using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SuperReader.SuperReader
{
    public class ContextConection: IDisposable
    {
        private SqlConnection myConnection;

        public ContextConection() {
            //Constriur conextion al servidor de baase de datos
            SqlConnectionStringBuilder myBuilder = new SqlConnectionStringBuilder();
            myBuilder.InitialCatalog = "master";
            myBuilder.DataSource = "(localdb)\\mssqllocaldb";
            myBuilder.ConnectTimeout = 30;

            myConnection = new SqlConnection();
            myConnection.ConnectionString = myBuilder.ConnectionString;
        }
        public static ContextConection Instancia
        {
            get { return Singleton<ContextConection>.Instancia; }
        }

        public void OpenConection()
        {
            if (myConnection.State == ConnectionState.Open)
                myConnection.Close();

            myConnection.Open();
        }
        public void CloseConnection()
        {
            if (myConnection.State == ConnectionState.Open)
                myConnection.Close();
        }
        
        public SqlDataReader getReader(string sql)
        {
            using (var command = new SqlCommand(sql, myConnection))
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
