using Accounting.DataLayer;
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
using ValidationComponents;



namespace Accountimg.App
{
    public partial class FrmNewAccounting : Form
    {
       private UnitOfWork db ;
        public int AccountId = 0;
        public FrmNewAccounting()
        {
            InitializeComponent();
        }

        private void FrmNewAccounting_Load(object sender, EventArgs e)
        {
            db = new UnitOfWork();
            dgvCustomers1.AutoGenerateColumns = false;
            dgvCustomers1.DataSource = db.customerRepository.GetCustomers();
            if (AccountId !=0)
            {
              var account=  db.AccountingRepository.GetById(AccountId);
                txtAmount.Text = account.Amount.ToString();
                txtDescription.Text = account.Description;
                txtName.Text=db.customerRepository.GetCustomerNameById(account.CustomerID);
                if (account.TypeID==1)
                {
                    rbRecieve.Checked = true;

                }
                else
                {
                    rbPay.Checked = true;
                }
                this.Text = "ویرایش";
                btnSave.Text = "ویرایش";
                db.Dispose();
            }

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
        {
            dgvCustomers1.AutoGenerateColumns = false;
            dgvCustomers1.DataSource = db.customerRepository.GetCustomers(txtFilter.Text);
        }

        private void dgvCustomers1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtName.Text = dgvCustomers1.CurrentRow.Cells[0].Value.ToString();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
              
            if (BaseValidator.IsFormValid(this.components))
            {
                if (rbRecieve.Checked || rbPay.Checked)
                {
                    db = new UnitOfWork();
                    CustomerAccounting accounting = new CustomerAccounting()
                    {
                        CustomerID = db.customerRepository.GetCustomerIdByName(txtName.Text),
                        TypeID = (rbRecieve.Checked) ? 1 : 2,
                        Amount = int.Parse(txtAmount.Value.ToString()),
                        Description=txtDescription.Text,
                        DateTime= DateTime.Now,



                    };
                    if (AccountId==0)
                    {
                        db.AccountingRepository.Insert(accounting);
                    }
                    else
                    {
                        accounting.ID = AccountId;
                        db.AccountingRepository.Update(accounting);
                        
                    }
                    db.Save();
                    DialogResult = DialogResult.OK;

                    db.Dispose();   
                   
                    
                    
                }
                else
                {
                    RtlMessageBox.Show("لطفا نوع تراکنش را انتخاب کنید");
                }
            }
        }
    }
}
