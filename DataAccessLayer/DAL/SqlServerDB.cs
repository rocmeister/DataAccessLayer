using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace LibraryDatabase.DAL
{
    /* Customized data access layer tool class for SQL server database @RockyWu2019*/
    public class SqlServerDB
    {
        // In AppSettings set key=DB.ConnectionString and value="data source=."
        private static string _DBConnString = ConfigurationManager.AppSettings["DB.ConnectionString"];

        public static string DBConnString
        {
            get
            {
                return _DBConnString;
            }
        }

        // Use SQL DataAdapter to access the source SQL server database
        public static DataSet GetDataSet(string sql)
        {
            if (DBConnString == null || DBConnString == "") return null;

            SqlDataAdapter sqlDA = new SqlDataAdapter(sql, DBConnString);
            DataSet ds = new DataSet();
            sqlDA.Fill(ds);
            return ds;

        }

        // // Results in a single value, such as from sum and avg
        public static string ExecuteScalar(string sql)
        {
            if (DBConnString == null || DBConnString == "") return null;

            SqlConnection sqlConnection = new SqlConnection(DBConnString);
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);

            sqlConnection.Open();
            object value = sqlCommand.ExecuteScalar();
            sqlConnection.Close();
            if (value == null) return null;
            return value.ToString();
        }

        // Executes Update/Delete/Insert queries 
        // Returns number of rows affected
        public static int ExecuteNonQuery(string sql)
        {
            if (DBConnString == null || DBConnString == "") return 0;

            SqlConnection sqlConnection = new SqlConnection(DBConnString);
            SqlCommand sqlCommand = new SqlCommand(sql, sqlConnection);

            sqlConnection.Open();
            int i = sqlCommand.ExecuteNonQuery();
            sqlConnection.Close();
            return i;
        }


    }
}
