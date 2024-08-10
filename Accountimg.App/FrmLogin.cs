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
    public partial class FrmLogin : Form
    {
        public bool isEdit = false;
        public FrmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (BaseValidator.IsFormValid(this.components))
            {

                using (UnitOfWork db = new UnitOfWork())
                {
                    if (isEdit == true)
                    {
                        var login = db.loginRepository.Get().First();
                        login.UserName = txtUserName.Text;
                        login.Password = txtPassword.Text;
                        db.loginRepository.Update(login);
                        db.Save();
                        Application.Restart();  

                    }
                    if (db.loginRepository.Get(l => l.UserName == txtUserName.Text && l.Password == txtPassword.Text).Any())
                    {
                        DialogResult = DialogResult.OK;
                    }
                    else
                    {
                        RtlMessageBox.Show("کاربری یافت نشد");
                    }

                }
            }

        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {

            if (isEdit == true)
            {
                this.Text = "تنظیمات ورود";
                btnLogin.Text = "ذخیره تغییرات";
                using (UnitOfWork db = new UnitOfWork())
                {
                    var user = db.loginRepository.Get().First();
                    txtUserName.Text = user.UserName;
                    txtPassword.Text = user.Password;

                }

            }



        }
    }
}
