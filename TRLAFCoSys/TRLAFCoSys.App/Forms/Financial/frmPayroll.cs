using Bunifu.UI.WinForms;
using Bunifu.UI.WinForms.BunifuTextbox;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TRLAFCoSys.App.Properties;
using TRLAFCoSys.Logic.Contracts;
using TRLAFCoSys.Logic.Implementors;
using TRLAFCoSys.Logic.Models;
using TRLAFCoSys.Queries.Persistence;


namespace TRLAFCoSys.App.Forms
{
    public partial class frmPayroll : Form
    {
        IPayrollLogic logic;
        private int id;
        private IEnumerable<PayrollListModel> records;
        public frmPayroll()
        {
            InitializeComponent();
            logic = new PayrollLogic();
        }

        private void frmMilkProductRecord_Load(object sender, EventArgs e)
        {
           
            LoadGridList();
           
        }
        
        private void LoadGridList()
        {
            DateTime date = dtDateSearch.Value;
            string criteria = txtSearch.Text;

            try
            {

                records = logic.GetMonthlyRecords(date, criteria);

                int count = 0;
                gridList.Rows.Clear();
                foreach (var item in records)
                {
                    count++;
                    gridList.Rows.Add(new string[] { 
                            "ID",
                            count.ToString(),
                            item.FarmerFullName,
                            item.TotalVolume.ToString(),
                            item.TotalAmount.ToString(),
                            item.FirstQuarterAmount.ToString(),
                            item.Savings.ToString(),
                    item.SecondQuarterAmount.ToString()});
                }

            }
            catch (Exception)
            {

                throw;
            }
        }
        
        private void gridList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_OnIconRightClick(object sender, EventArgs e)
        {
            LoadGridList();
        }


      
        

        private void item_IntCheckTextChange(object sender, EventArgs e)
        {
            var control = (PlaceholderTextBox)sender;

            int value = 0;
            if (!int.TryParse(control.Text, out value))
            {
                control.Text = "";
            }
        }

        void item_DoubleCheckTextChange(object sender, EventArgs e)
        {
            var control = (PlaceholderTextBox)sender;

            double value = 0;
            if (!double.TryParse(control.Text, out value))
            {
                control.Text = "";
            }
        }


        
      
       
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

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

        private void btnMinimized_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }


    }
}
