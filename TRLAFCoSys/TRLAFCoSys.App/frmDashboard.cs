using TRLAFCoSys.App.Helpers.Themes;
using TRLAFCoSys.App.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TRLAFCoSys.Queries.Persistence;
using TRLAFCoSys.App.Forms;
using Bunifu.UI.WinForms.BunifuTextbox;
using Bunifu.Framework.UI;

namespace TRLAFCoSys.App
{
    public partial class frmDashboard : Form
    {

        public frmDashboard()
        {
            InitializeComponent();
        }

        private void btnMenu_Click(object sender, EventArgs e)
        {
            //ResizeSideBar();
        }
        //private void ResizeSideBar()
        //{
        //    const int maxSize = (int)SideBar.maxSize;
        //    const int minSize = (int)SideBar.minSize;
        //    switch (pnlSideBar.Width)
        //    {
        //        case maxSize:
        //            //reduce the size of side bar
        //            pnlSideBar.Width = minSize; 
        //            //hide the avatar
        //            pictureBox1.Visible = false;
        //            btnDashboard.Text = "";
        //            btnAbout.Text = "";
        //            break;
        //        default:
        //            //show the avatar
        //            pictureBox1.Visible = true;
        //            //increase the size of side bar
        //            pnlSideBar.Width = maxSize;
        //            btnDashboard.Text = btnDashboard.Tag.ToString();
        //            btnAbout.Text = btnAbout.Tag.ToString();
        //            break;
        //    }
        //}

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            switch (this.WindowState)
            {
                case FormWindowState.Maximized:
                    // restore the default windows state
                    this.WindowState = FormWindowState.Normal;
                    // change image to maximized
                    btnMaximize.Image = Resources.maximize_window_48;
                    
                    break;
                default:
                    // restore the maximized windows state
                    this.WindowState = FormWindowState.Maximized;
                    // change image to restore
                    btnMaximize.Image = Resources.restore_window_48;
                    
                    break;
            }  
             
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMinimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnShowMoreMenus_Click(object sender, EventArgs e)
        {
            //ResizeSideBar();
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            mainPage.SetPage(page1);
        }

        private void HideAllControls()
        {
            
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            //mainPage.SetPage(pageDashboard);
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            ShowLogin();
            AddHandlers();
            lblDate.Text = DateTime.Now.ToLongDateString();
        }

        private void AddHandlers()
        {
            var flatButtons = LocalUtils.GetAll(this, typeof(BunifuFlatButton))
                     .OrderBy(c => c.TabIndex);
            foreach (BunifuFlatButton item in flatButtons)
            {
                item.IconVisible = true;
            }
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
           
        }

        private void bunifuCards1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuThinButton21_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pageDashboard_Resize(object sender, EventArgs e)
        {   
            //Utils.CenterControlXY(pageDashboard, pnlDashboard);
        }

        private void btnMilkCollection_Click(object sender, EventArgs e)
        {
            var form = new frmMilkCollectionRecord();
            form.ShowDialog();
        }

        private void btnExpenses_Click(object sender, EventArgs e)
        {
            var form = new frmExpenses();
            form.ShowDialog();
        }

        private void btnPayroll_Click(object sender, EventArgs e)
        {
            var form = new frmPayroll();
            form.ShowDialog();
        }

        private void btnMilkUtilization_Click(object sender, EventArgs e)
        {
            var form = new frmMilkDeliveryUtilization();
            form.ShowDialog();
        }

        private void btnSupplyInventory_Click(object sender, EventArgs e)
        {
            var form = new frmSupplyInventory();
            form.ShowDialog();
        }

        private void btnActualDailySales_Click(object sender, EventArgs e)
        {
            var form = new frmActualDailySales();
            form.ShowDialog();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            var form = new frmProduct();
            form.ShowDialog();
        }

        private void btnProduction_Click(object sender, EventArgs e)
        {
            var form = new frmProduction();
            form.ShowDialog();
        }

        private void bunifuThinButton21_Click_1(object sender, EventArgs e)
        {
            var form = new frmFarmersRecord();
            form.ShowDialog();
        }

        private void bunifuFlatButton4_Click(object sender, EventArgs e)
        {
            var form = new frmMilkDeliveryUtilization();
            form.ShowDialog(); 
        }

        private void btnPagNav_Click(object sender, EventArgs e)
        {
            if (mainPage.SelectedTab == page1)
            {
                mainPage.SetPage(page2);
                
                btnPagNav.Image = Resources.arrow_96_256;
                lblPageNav.Text = "Page 2/2";
                
            }
            else
            {
                mainPage.SetPage(page1);
                btnPagNav.Image = Resources.arrow_31_256;
                lblPageNav.Text = "Page 1/2";
            }
            
        }

        private void btnFarmers_Click(object sender, EventArgs e)
        {
            var form = new frmFarmersRecord();
            form.ShowDialog();
        }

        private void btnSupplyTypes_Click(object sender, EventArgs e)
        {
            var form = new frmSupplyType();
            form.ShowDialog();
        }

        private void btnMilkCollections_Click(object sender, EventArgs e)
        {
            var form = new frmMilkCollectionRecord();
            form.ShowDialog();
        }

        private void btnRawMilkProcess_Click(object sender, EventArgs e)
        {
            var form = new frmRawMilkProcess();
            form.ShowDialog();
        }

        private void btnSupplyInventory_Click_1(object sender, EventArgs e)
        {
            var form = new frmSupplyInventory();
            form.ShowDialog();
        }

        private void btnProductRecords_Click(object sender, EventArgs e)
        {
            var form = new frmProduct();
            form.ShowDialog();
        }

        private void btnProductionRecords_Click(object sender, EventArgs e)
        {
            var form = new frmProduction();
            form.ShowDialog();
        }

        private void btnActualDailySales_Click_1(object sender, EventArgs e)
        {
            var form = new frmActualDailySales();
            form.ShowDialog();
        }

        private void btnExpenses_Click_1(object sender, EventArgs e)
        {
            var form = new frmExpenses();
            form.ShowDialog();
        }

        private void btnPayroll_Click_1(object sender, EventArgs e)
        {
            var form = new frmPayroll();
            form.ShowDialog();
        }

        private void btnProductSales_Click(object sender, EventArgs e)
        {
            var form = new frmProductSale();
            form.ShowDialog();
        }

        private void btnUserAccounts_Click(object sender, EventArgs e)
        {
            var form = new frmUsers();
            form.ShowDialog();
        }

        private void frmDashboard_Shown(object sender, EventArgs e)
        {
          
        }
        private void ShowLogin()
        {
            var frmLogin = new frmLogin();
            frmLogin.OnLoginSuccess += frmLogin_OnLoginSuccess;
            frmLogin.ShowDialog();
        }

        private void frmLogin_OnLoginSuccess(object sender, EventArgs e)
        {
           
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            ShowLogin();
            
        }
    }
}
