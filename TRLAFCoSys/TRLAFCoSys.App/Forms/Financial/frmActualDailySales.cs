using Bunifu.UI.WinForms;
using Bunifu.UI.WinForms.BunifuTextbox;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
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
    public partial class frmActualDailySales : Form, IFormActions
    {
        private int dailySaleID;
        
        public frmActualDailySales()
        {
            InitializeComponent();
           
        }

        private void frmMilkProductRecord_Load(object sender, EventArgs e)
        {
            LoadMainGrid();
            AddHandlers();
            SetToolTip();
        }

        

        
        
        private void gridList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_OnIconRightClick(object sender, EventArgs e)
        {
            LoadMainGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
            
        }

        private void btnUpdateFormClose_Click(object sender, EventArgs e)
        {
            // we close the current page to show the main grid
            ShowMainGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {

            Edit();
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

        private void tabPage2_Resize(object sender, EventArgs e)
        {
            LocalUtils.CenterControlXY(pageDetail, pnlMainData);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }


        #region Private Methods

        #region Validations
        public bool ValidateFields()
        {
            var textboxes = LocalUtils.GetAll(pnlMainData, typeof(BunifuTextBox))
                      .Where(c => c.CausesValidation == true).OrderBy(c => c.TabIndex);
            var dropdowns = LocalUtils.GetAll(pnlMainData, typeof(BunifuDropdown))
                      .Where(c => c.CausesValidation == true).OrderBy(c => c.TabIndex);

            foreach (BunifuTextBox item in textboxes)
            {
                if (item.Text == string.Empty)
                {
                    return false;
                }
            }
            foreach (BunifuDropdown item in dropdowns)
            {
                if (item.Text == string.Empty)
                {
                    return false;
                }
            }
            return true;
        }
        private bool ValidateFields(Control container)
        {
            var textboxes = LocalUtils.GetAll(container, typeof(BunifuTextBox))
                      .Where(c => c.CausesValidation == true).OrderBy(c => c.TabIndex);
            var dropdowns = LocalUtils.GetAll(container, typeof(BunifuDropdown))
                      .Where(c => c.CausesValidation == true).OrderBy(c => c.TabIndex);

            foreach (BunifuTextBox item in textboxes)
            {
                if (item.Text == string.Empty)
                {
                    return false;
                }
            }
            foreach (BunifuDropdown item in dropdowns)
            {
                if (item.Text == string.Empty)
                {
                    return false;
                }
            }
            return true;
        }
        private void AddHandlers()
        {
            var textboxes = LocalUtils.GetAll(this, typeof(BunifuTextBox))
                     .Where(c => c.CausesValidation == true).OrderBy(c => c.TabIndex);
            foreach (BunifuTextBox item in textboxes)
            {
                if (item.Tag != null)
                {


                    if (item.Tag.ToString().Contains("integer") || item.Tag.ToString().Contains("double"))
                    {
                        item.KeyPress += textBox_keyPress;
                    }


                }
            }
            //We get all textboxes with tag compute to compute textbox value
            // after text change event fires
            var computeTextBoxes = LocalUtils.GetAll(this, typeof(BunifuTextBox))
                    .Where(c => c.Tag.ToString().Contains("compute")).OrderBy(c => c.TabIndex);
            foreach (BunifuTextBox item in computeTextBoxes)
            {
                item.TextChange += item_TextChange;
            }
        }
        private void textBox_keyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            {
                e.Handled = true;
            }
        }
        void item_TextChange(object sender, EventArgs e)
        {
            Compute();
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

        #endregion

        public void Add()
        {
            try
            {
                mainPage.SetPage(pageDetail);
                ResetInputs();
                dailySaleID = 0;
                SetInitialData();
                dtDate.Enabled = true;
                bunifuTransition1.HideSync(pnlSide, false, BunifuAnimatorNS.Animation.HorizBlind);
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }

        }
        public void Edit()
        {
            try
            {
                if (gridList.SelectedRows.Count > 0)
                {
                    // get id
                    dailySaleID = int.Parse(gridList.SelectedRows[0].Cells[0].Value.ToString());
                    if (dailySaleID != 0)
                    {
                        //show and hide controls
                        mainPage.SetPage(pageDetail);
                        bunifuTransition1.HideSync(pnlSide, false, BunifuAnimatorNS.Animation.Transparent);
                        SetData();
                    }
                    else
                    {
                        LocalUtils.ShowNoRecordFoundMessage(this);
                        mainPage.SetPage(tabGridList);
                        bunifuTransition1.ShowSync(pnlSide, false, BunifuAnimatorNS.Animation.Transparent);
                    }

                }
                else
                {

                    LocalUtils.ShowNoRecordSelectedMessage(this);
                }
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }

        }
            public void Delete()
            {
                try
                {
                    if (gridList.SelectedRows.Count > 0)
                    {
                        dailySaleID = int.Parse(gridList.SelectedRows[0].Cells[0].Value.ToString());
                        if (dailySaleID != 0)
                        {
                            DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Milk Collection", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (result == DialogResult.Yes)
                            {
                                Factories.CreateDailySale().Delete(dailySaleID);
                                LocalUtils.ShowDeleteSuccessMessage(this);
                                ShowMainGrid();
                                dailySaleID = 0;
                            }
                        }
                        else
                        {
                            LocalUtils.ShowNoRecordFoundMessage(this);
                        }

                    }
                    else
                    {

                        LocalUtils.ShowNoRecordSelectedMessage(this);
                    }

                }
                catch (Exception)
                {

                    throw;
                }
            }
        private void Compute()
        {
            try
            {

                var outletSale1 = LocalUtils.ConvertToDouble(txtOutletSale1.Text);
                var outletSale2 = LocalUtils.ConvertToDouble(txtOutletSale2.Text);
                var processingSale = LocalUtils.ConvertToDouble(txtProcessingSale.Text);
                var saleOnAccount = LocalUtils.ConvertToDouble(txtSalesOnAccount.Text);
                var rawMilkSale = LocalUtils.ConvertToDouble(txtRawMilkSales.Text);
                var totalSaleDairyProduct = LocalUtils.ConvertToDouble(txtTotalSaleDairyProducts.Text);
                var c_totalCashSale = Factories.CreateDailySale().ComputeTotalCashSale(outletSale1, outletSale2, processingSale);
                var c_totalSale = Factories.CreateDailySale().ComputeTotalSale(outletSale1,outletSale2, processingSale, saleOnAccount,rawMilkSale);
                var c_totalSaleDairyProduct = Factories.CreateDailySale().ComputeTotalSaleDairyProduct(outletSale1, outletSale2, processingSale, saleOnAccount);
                txtTotalSale.Text = c_totalSale.ToString();
                txtTotalCashSale.Text = c_totalCashSale.ToString();
                txtTotalSaleDairyProducts.Text = c_totalSaleDairyProduct.ToString();

            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }
        }
        public  void LoadMainGrid()
        {
            try
            {
                var date = dtSearchDate.Value;
             
                var models = Factories.CreateDailySale().GetAllByMonth(date);
                gridList.Rows.Clear();
                int count = 0;
                foreach (var item in models)
                {
                    count++;
                    gridList.Rows.Add(new string[] { item.ID.ToString(), count.ToString(), 
                            
                            item.Date.ToShortDateString(),
                            item.OutletSale1.ToString(),
                            item.OutletSale2.ToString(),
                            item.ProcessingSale.ToString(),
                            item.TotalCashSales.ToString(),
                            item.SaleOnAccount.ToString(),
                            item.TotalSaleForDairyProduct.ToString(),
                            item.TotalSales.ToString(),
                            item.Debtor
                    });
                }

                //use 0 id to avoid null exception while click edit button
                ////Adds space
                //var summaryModels = mainLogic.GetAllSummary(date);

                //gridList.Rows.Add(new string[] { "0","", 
                //            "",
                //            "Total",
                //            summaryModels.TotalMilkSold               .ToString()   , 
                //            summaryModels.TotalMilkSoldAmount         .ToString()   ,
                //            summaryModels.TotalB1plus1                .ToString()   ,
                //            summaryModels.TotalDiscountSale           .ToString()   ,
                //            summaryModels.TotalEcoBag                 .ToString()   ,
                //            summaryModels.TotalOutletSale1            .ToString()   ,
                //            summaryModels.TotalOutletSale2            .ToString()   ,
                //            summaryModels.TotalProcessingSale         .ToString()   ,
                //            summaryModels.GrandTotalCashSale          .ToString()   ,
                //            summaryModels.TotalSaleOnAccount          .ToString()   ,
                //            summaryModels.GrandTotalSaleDairyProduct  .ToString()   ,
                //           });


                SetGridListCustomDesign();



            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }
        }

        private void SetGridListCustomDesign()
        {
            // modification to columns
            foreach (DataGridViewRow row in gridList.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.Value != null)
                    {
                        var cellValue = cell.Value.ToString();
                        if (cellValue.Contains("Total"))
                        {
                            row.DefaultCellStyle.BackColor = Color.FromArgb(30, 144, 255);
                            row.DefaultCellStyle.ForeColor = Color.White;
                        }
                    }

                }
            }
        }

        public void ResetInputs()
        {
            var textboxes = LocalUtils.GetAll(pnlMainData, typeof(BunifuTextBox));
            foreach (BunifuTextBox item in textboxes)
            {
                item.Text = string.Empty;
            }
            var comboboxes = LocalUtils.GetAll(pnlMainData, typeof(BunifuDropdown));
            foreach (BunifuDropdown item in comboboxes)
            {
                item.Text = string.Empty;
            }
        }

        public void SetData()
        {
            try
            {

                // populate data
                var model = Factories.CreateDailySale().Get(dailySaleID);
                dtDate.Value = model.Date;
                txtOutletSale1.Text = model.OutletSale1.ToString();
                txtOutletSale2.Text = model.OutletSale2.ToString();
                txtProcessingSale.Text = model.ProcessingSale.ToString();
                // Total Cash Sale Dairy Product
                txtTotalCashSale.Text = model.TotalCashSales.ToString();
                txtRawMilkSales.Text = model.RawMilkSales.ToString();
                txtSalesOnAccount.Text = model.SaleOnAccount.ToString();
                txtTotalSaleDairyProducts.Text = model.TotalSaleForDairyProduct.ToString();
                // Total Sale Over All Sale 
                txtTotalSale.Text = model.TotalSales.ToString();
                
                txtDebtor.Text = model.Debtor.ToString();
                
               

            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }

        }
        public void SetInitialData()
        {
            try
            {

                // populate data
                var model = Factories.CreateDailySale().GetInitial(dtDate.Value);
                dtDate.Value = model.Date;
                txtOutletSale1.Text = model.OutletSale1.ToString();
                txtOutletSale2.Text = model.OutletSale2.ToString();
                txtRawMilkSales.Text = model.RawMilkSales.ToString();
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }

        }
        public  void Save()
        {
            try
            {
                if (!ValidateFields())
                {
                    LocalUtils.ShowValidationFailedMessage(this);

                }
                else
                {

                    var model = new DailySaleRecordModel();
                   
                    model.Debtor = txtDebtor.Text;
                    model.Date = dtDate.Value;
                    model.ProcessingSale = LocalUtils.ConvertToDouble(txtProcessingSale.Text);
                    model.SaleOnAccount = LocalUtils.ConvertToDouble(txtSalesOnAccount.Text);
                    // get id
                    if (dailySaleID != 0)
                    {

                        Factories.CreateDailySale().Edit(dailySaleID, model);
                        LocalUtils.ShowSaveMessage(this);
                    }
                    else
                    {

                        Factories.CreateDailySale().Add(model);
                        dailySaleID = model.ID;
                        LocalUtils.ShowSaveMessage(this);
                    }
                }

            }
            catch (ApplicationException ex)
            {
                LocalUtils.ShowErrorMessage(this, ex.Message);
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }
        }

        private void ShowMainGrid()
        {
            mainPage.SetPage(tabGridList);
            LoadMainGrid();
            bunifuTransition1.ShowSync(pnlSide, false, BunifuAnimatorNS.Animation.Transparent);

        }
        #endregion


       

        private void dtSearchDate_ValueChanged(object sender, EventArgs e)
        {
            LoadMainGrid();
        }

        private void pnlMainData_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bunifuCustomLabel8_Click(object sender, EventArgs e)
        {

        }

        private void txtSalesOnAccount_TextChanged(object sender, EventArgs e)
        {

        }

        private void dtDate_ValueChanged(object sender, EventArgs e)
        {
            
            if (dailySaleID == 0)
            {
                SetInitialData();
            }
        }
        void SetToolTip()
        {
            bunifuToolTip1.SetToolTip(btnAdd, "Add");
            bunifuToolTip1.SetToolTip(btnEdit, "Edit");
            bunifuToolTip1.SetToolTip(btnDelete, "Delete");
        }

        
    }
    

}
