using Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TRLAFCoSys.App.Forms;
using TRLAFCoSys.Queries.Persistence;

namespace TRLAFCoSys.App
{
    public partial class frmDashboardV2 : Form
    {
        private DateTime expDate;
        public frmDashboardV2()
        {
            InitializeComponent();

        }

        private void btnGeneral_Click(object sender, EventArgs e)
        {
            pages.SetPage(pageGeneral);
        }

        private void btnMilkRecord_Click(object sender, EventArgs e)
        {
            pages.SetPage(pageMilkRecord);
        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            pages.SetPage(pageInventory);
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            pages.SetPage(pageProduct);
        }

        private void btnFinancial_Click(object sender, EventArgs e)
        {
            pages.SetPage(pageFinancial);
        }

        private void btnSetting_Click(object sender, EventArgs e)
        {
            pages.SetPage(pageSetting);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void SetOpacityToImage(PictureBox pic)
        {
            Image original = null;
            if (original == null) original = (Bitmap)pic.Image.Clone();
            pic.BackColor = Color.Transparent;
            pic.Image = SetAlpha((Bitmap)original, 50);
        }

        private Image SetAlpha(Bitmap bmpIn, int alpha)
        {
            Bitmap bmpOut = new Bitmap(bmpIn.Width, bmpIn.Height);
            float a = alpha / 255f;
            Rectangle r = new Rectangle(0, 0, bmpIn.Width, bmpIn.Height);

            float[][] matrixItems = { 
        new float[] {1, 0, 0, 0, 0},
        new float[] {0, 1, 0, 0, 0},
        new float[] {0, 0, 1, 0, 0},
        new float[] {0, 0, 0, a, 0}, 
        new float[] {0, 0, 0, 0, 1}};

            ColorMatrix colorMatrix = new ColorMatrix(matrixItems);

            ImageAttributes imageAtt = new ImageAttributes();
            imageAtt.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

            using (Graphics g = Graphics.FromImage(bmpOut))
                g.DrawImage(bmpIn, r, r.X, r.Y, r.Width, r.Height, GraphicsUnit.Pixel, imageAtt);

            return bmpOut;
        }
        private void btnUserAccount_Click(object sender, EventArgs e)
        {
            var frm = new frmUsers();
            frm.ShowDialog();
        }

        private void btnFarmer_Click(object sender, EventArgs e)
        {
            var frm = new frmFarmersRecord();
            frm.ShowDialog();
        }

        private void btnSupplyType_Click(object sender, EventArgs e)
        {
            var frm = new frmSupplyType();
            frm.ShowDialog();
        }

        private void btnMilkCollection_Click(object sender, EventArgs e)
        {
            var frm = new frmMilkCollectionRecord();
            frm.ShowDialog();
        }

        private void btnDeliver_Click(object sender, EventArgs e)
        {
            var frm = new frmMilkDeliveryUtilization();
            frm.ShowDialog();
        }

        private void btnMilkProcess_Click(object sender, EventArgs e)
        {
            var frm = new frmRawMilkProcess();
            frm.ShowDialog();
        }

        private void btnSupplyInventory_Click(object sender, EventArgs e)
        {
            var frm = new frmSupplyInventory();
            frm.ShowDialog();
        }

        private void btnProducts_Click(object sender, EventArgs e)
        {
            var frm = new frmProduct();
            frm.ShowDialog();
        }

        private void btnProductions_Click(object sender, EventArgs e)
        {
            var frm = new frmProduction();
            frm.ShowDialog();
        }

        private void btnDailySale_Click(object sender, EventArgs e)
        {
            var frm = new frmActualDailySales();
            frm.ShowDialog();
        }

        private void btnProductSale_Click(object sender, EventArgs e)
        {
            var frm = new frmProductSale();
            frm.ShowDialog();
        }

        private void btnExpeses_Click(object sender, EventArgs e)
        {
            var frm = new frmExpenses();
            frm.ShowDialog();
        }

        private void btnPayroll_Click(object sender, EventArgs e)
        {
            var frm = new frmPayroll();
            frm.ShowDialog();
        }
        private void CheckTrialVersion()
        {
            if (expDate.Date <= DateTime.Now.Date)
            {
                LocalUtils.ShowErrorMessage(this, "Your trial version has been ended! Please contact developer.");
                Application.Exit();
            }
            else
            {
                LocalUtils.ShowInfo(this, "This application is a trial version and will be expired on " + expDate.ToString("MMMM dd, yyyy"));
            }
        }
        private void frmDashboardV2_Load(object sender, EventArgs e)
        {
            expDate = DateTime.Parse("2020-04-30");
            //CheckTrialVersion();
            ShowLogin();
            SetOpacityToPanel(panel2);
            SetOpacityToPanel(panel3);
            SetOpacityToPanel(panel4);
            SetOpacityToPanel(panel5);
            SetOpacityToPanel(panel6);
            SetOpacityToPanel(panel7);
            SetOpacityToPanel(pnlAbout,220);
           
        }

        private void ShowLogin()
        {
            var frm = new frmLogin();
            frm.OnLoginSuccess += frm_OnLoginSuccess;
            frm.ShowDialog();
        }

        void frm_OnLoginSuccess(object sender, EventArgs e)
        {
            this.Show();
        }

        private void SetOpacityToPanel(Panel panel)
        {
            panel.BackColor = Color.FromArgb(125, Color.Gainsboro);
        }
        private void SetOpacityToPanel(Panel panel, int opacity)
        {
            panel.BackColor = Color.FromArgb(opacity, Color.Gainsboro);
        }
        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pageFinancial_Click(object sender, EventArgs e)
        {

        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Hide();
            ShowLogin();
        }

        private void bunifuFlatButton1_Click(object sender, EventArgs e)
        {
            pages.SetPage(pageAbout);
        }
    }
}
