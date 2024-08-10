using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using Accounting.Utility.Covertor;
using Accounting.ViewModels.Customers;
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
    public partial class FrmReport : Form
    {
        UnitOfWork db = new UnitOfWork();
        public int TypeID = 0;
        public FrmReport()
        {
            InitializeComponent();
        }

        private void FrmReport_Load(object sender, EventArgs e)
        {
            List<ListCustomerViewModel> list = new List<ListCustomerViewModel>();
            list.Add(new ListCustomerViewModel()
            {
                CustomerID = 0,
                    FullName = "انتخاب کنید"
            });
            list.AddRange(db.customerRepository.GetCustomers());
            cbCustomer.DataSource= list;
            cbCustomer.DisplayMember = "FullName";
            cbCustomer.ValueMember = "CustomerID";


            if (TypeID == 1)
            {
                this.Text = "گزارش دریافتی ها";
            }
            else
            {
                this.Text = "گزارش پرداختی ها";
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {

            Filter();
        }
        void Filter()
        {
            
            List<CustomerAccounting> list = new List<CustomerAccounting>();

            DateTime? startDate;
            DateTime? endDate;
            
           
           
          

            if ((int)cbCustomer.SelectedValue !=0)
            {
               list.AddRange( db.AccountingRepository.Get(c=> c.TypeID==TypeID && c.CustomerID==(int)cbCustomer.SelectedValue));
                
            }
            else
            {
              list.AddRange( db.AccountingRepository.Get(c => c.TypeID == TypeID));
            }
            if (txtFromDate.Text !="    /  /")
            {

                startDate = Convert.ToDateTime(txtFromDate.Text);
                //startDate = DateConvertor.ToMiladi(startDate.Value);
                list = list.Where(r => r.DateTime >= startDate.Value).ToList();
            }
            if (txtToDate.Text !="    /  /")
            {
                endDate = Convert.ToDateTime(txtToDate.Text);
                //endDate = DateConvertor.ToMiladi(endDate.Value);
                list = list.Where(r => r.DateTime <= endDate.Value).ToList();
            }

            dgReport.Rows.Clear();
            foreach (var accounting in list)
            {
                string customerName = db.customerRepository.GetCustomerNameById(accounting.CustomerID);
                dgReport.Rows.Add(accounting.ID, customerName, accounting.Amount, accounting.DateTime.ToShamsi(), accounting.Description);

            }


        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Filter();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgReport.CurrentRow != null)
            {
                int id = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                if (RtlMessageBox.Show($"ایا از حذف مطمئن هستید؟", "هشدار", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    db.AccountingRepository.Delete(id);
                    db.Save();
                    Filter();

                }
            }
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgReport.CurrentRow != null)
            {
                int id = int.Parse(dgReport.CurrentRow.Cells[0].Value.ToString());
                FrmNewAccounting frm = new FrmNewAccounting();
                frm.AccountId = id;


                if (frm.ShowDialog() == DialogResult.OK)
                {
                    RtlMessageBox.Show("uldgdhj nskng");

                    Filter();

                }
            }


        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            DataTable dtPrint=new DataTable();
            dtPrint.Columns.Add("Customer");
            dtPrint.Columns.Add("Amount");
            dtPrint.Columns.Add("Date");
            dtPrint.Columns.Add("Description");

            foreach (DataGridViewRow item in dgReport.Rows)
            {
                dtPrint.Rows.Add(
                    item.Cells[1].Value.ToString(),
                    item.Cells[2].Value.ToString(),
                    item.Cells[3].Value.ToString(),
                    item.Cells[4].Value.ToString());

            }
            stiPrint.Load(Application.StartupPath + "/Report.mrt");
            stiPrint.RegData("DT",dtPrint);
            stiPrint.Show();

        }
    }
}
