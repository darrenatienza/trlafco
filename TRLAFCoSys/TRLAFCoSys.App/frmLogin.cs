using TRLAFCoSys.App.Helpers.CustomAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TRLAFCoSys.Logic;
using TRLAFCoSys.Queries.Persistence;

namespace TRLAFCoSys.App
{
    public partial class frmLogin : Form
    {
        public event EventHandler OnLoginSuccess;
        private bool isLogin;
        private int progressID;
        public frmLogin()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        async void Login()
        {
            try
            {
                if (txtUserName.Text == string.Empty && txtPassword.Text == string.Empty)
                {
                    LocalUtils.ShowErrorMessage(this, "User Name or Password is empty");
                }
                else
                {
                    lblLoading.Visible = true;
                    tmrLoading.Enabled = true;

                    await Task.Run(() => Factories.CreateUser().LoginUser(txtUserName.Text, txtPassword.Text));
                    //if login success set isLogin to true
                    isLogin = true;
                    LoginSuccess();
                    this.Close();
                }
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.Message);
            }

        }
        void LoginSuccess()
        {
            var handler = this.OnLoginSuccess;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
           
            Login();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isLogin)
            {
                Application.Exit();
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tmrLoading_Tick(object sender, EventArgs e)
        {
            switch (progressID)
            {
                case 0:
                    progressID++;
                    lblLoading.Text = "Loading Initial Resources..";
                    break;
                case 1:
                    progressID++;
                    lblLoading.Text = "Loading DB Resources..";
                    break;
                case 2:
                    progressID++;
                    lblLoading.Text = "Validating progress..";
                    break;
            }
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
