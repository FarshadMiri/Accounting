using Accounting.DataLayer;
using Accounting.DataLayer.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ValidationComponents;

namespace Accountimg.App
{
    public partial class FrmAddOrEdit : Form
    {
        public int customerId=0;
        UnitOfWork db=new UnitOfWork();
        public FrmAddOrEdit()
        {
            InitializeComponent();
        }

        private void btnSelectPhoto_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog()==DialogResult.OK)
            {
                pcCustomer.ImageLocation = ofd.FileName;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {
                string imageName=Guid.NewGuid().ToString()+Path.GetExtension(pcCustomer.ImageLocation);
                string path = Application.StartupPath + "/Images/";
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                pcCustomer.Image.Save(path+imageName);
                Customers customers = new Customers()
                {
                    FullName = txtName.Text,
                    Mobile = txtMobile.Text,
                    Email = txtEmail.Text,
                    Address = txtAddress.Text,
                    CustomerImage = imageName

                };
                if (customerId==0)
                {
                  
                    db.customerRepository.InsertCustomer(customers);


                }
                else
                {
                    customers.CustomerID= customerId;   
                    db.customerRepository.UpdateCustomer(customers);
                }

                db.Save();
                DialogResult=DialogResult.OK;

            }

        }

        private void FrmAddOrEdit_Load(object sender, EventArgs e)
        {
            if (customerId!=0)
            {
                this.Text = "ویرایش شخص";
                this.btnSave.Text = "ویرایش";
                var customer=db.customerRepository.GetCustomerById(customerId);
                txtName.Text = customer.FullName;
                txtMobile.Text = customer.Mobile;
                txtEmail.Text = customer.Email; 
                txtAddress.Text = customer.Address;
                pcCustomer.ImageLocation = Application.StartupPath + "/Images/" + customer.CustomerImage;

            }
        }
    }
}
