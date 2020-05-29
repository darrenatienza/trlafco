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
    public partial class frmMilkDeliveryUtilization : Form
    {
       
        private int milkUtilizeRecordID = 0;
        
        public frmMilkDeliveryUtilization()
        {
            InitializeComponent();
           
           
        }

        private void frmMilkProductRecord_Load(object sender, EventArgs e)
        {
            LoadMainGridList();
            AddHandlers();
            SetToolTip();
        }
        void SetToolTip()
        {
            bunifuToolTip1.SetToolTip(btnAdd, "Add");
            bunifuToolTip1.SetToolTip(btnEdit, "Edit");
            bunifuToolTip1.SetToolTip(btnDelete, "Delete");
        }
        private void gridList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_OnIconRightClick(object sender, EventArgs e)
        {
            LoadMainGridList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {


            mainPage.SetPage(pageAddNewDateSelection);
            
        }

        private void btnAddEditClose_Click(object sender, EventArgs e)
        {
            // we close the current page to show the main grid
            ShowMainGrid();
        }
       
        private void LoadMainGridList()
        {
            try
            {
                
                var list = Factories.CreateMilkUtilize().GetRecords(dtSearchDate.Value);
                gridList.Rows.Clear();
                int count = 0;
                foreach (var item in list)
                {
                    count++;
                    gridList.Rows.Add(new string[] { item.ID.ToString(), count.ToString(),
                        item.ActualDate.ToShortDateString(), 
                        item.BeginningVolume.ToString(),
                        item.MilkDeliveredFromFarmers.ToString(),
                        item.TotalMilkForUtilization.ToString(), 
                        item.TotalRawMilkWithdrawn.ToString(),
                        item.Spillage.ToString(),
                        item.Analysis.ToString(),
                        item.EndingVolumeBalance.ToString() });
                }
                // add space
                gridList.Rows.Add(new string[] { });

                var summary = Factories.CreateMilkUtilize().GetSummary(dtSearchDate.Value);

                gridList.Rows.Add(new string[] { "", "",
                       "", 
                        "Milk Delivered from Farmers",
                        "Raw Milk Sold", 
                        "Raw Milk Process", 
                        "Total Milk Withdrawn",
                 "Analysis"});
                gridList.Rows.Add(new string[] { "", "",
                        "Total",
                        summary.MilkCollectionPerMonth.ToString(),
                        summary.MilkSoldPerMonth.ToString(), 
                        summary.MilkProcessPerMonth.ToString(), 
                        summary.MilkWithdrawnPerMonth.ToString(),
                summary.MilkAnalysisPerMonth.ToString()});
                // add space
                gridList.Rows.Add(new string[] {});

               
            }
            catch (Exception)
            {

                throw;
            }
        }

        #region Validations
        private bool ValidateFields()
        {
            var textboxes = LocalUtils.GetAll(pnlAddEdit, typeof(BunifuTextBox))
                      .Where(c => c.CausesValidation == true).OrderBy(c => c.TabIndex);
            var dropdowns = LocalUtils.GetAll(pnlAddEdit, typeof(BunifuDropdown))
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

        private void Compute()
        {
            try
            {

                
                var model = new MilkUtilizeComputeModel();
                model.TotalMilkForUtilization = LocalUtils.ConvertToDouble(txtTotalMilkForUtilization.Text);
                model.TotalMilkSold = LocalUtils.ConvertToDouble(txtTotalRawMilkSold.Text);
                model.RawMilkProcess = LocalUtils.ConvertToDouble(txtRawMilkProcess.Text);
                model.Spillage = LocalUtils.ConvertToDouble(txtSpillage.Text);
                model.Analysis = LocalUtils.ConvertToDouble(txtAnalysis.Text);
                var result  = Factories.CreateMilkUtilize().Compute(model);
                // Get the result
                txtTotalRawMilkWithdrawn.Text = result.TotalRawMilkWithdrawn.ToString();
                txtEndingBalance.Text = result.EndingBalance.ToString();
            }
            catch (Exception)
            {
                
                throw;
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

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        private void Save()
        {

                
                    //id greater than zero = edit
                    //id equal to zero = add
                    if (milkUtilizeRecordID == 0)
                    {
                        // Add new record
                        var model = new MilkUtilizeAddModel();

                        model.ActualDate = dtDateSelection.Value;
                        //model.Analysis = LocalUtils.ConvertToDouble(txtAnalysis.Text);
                        //model.Demo = LocalUtils.ConvertToInteger(txtDemo.Text);
                        //model.Remarks = txtRemarks.Text;
                        //model.Spillage = LocalUtils.ConvertToDouble(txtSpillage.Text);
                        //model.SpoilageQty = LocalUtils.ConvertToDouble(txtSpoilageQty.Text);
                        //model.SpoilageValue = LocalUtils.ConvertToDouble(txtSpoilageValue.Text);
                        //model.WithdrawnByProcessor = txtWithdrawnByProcessor.Text;
                        //model.Remarks = txtRemarks.Text;
                        Factories.CreateMilkUtilize().Add(model);
                        milkUtilizeRecordID = model.ID;

                    }
                    else
                    {
                        if (!ValidateFields())
                        {
                            LocalUtils.ShowValidationFailedMessage(this);
                            return;
                        }
                        // Edit record
                        var model = new MilkUtilizeEditModel();
                        model.Analysis = LocalUtils.ConvertToDouble(txtAnalysis.Text);
                        model.Demo = LocalUtils.ConvertToInteger(txtDemo.Text);
                        model.Remarks = txtRemarks.Text;
                        model.Spillage = LocalUtils.ConvertToDouble(txtSpillage.Text);
                        model.SpoilageQty = LocalUtils.ConvertToDouble(txtSpoilageQty.Text);
                        model.SpoilageValue = LocalUtils.ConvertToDouble(txtSpoilageValue.Text);
                        model.WithdrawnByProcessor = txtWithdrawnByProcessor.Text;
                        model.Remarks = txtRemarks.Text;
                        Factories.CreateMilkUtilize().Edit(milkUtilizeRecordID, model);
                        mainPage.SetPage(pageList);

                    }

                    LocalUtils.ShowSaveMessage(this);
                
                
            
        }
        /// <summary>
        /// Use to get initial data when adding new milk utilization
        /// </summary>
        private void SetInitialAddData()
        {
            try
            {
                var initialDate = dtDate.Value;
                var model = Factories.CreateMilkUtilize().GetRecord(date: dtDate.Value);
                if (model.ID == 0)
                {

                    txtBeginningBalance.Text = model.BeginningVolumeBalance.ToString();
                    txtEndingBalance.Text = model.EndingVolumeBalance.ToString();
                    txtRawMilkProcess.Text = model.RawMilkProcess.ToString();
                    txtTotalRawMilkSold.Text = model.RawMilkSold.ToString();
                    txtTotalMilkDeliveredByFarmers.Text = model.TotalMilkDeliveredFromFarmers.ToString();
                    txtTotalMilkForUtilization.Text = model.TotalMilkForUtilization.ToString();
                    txtTotalRawMilkWithdrawn.Text = model.TotalRawMilkWithdrawn.ToString();

                }
                else
                {
                    dtDate.Value = initialDate;
                    LocalUtils.ShowErrorMessage(this, "Record Already exists for this date!");
                }
                
            }
            catch (Exception)
            {

               
            }
        }
        private void ShowMainGrid()
        {
            mainPage.SetPage(pageList);

            LoadMainGridList();
            bunifuTransition1.ShowSync(pnlSide,false,BunifuAnimatorNS.Animation.Transparent);

        }
       

        private void btnEdit_Click(object sender, EventArgs e)
        {
            var gridCount = gridList.SelectedRows.Count;
            var gridValue = gridList.SelectedRows[0].Cells[0].Value.ToString();
            if ( gridCount > 0 && gridValue != "")
            {
                milkUtilizeRecordID = int.Parse(gridValue);
                mainPage.SetPage(pageMainDetail);
                lblAddEditTitle.Text = "Edit Record";
                SetData();
                
                bunifuTransition1.HideSync(pnlSide, false, BunifuAnimatorNS.Animation.Transparent);
            }
            else
            {
                LocalUtils.ShowErrorMessage(this, "No record selected!");
                return;
            }
            
           
        }
        private void SetData()
        {
            try
            {
                var model = Factories.CreateMilkUtilize().GetRecord(milkUtilizeRecordID);
                if (model != null)
                {
                    dtDate.Value = model.ActualDate;
                    txtBeginningBalance.Text = model.BeginningVolumeBalance.ToString();
                    txtTotalMilkDeliveredByFarmers.Text = model.TotalMilkDeliveredFromFarmers.ToString();
                    txtTotalMilkForUtilization.Text = model.TotalMilkForUtilization.ToString();
                    txtTotalRawMilkSold.Text = model.RawMilkSold.ToString();
                    txtWithdrawnByProcessor.Text = model.WithdrawnByProcessor;
                    txtRawMilkProcess.Text = model.RawMilkProcess.ToString();
                    txtTotalRawMilkWithdrawn.Text = model.TotalRawMilkWithdrawn.ToString();
                    txtSpillage.Text = model.Spillage.ToString();
                    txtAnalysis.Text = model.Analysis.ToString();
                    txtEndingBalance.Text = model.EndingVolumeBalance.ToString();
                    txtDemo.Text = model.Demo.ToString();
                    txtSpoilageQty.Text = model.SpoilageQty.ToString();
                    txtSpoilageValue.Text = model.SpoilageValue.ToString();
                    txtRemarks.Text = model.Remarks;
                }
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

        private void tabPage2_Resize(object sender, EventArgs e)
        {
            LocalUtils.CenterControlXY(pageMainDetail, pnlAddEdit);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridList.SelectedRows.Count > 0)
                {
                    milkUtilizeRecordID = int.Parse(gridList.SelectedRows[0].Cells[0].Value.ToString());
                    if (milkUtilizeRecordID != 0)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Milk Collection", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            Factories.CreateMilkUtilize().Delete(milkUtilizeRecordID);
                            LocalUtils.ShowDeleteSuccessMessage(this);
                            ShowMainGrid();
                            milkUtilizeRecordID = 0;
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
        private void ResetInputs()
        {
            var controls = LocalUtils.GetAll(pnlAddEdit, typeof(BunifuTextBox));
            foreach (BunifuTextBox item in controls)
            {
                item.Text = string.Empty;
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            LoadMainGridList();
        }
        private void btnCloseAddEditRawMilkSold_Click(object sender, EventArgs e)
        {
            ShowAddRecordUI();
        }

      
        private void ShowAddRecordUI()
        {
            mainPage.SetPage(pageMainDetail);
           
        }
        
        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
       

        private void btnSaveProduct_Click(object sender, EventArgs e)
        {
          
        }



        private void btnAddEditProductClose_Click(object sender, EventArgs e)
        {
            mainPage.SetPage(pageMainDetail);
        }
      
        

        

       


        

        

        private void dtDate_ValueChanged(object sender, EventArgs e)
        {
            
            
        }

        private void gridCustomer_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtSearchDate_ValueChanged(object sender, EventArgs e)
        {
            LoadMainGridList();
        }

        private void btnCreateNewRecord_Click(object sender, EventArgs e)
        {
            try
            {
                // checks record if exists on date selected
                CheckRecordIfExistsByDate(dtDateSelection.Value);

                ResetInputs();
                milkUtilizeRecordID = 0;
                Save(); // save the new record here.
                SetData();
                mainPage.SetPage(pageMainDetail);
                bunifuTransition1.HideSync(pnlSide, false, BunifuAnimatorNS.Animation.HorizBlind);
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.Message);
            }
        }

        private void btnDateSelectionClose_Click(object sender, EventArgs e)
        {
            ShowMainGrid();
        }

        private void dtDateSelection_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void CheckRecordIfExistsByDate(DateTime date)
        {
           
                var isExists = Factories.CreateMilkUtilize().CheckRecordIfExists(date);
                if (isExists)
                {
                    throw new Exception("Record Already Exists!");
                }
               
           
        }

        

       












    }
    

}
