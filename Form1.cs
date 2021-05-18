using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeworkExample
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            dgvData.DataSource = Customer.Instance.GetAllCustomerInformation();

            dgvData.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            cbCustomerID.DataSource = Customer.Instance.GetAllCustomerItem();
            cbCustomerID.ValueMember = "MAKH";
            cbCustomerID.DisplayMember = "MAKH";

            LoadCustomerInformation();
        }

        private void CbCustomerID_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(cbCustomerID.SelectedValue.ToString());
            //Customer.Instance.GetCustomerInformationByID(cbCustomerID.SelectedValue.ToString());

            //CustomerModel customerModel = Customer.Instance.GetCustomerInformationByID(cbCustomerID.SelectedValue.ToString());

            //if(customerModel == null)
            //{
            //    return;
            //}

            ////MessageBox.Show(customerModel.Name);

            //txtCustomerName.Text = customerModel.Name;
            //txtCustomerAddress.Text = customerModel.Address;

            LoadCustomerInformation();

        }

        void LoadCustomerInformation()
        {
            CustomerModel customerModel = Customer.Instance.GetCustomerInformationByID(cbCustomerID.SelectedValue.ToString());

            if (customerModel == null)
            {
                return;
            }

            //MessageBox.Show(customerModel.Name);

            txtCustomerName.Text = customerModel.Name;
            txtCustomerAddress.Text = customerModel.Address;

            
            double total = Convert.ToDouble( Customer.Instance.GetTotalByID(cbCustomerID.SelectedValue.ToString()));

            string strTotal = String.Format("{0} VND", total.ToString("0,000"));

            lbTotal.Text = strTotal;
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = Customer.Instance.GetCustomerBillByID(cbCustomerID.SelectedValue.ToString());

        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            dgvData.DataSource = Customer.Instance.GetAllCustomerInformation();
        }
    }
}
