using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace HomeworkExample
{
    class DBConnection
    {
        public static SqlConnection conn = new SqlConnection();

        public static bool OpenConnection()
        {
            try
            {
                //"Data Source=ADMIN\SQLEXPRESS;Initial Catalog=QLKH;Integrated Security=True"

                conn.ConnectionString = "Data Source=ADMIN\\SQLEXPRESS;Initial Catalog=QLKH;Integrated Security=True";

                conn.Open();                

            }
            catch (Exception)
            {
                return false;
                throw;
            }

            return true;
        }

        public static bool CloseConnection()
        {
            try
            {
                conn.Close();
            }
            catch (Exception)
            {
                return false;
                throw;
            }

            return true;
        }
    }
}
