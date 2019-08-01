using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.ComponentModel;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;


namespace SperReader
{
    public class dbContext: IDisposable
    {
        // Other managed resource this class uses.
        private Component component = new Component();
        // Track whether Dispose has been called.
        private bool disposed = false;

        private IntPtr handle;

        // Property singleton
        public static dbContext Instancia
        {
            get { return Singleton<dbContext>.GetInstancia; }
        }

        #region Constructor
      

        public dbContext()
        {
            handle = new IntPtr();
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
        #endregion

        #region Config Connection
        protected SqlConnection myConnection = null;
        
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
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    component.Dispose();
                }

                // Call the appropriate methods to clean up
                // unmanaged resources here.
                // If disposing is false,
                // only the following code is executed.
                CloseHandle(handle);
                handle = IntPtr.Zero;

                // Note disposing has been done.
                disposed = true;

            }
        }
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private extern static Boolean CloseHandle(IntPtr handle);

        // Use C# destructor syntax for finalization code.
        // This destructor will run only if the Dispose method
        // does not get called.
        // It gives your base class the opportunity to finalize.
        // Do not provide destructors in types derived from this class.
        ~dbContext()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }
        #endregion uport Dispose
    }
}
