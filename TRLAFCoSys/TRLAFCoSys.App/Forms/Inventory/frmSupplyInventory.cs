using Bunifu.UI.WinForms;
using Bunifu.UI.WinForms.BunifuTextbox;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using TRLAFCoSys.App.Properties;
using TRLAFCoSys.Logic;
using TRLAFCoSys.Logic.Contracts;
using TRLAFCoSys.Logic.Implementors;
using TRLAFCoSys.Logic.Models;
using TRLAFCoSys.Queries.Persistence;


namespace TRLAFCoSys.App.Forms
{
    public partial class frmSupplyInventory : Form
    {
        private int id;

        public frmSupplyInventory()
        {
            InitializeComponent();
           
            
        }

        private void frmSupplyInventory_Load(object sender, EventArgs e)
        {
           
          
            SetToolTip();
            AddHandlers();
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
            mainPage.SetPage(pageDetail);
            ResetInputs();
            lblAddEditTitle.Text = "Add New Record";
;
            id = 0;
            bunifuTransition1.HideSync(pnlSide,false,BunifuAnimatorNS.Animation.HorizBlind);
            
        }

        private void btnAddEditClose_Click(object sender, EventArgs e)
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
            try
            {
                if (gridList.SelectedRows.Count > 0)
                {
                    id = int.Parse(gridList.SelectedRows[0].Cells[0].Value.ToString());
                    if (id != 0)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Milk Collection", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            Factories.CreateSupplyInventory().Delete(id);
                            LocalUtils.ShowDeleteSuccessMessage(this);
                            ShowMainGrid();
                            id = 0;
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
        private void dtDate_ValueChanged(object sender, EventArgs e)
        {
           
        }
        #region Private Methods

        #region Validations
        private bool ValidateFields()
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
        private void Compute()
        {
            try
            {
                var withdrawQty = LocalUtils.ConvertToDouble(txtWithdrawQty.Text);
                var purchaseQty = LocalUtils.ConvertToDouble(txtPurchaseQty.Text);
                var unitprice = LocalUtils.ConvertToDouble(txtUnitPrice.Text);

                var purchaseTotal = Factories.CreateSupplyInventory().Compute(purchaseQty, unitprice);
                var widthdrawTotal = Factories.CreateSupplyInventory().Compute(withdrawQty, unitprice);
                txtPurchaseTotal.Text = purchaseTotal.ToString();
                txtWithdrawTotal.Text = widthdrawTotal.ToString();

            }
            catch (Exception)
            {

                throw;
            }
        }
       
        
        private async void LoadMainGrid()
        {
            try
            {
                var criteria = txtSearch.Text;
                var date = dtSearchDate.Value;


                var models = await Task.Run(() => Factories.CreateSupplyInventory().GetComputedByMonth(date, criteria));


                gridList.Rows.Clear();
                int count = 0;
                foreach (var item in models)
                {
                    count++;
                    gridList.Rows.Add(new string[] { item.ID.ToString(), item.SupplyClassID.ToString(), item.SupplyTypeID.ToString(), count.ToString(), 
                            
                            item.Description,
                            item.UnitPrice.ToString(),
                            item.BeginningQuantity.ToString(), 
                            item.BeginningTotal.ToString(),
                            item.PurchaseQuantity.ToString(), 
                            item.PurchaseTotal.ToString(),
                            item.WithdrawQuantity.ToString(), 
                            item.WithdrawTotal.ToString(),
                            item.EndingQuantity.ToString(),
                            item.EndingTotal.ToString()});
                }
                // use 0 id to avoid null exception while click edit button
                //Adds space
                gridList.Rows.Add(new string[] { "0", });
                gridList.Rows.Add(new string[] { "0","", 
                            "",
                           "",
                            "",
                            "Summaries - Sub Total Amount"
                           });
                gridList.Rows.Add(new string[] { "0","", 
                            "",
                           "Description",
                            "Beginning",
                            "Purchase",
                            "Withdraw",
                            "Ending"
                           });

                var summaryModels = await Task.Run(() => Factories.CreateSupplyInventory().GetAllSummary(date));
                int summaryCount = 0;
                foreach (var item in summaryModels)
                {
                    summaryCount++;
                    gridList.Rows.Add(new string[] { "0","", 
                            "",
                            item.Description,
                            item.BeginningSubTotalAmount.ToString(), 
                             item.PurchaseSubTotalAmount.ToString(),
                              item.WithdrawSubTotalAmount.ToString(),
                               item.EndingSubTotalAmount.ToString(),
                           });
                }

                gridList.Rows.Add(new string[] { "0","", 
                            "",
                           "",
                            "",
                            "Summaries - Grand Total"
                           });
                gridList.Rows.Add(new string[] { "0","", 
                            "",
                           "",
                            "Beginning",
                            "Purchase",
                            "Withdraw",
                            "Ending"
                           });

                var grandTotalSummary = await Task.Run(() => Factories.CreateSupplyInventory().GetGrandTotalSummary(date));


                gridList.Rows.Add(new string[] { "0","", 
                            "",
                            "",
                            grandTotalSummary.BeginningGrandTotalAmount.ToString(), 
                             grandTotalSummary.PurchaseGrandTotalAmount.ToString(),
                              grandTotalSummary.WithdrawGrandTotalAmount.ToString(),
                               grandTotalSummary.EndingGrandTotalAmount.ToString(),
                           });
                // modification to columns
                foreach (DataGridViewRow row in gridList.Rows)
                {
                    foreach (DataGridViewCell cell in row.Cells)
                    {
                        if (cell.Value != null)
                        {
                            var cellValue = cell.Value.ToString();
                            if (cellValue.Contains("Summaries"))
                            {
                                row.DefaultCellStyle.BackColor = Color.FromArgb(30, 144, 255);
                                row.DefaultCellStyle.ForeColor = Color.White;
                            }
                        }

                    }
                }

            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }
        }

        private void ResetInputs()
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
        public void Edit()
        {
            try
            {
                if (gridList.SelectedRows.Count > 0)
                {
                    // get id
                    id = int.Parse(gridList.SelectedRows[0].Cells[0].Value.ToString());
                    if (id != 0)
                    {

                        //show and hide controls

                        lblAddEditTitle.Text = "Edit Record";
                        SetData();
                        mainPage.SetPage(pageDetail);
                        bunifuTransition1.HideSync(pnlSide, false, BunifuAnimatorNS.Animation.Transparent);

                    }
                    else
                    {
                        supplyClassID = int.Parse(gridList.SelectedRows[0].Cells[1].Value.ToString());
                        supplyTypeID = int.Parse(gridList.SelectedRows[0].Cells[2].Value.ToString());
                        Save();
                        SetData();
                    }

                }
                else
                {

                    LocalUtils.ShowNoRecordSelectedMessage(this);
                }
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.Message);
            }
        }
        private void SetData()
        {

            var obj = Factories.CreateSupplyInventory().GetBy(id);
            // populate data
            mainPage.SetPage(pageDetail);
            bunifuTransition1.HideSync(pnlSide, false, BunifuAnimatorNS.Animation.Transparent);

            dtDate.Value = obj.Date;
            txtUnitPrice.Text = obj.UnitPrice.ToString();
            txtSupplyType.Text = obj.SupplyTypeName;
            txtSupplyClass.Text = obj.supplyTypeClassName;
            supplyClassID = obj.SupplyClassID;
            supplyTypeID = obj.SupplyTypeID;
            txtPurchaseQty.Text = obj.PurchaseQuantity.ToString();
            txtPurchaseTotal.Text = obj.PurchaseTotalAmount.ToString();
            txtWithdrawQty.Text = obj.WithdrawQuantity.ToString();
            txtWithdrawTotal.Text = obj.WithdrawTotaAmount.ToString();
        }
        private void Save()
        {
                
                
                    var model = new SupplyInventoryModel();
                   
                    model.SupplyTypeID = supplyTypeID;
                    model.SupplyClassID = supplyClassID;
                    model.PurchaseQuantity = LocalUtils.ConvertToDouble(txtPurchaseQty.Text);
                    // get id
                    if (id != 0)
                    {
                        model.Date = dtDate.Value;
                        if (!ValidateFields())
                        {
                            LocalUtils.ShowValidationFailedMessage(this);
                            return;
                        }
                        Factories.CreateSupplyInventory().Edit(id, model);
                        LocalUtils.ShowSaveMessage(this);
                    }
                    else
                    {

                        model.Date = dtSearchDate.Value;
                        Factories.CreateSupplyInventory().Add(model);
                        id = model.ID;
                        mainPage.SetPage(pageDetail);
                        bunifuTransition1.HideSync(pnlSide, false, BunifuAnimatorNS.Animation.Transparent);
                    }
        }

        private void ShowMainGrid()
        {
            LoadMainGrid();
            mainPage.SetPage(tabGridList);

            
            bunifuTransition1.ShowSync(pnlSide, false, BunifuAnimatorNS.Animation.Transparent);

        }
        #endregion

        private void txtWithdrawQty_TextChanged(object sender, EventArgs e)
        {

        }









        void SetToolTip()
        {
           
            bunifuToolTip1.SetToolTip(btnEdit, "Edit");
            bunifuToolTip1.SetToolTip(btnDelete, "Delete");
        }








        public int supplyTypeID { get; set; }

        public int supplyClassID { get; set; }

        private void frmSupplyInventory_Shown(object sender, EventArgs e)
        {
            LoadMainGrid();
        }
    }
    

}
