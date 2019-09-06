using System;
using System.Data;
using System.Data.OleDb;
using System.Configuration;

namespace LibraryDatabase.DAL
{
    /* Customized data access layer tool class for Access Database @RockyWu2019*/
    public class AccessDB
    {
        private static string _AccessDB = ConfigurationManager.AppSettings["AccessDB"];
        private static string _DBConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source="
            + Environment.CurrentDirectory + "\\" + _AccessDB; // Connection string to Access Database
        public static string DBConnString
        {
            get
            {
                return _DBConnString;
            }
        }

        public static DataSet GetDataSet(string sql)
        {
            if (DBConnString == null || DBConnString == "") return null;

            // Use DataAdapter to connect to target database
            OleDbDataAdapter accessAdapter = new OleDbDataAdapter(sql, DBConnString);
            DataSet dsMyData = new DataSet();
            accessAdapter.Fill(dsMyData);
            return dsMyData;
        }

        // Results in a single value, such as from sum and avg
        public static string ExecuteScalar(string sql)
        {
            if (DBConnString == null || DBConnString == "") return null;

            OleDbConnection newConn = new OleDbConnection(DBConnString);
            OleDbCommand newCommand = new OleDbCommand(sql, newConn);
            newConn.Open();
            object value = newCommand.ExecuteScalar();
            newConn.Close();
            if (value == null) return null;
            return value.ToString();

        }

        // Executes Update/Delete/Insert queries
        // Returns number of rows affected
        public static int ExecuteNonQuery(string sql)
        {
            if (DBConnString == null || DBConnString == "") return 0;

            OleDbConnection newConn = new OleDbConnection(DBConnString);
            OleDbCommand newCommand = new OleDbCommand(sql, newConn);
            newConn.Open();
            int i = newCommand.ExecuteNonQuery();
            newConn.Close();
            return i;
        }
    }
}
