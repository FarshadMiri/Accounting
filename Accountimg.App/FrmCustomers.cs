using Accounting.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Accountimg.App
{
    public partial class FrmCustomers : Form
    {
        UnitOfWork db = new UnitOfWork();
        public FrmCustomers()
        {
            InitializeComponent();
        }

        private void FrmCustomers_Load(object sender, EventArgs e)
        {
            BindGrid();
        }
        public void BindGrid()
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dgvCustomers.AutoGenerateColumns = false;
                dgvCustomers.DataSource = db.customerRepository.GetAllCustomers();
            }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            using (UnitOfWork db = new UnitOfWork())
            {
                dgvCustomers.DataSource = db.customerRepository.GetCustomerByFilter(txtFilter.Text);

            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            BindGrid();
            txtFilter.Text = null;
        }


        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            FrmAddOrEdit frm = new FrmAddOrEdit();

            if (frm.ShowDialog() == DialogResult.OK)
            {
                BindGrid();

            }
        }
        //مشکل پیش امده
        private void btnDeleteCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow != null)
            {
                string name = dgvCustomers.CurrentRow.Cells[1].Value.ToString();
                int customerId = int.Parse(dgvCustomers.CurrentRow.Cells[0].Value.ToString());
                if (RtlMessageBox.Show($"ایا از حذف {name}  مطئن هستید؟", "هشدار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    bool IsSuccess = db.customerRepository.DeleteCustomer(customerId);
                    db.Save();
                    if (IsSuccess == true)
                    {
                        MessageBox.Show("عملیات با موفقیت انجام شد");

                        BindGrid();

                    }
                    else
                    {
                        MessageBox.Show("عملیا با شکست مواجه شد");
                    }

                }



            }
            else
            {
                RtlMessageBox.Show("لطفا شخص را انتخاب کنید ");
            }

        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            if (dgvCustomers.CurrentRow!=null)
            {
                int customerId = int.Parse(dgvCustomers.CurrentRow.Cells[0].Value.ToString());
                FrmAddOrEdit frm=new FrmAddOrEdit();
                frm.customerId = customerId;    
                if (frm.ShowDialog()==DialogResult.OK)
                {
                    BindGrid();

                }

            }
        }
    }
}
