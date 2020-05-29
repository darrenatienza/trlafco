using Bunifu.UI.WinForms;
using Bunifu.UI.WinForms.BunifuTextbox;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TRLAFCoSys.App.Properties;
using TRLAFCoSys.Logic;
using TRLAFCoSys.Logic.Contracts;
using TRLAFCoSys.Logic.Implementors;
using TRLAFCoSys.Logic.Models;
using TRLAFCoSys.Queries.Persistence;


namespace TRLAFCoSys.App.Forms
{
    public partial class frmRawMilkProcess : Form
    {
       
        private int milkUtilizeRecordID = 0;

        public frmRawMilkProcess()
        {
            InitializeComponent();
           
           
        }

        private void frmMilkProductRecord_Load(object sender, EventArgs e)
        {
            LoadMainGridList();
        }
        
        private void gridList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void LoadMainGridList()
        {
            try
            {
                
                var list = Factories.CreateRawMilkProcess().GetRecords(dtSearchDate.Value);
                gridList.Rows.Clear();
                int count = 0;
                foreach (var item in list)
                {
                    count++;
                    gridList.Rows.Add(new string[] { "0", count.ToString(),
                        item.Date.ToShortDateString(), 
                        item.ProductName.ToString(),
                        item.Quantity.ToString(),
                         });
                }
                // add space
                gridList.Rows.Add(new string[] { });

                var summaries = Factories.CreateRawMilkProcess().GetSummary(dtSearchDate.Value);
                gridList.Rows.Add(new string[] { "0", "",
                       "", 
                        "Date",
                        "Total Quantity Per Date"});
                foreach (var item in summaries)
                {
                    gridList.Rows.Add(new string[] { "0", "",
                       "",
                        item.Date.ToShortDateString(),
                        item.TotalQuantityPerDay.ToString()
                        });

                }

                var monthlyQuantityTotal = Factories.CreateRawMilkProcess().GetMonthlyTotal(dtSearchDate.Value);

                
                gridList.Rows.Add(new string[] { "0", "",
                    "",
                        "Total Monthly Quantity",
                        monthlyQuantityTotal.ToString()});

       

               
            }
            catch (Exception)
            {

                throw;
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadMainGridList();
        }
        private void dtSearchDate_ValueChanged(object sender, EventArgs e)
        {
            LoadMainGridList();
        }

    }
    

}
