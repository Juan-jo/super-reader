using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace SperReader
{
    public class dbContext: IDisposable
    {
        // Property singleton
        public static dbContext Instancia
        {
            get { return Singleton<dbContext>.GetInstancia; }
        }

        #region Config Connection
        protected SqlConnection myConnection = null;

        public dbContext() {
            SqlConnectionStringBuilder myBuilder = new SqlConnectionStringBuilder()
            {
                InitialCatalog = "master",
                DataSource = "(localdb)\\mssqllocaldb",
                ConnectTimeout = 30
            };

            myConnection = new SqlConnection()
            {
                ConnectionString = myBuilder.ConnectionString
            };
        }
        
        public void OpenConnection()
        {
            if (myConnection.State == ConnectionState.Open)
                return;
            myConnection.Open();
        }

        public void CloseConnection()
        {
            if (myConnection.State == ConnectionState.Open)
                myConnection.Close();
        }
        #endregion Config Connection

        #region MSSQL
        public SqlDataReader GetReaderBySQL(string sql)
        {
            using (var cmd = new SqlCommand(sql, myConnection))
            {
                cmd.CommandType = CommandType.Text;
                cmd.CommandTimeout = 120;
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
        }


        #endregion MSSQL

        #region Support Dispose
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion uport Dispose
    }
}
