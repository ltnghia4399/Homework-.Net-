using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace HomeworkExample
{
    class DataProvider
    {
        private static DataProvider instance;
        /// <summary>
        /// Sigleton of Dataprovider
        /// </summary>
        internal static DataProvider Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new DataProvider();

                }
                return instance;
            }

            set => instance = value;
        }

        /// <summary>
        /// Insert, Update, Delete
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string query, object[] parameter = null)
        {
            int data = 0;

            using (SqlConnection conn = DBConnection.conn)
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(query, conn);

                if (parameter != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            cmd.Parameters.AddWithValue(item, parameter[i]);
                            i++;
                        }
                    }
                }

                data = cmd.ExecuteNonQuery();

                conn.Close();
            }

            return data;
        }

        /// <summary>
        /// Select
        /// </summary>
        /// <param name="query"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public DataTable ExecuteQuery(string query, object[] parameter = null)
        {

            DataTable data = new DataTable();
            using (SqlConnection conn = DBConnection.conn)
            {
                try
                {
                    DBConnection.OpenConnection();

                    SqlCommand cmd = new SqlCommand(query, conn);

                    if (parameter != null)
                    {
                        string[] listPara = query.Split(' ');
                        int i = 0;
                        foreach (string item in listPara)
                        {
                            if (item.Contains('@'))
                            {
                                cmd.Parameters.AddWithValue(item, parameter[i]);
                                i++;
                            }
                        }
                    }

                    SqlDataAdapter adap = new SqlDataAdapter(cmd);

                    try
                    {
                        adap.Fill(data);
                    }
                    catch (Exception ex)
                    {
                        //MessageBox.Show("Error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    conn.Close();
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("Error : " + ex.Message, "Cant connect with database", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }

            return data;

        }
        /// <summary>
        /// Get String Value
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public string GetFieldValues(string query)
        {
            string mString = null;

            using (SqlConnection conn = DBConnection.conn)
            {
                DBConnection.OpenConnection();

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader reader;
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    mString = reader.GetValue(0).ToString();

                }
                reader.Close();

                DBConnection.CloseConnection();
            }

            return mString;
        }

    }
}
