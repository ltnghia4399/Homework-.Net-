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
