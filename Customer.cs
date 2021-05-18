using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace HomeworkExample
{
    class Customer
    {

        private static Customer instance;

        internal static Customer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Customer();
                }

                return instance;
            }

            set => instance = value;
        }

        public DataTable GetAllCustomerInformation()
        {

            //DataTable dt = new DataTable();

            string query = "SELECT DISTINCT khachhang.TENKH, Chitiet.MASD, Sudung.LOAISD, Chitiet.SOKW, Sudung.DONGIA, dbo.thanhtien(Chitiet.MAKH, Chitiet.MASD) as THANHTIEN FROM khachhang " +
                "JOIN Chitiet ON Khachhang.MAKH = Chitiet.MAKH JOIN Sudung ON Sudung.MASD = Chitiet.MASD";

            //DBConnection.OpenConnection();

            //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, DBConnection.conn);

            //sqlDataAdapter.Fill(dt);

            //DBConnection.CloseConnection();

            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable GetAllCustomerItem()
        {
            //DataTable dt = new DataTable();


            //DBConnection.OpenConnection();

            //SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(query, DBConnection.conn);

            //sqlDataAdapter.Fill(dt);

            //DBConnection.CloseConnection();

            //return dt;


            string query = "select * from khachhang";

            
            return DataProvider.Instance.ExecuteQuery(query);
        }

        public DataTable CustomerInfo()
        {
            string query = "Select * from khachhang";

            return DataProvider.Instance.ExecuteQuery(query);
        }

        //public CustomerModel GetCustomerInformationByID(string _id)
        //{
        //    SqlDataReader reader;

        //    string query = "select * from khachhang where MAKH = @id;";

        //    SqlCommand cmd = new SqlCommand(query, DBConnection.conn);

        //    cmd.Parameters.AddWithValue("@id", _id);

        //    reader = cmd.ExecuteReader();
        //    while (reader.Read())
        //    {

        //        CustomerModel customerModel = new CustomerModel(reader["MAKH"].ToString(), reader["TENKH"].ToString(), reader["DIACHI"].ToString());
        //    }
        //        return customerModel;

        //}

        public CustomerModel GetCustomerInformationByID(string _id)
        {
            string query = "select * from khachhang where MAKH = @id ;";

            DataTable dt = DataProvider.Instance.ExecuteQuery(query, new object[] { _id});

            foreach (DataRow item in dt.Rows)
            {
                return new CustomerModel(item);
            }

            return null;
        }

        public DataTable GetCustomerBillByID(string _id)
        {
            //SELECT DISTINCT khachhang.TENKH, Chitiet.MASD, Sudung.LOAISD, Chitiet.SOKW, Sudung.DONGIA, Chitiet.THANHTIEN FROM khachhang JOIN Chitiet ON khachhang.MAKH = Chitiet.MAKH JOIN Sudung ON Sudung.MASD = Chitiet.MASD WHERE Chitiet.MAKH  = 'KH01'


            string query = "SELECT DISTINCT khachhang.TENKH, Chitiet.MASD, Sudung.LOAISD, Chitiet.SOKW, Sudung.DONGIA, dbo.thanhtien(Chitiet.MAKH, Chitiet.MASD) as THANHTIEN FROM khachhang JOIN Chitiet ON khachhang.MAKH = Chitiet.MAKH JOIN Sudung ON Sudung.MASD = Chitiet.MASD WHERE Chitiet.MAKH  = @id ";

            return DataProvider.Instance.ExecuteQuery(query, new object[] { _id});

        }

        public string GetTotalByID(string _id)
        {
            
            string query = "select distinct Sum( dbo.thanhtien(Chitiet.MAKH, Chitiet.MASD)) as TongTien FROM khachhang JOIN Chitiet ON Khachhang.MAKH = Chitiet.MAKH JOIN Sudung ON Sudung.MASD = Chitiet.MASD WHERE Chitiet.MAKH  = '" + _id + "';";

            return DataProvider.Instance.GetFieldValues(query);
        }
    }
}
