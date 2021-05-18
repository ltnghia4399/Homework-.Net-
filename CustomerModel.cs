using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace HomeworkExample
{
    public class CustomerModel
    {
        string id;
        string name;
        string address;

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Address { get => address; set => address = value; }

        public CustomerModel(DataRow dataRow)
        {
            this.id = dataRow["MAKH"].ToString();
            this.name = dataRow["TENKH"].ToString();
            this.address = dataRow["DIACHI"].ToString();
        }

        public CustomerModel(string _id, string _name, string _address)
        {
            this.id = _id;
            this.name = _name;
            this.address = _address;
        }
    }
}
