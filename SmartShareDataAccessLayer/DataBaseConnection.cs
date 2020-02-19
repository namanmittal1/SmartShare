using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartShareDataAccessLayer
{
    public class DataBaseConnection
    {
        private static DataBaseConnection dbInstance;
        //static string fileName=@"ExpenseTracker.db";

#if DEBUG
        private static String DATAPATH = @"Data Source = \\SmartShareDB.db; Version = 3 ";
#else
        private static String ExePath = System.AppDomain.CurrentDomain.BaseDirectory;
        private static String DATAPATH = "Data Source = " + ExePath + @"SmartShareDataBase\SmartShareDB.db" + "; Version = 3 ";
#endif
        private readonly SQLiteConnection conn = new SQLiteConnection(DATAPATH);

        private DataBaseConnection()
        {
        }

        public static DataBaseConnection GetDbInstance()
        {
            if (dbInstance == null)
            {
                dbInstance = new DataBaseConnection();
            }
            return dbInstance;
        }

        public SQLiteConnection GetDBConnection()
        {
            try
            {
                if (conn != null && conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                    var pragma = new SQLiteCommand("PRAGMA foreign_keys = true;", conn);
                    pragma.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
            }
            finally
            {
            }
            return conn;
        }

        public void CloseDBConnection()
        {
            try
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                    conn.Dispose();
                }
            }
            catch (SqlException e)
            {
            }
            finally
            {
            }
        }
    }
}
