using System;

namespace LibraryDatabase.BLL
{
    /* A business logic layer class that checks the username and password */
    public class Biz
    {
        public static bool CheckUserPassword(string userName, string password)
        {
            string sql = string.Format("select count(*) from [User] where UserName='{0}' and Password='{1}'",
                userName, password);

            int result = Convert.ToInt32(DAL.AccessDB.ExecuteScalar(sql));
            return result != 0;
        }
    }
}
