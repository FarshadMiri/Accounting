using Accounting.Business;
using Accounting.Utility.Covertor;
using Accounting.ViewModels.Accounting;
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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnCustomer_Click(object sender, EventArgs e)
        {
            FrmCustomers frm=new FrmCustomers();    
            frm.ShowDialog();
        }

        private void btnNewAccounting_Click(object sender, EventArgs e)
        {
            FrmNewAccounting frm=new FrmNewAccounting();
            frm.ShowDialog();
        }

        private void btnReportPay_Click(object sender, EventArgs e)
        {
            FrmReport frmReport=new FrmReport();
            frmReport.TypeID = 2;
            frmReport.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            this.Hide();
            FrmLogin frmLogin = new FrmLogin(); 
            if (frmLogin.ShowDialog()==DialogResult.OK)
            {
                
                
                
                lblDate.Text = DateTime.Now.ToShamsi();
                lblTime.Text = DateTime.Now.ToString("hh:mm:ss");
                Report();
                

            }
            else
            {
                Application.Exit(); 

            }
        }
        public void Report()
        {
            ReportViewModel report = Account.ReportForMain();
            lblRecieve.Text = report.Recieve.ToString("#,0");
            lblPay.Text = report.Pay.ToString("#,0");    
            lblRest.Text = report.Rest.ToString("#,0");  
        }

        private void btnReportRecieve_Click(object sender, EventArgs e)
        {
            FrmReport frmReport = new FrmReport();
            frmReport.TypeID = 1;
            frmReport.ShowDialog();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss");
        }

        private void btnEditLogin_Click(object sender, EventArgs e)
        {
            FrmLogin frmLogin = new FrmLogin();
            frmLogin.isEdit= true;
            frmLogin.ShowDialog();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Report();
        }
    }
}
