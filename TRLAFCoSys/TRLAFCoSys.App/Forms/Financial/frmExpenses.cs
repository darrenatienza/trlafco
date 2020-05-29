using Bunifu.UI.WinForms;
using Bunifu.UI.WinForms.BunifuTextbox;
using MetroFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using TRLAFCoSys.App.Properties;
using TRLAFCoSys.Logic.Contracts;
using TRLAFCoSys.Logic.Implementors;
using TRLAFCoSys.Logic.Models;
using TRLAFCoSys.Queries.Persistence;


namespace TRLAFCoSys.App.Forms
{
    public partial class frmExpenses : Form
    {
        private int expenseID;
        private IExpenseLogic mainLogic;
        private IExpenseTypeLogic expenseTypeLogic;
        public frmExpenses()
        {
            InitializeComponent();
            mainLogic = new ExpenseLogic();
            expenseTypeLogic = new ExpenseTypeLogic();
        }
        void SetToolTip()
        {
            bunifuToolTip1.SetToolTip(btnAdd, "Add");
            bunifuToolTip1.SetToolTip(btnEdit, "Edit");
            bunifuToolTip1.SetToolTip(btnDelete, "Delete");
        }
        private void frmMilkProductRecord_Load(object sender, EventArgs e)
        {
            LoadMainGridList();
            LoadExpenseTypes();
            AddHandlers();
            SetToolTip();
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
            mainPage.SetPage(tabMainData);
            ResetInputs();
            lblAddEditTitle.Text = "Add New Record";
            expenseID = 0;
            bunifuTransition1.HideSync(pnlSide,false,BunifuAnimatorNS.Animation.HorizBlind);
            
        }

        private void btnAddEditClose_Click(object sender, EventArgs e)
        {
            // we close the current page to show the main grid
            ShowMainGrid();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveMainData();
        }
        private void btnEdit_Click(object sender, EventArgs e)
        {

                SetMainData();
               
                
               
            
            
           
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
            LocalUtils.CenterControlXY(tabMainData, pnlMainData);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridList.SelectedRows.Count > 0)
                {
                    expenseID = int.Parse(gridList.SelectedRows[0].Cells[0].Value.ToString());
                    if (expenseID != 0)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Milk Collection", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            mainLogic.Delete(expenseID);
                            LocalUtils.ShowDeleteSuccessMessage(this);
                            ShowMainGrid();
                            expenseID = 0;
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

        private void Compute()
        {
            try
            {
                var quantity = LocalUtils.ConvertToInteger(txtQuantity.Text);
                var unitprice = LocalUtils.ConvertToDouble(txtUnitPrice.Text);

                var total = mainLogic.ComputeTotal(quantity, unitprice);
                txtTotal.Text = total.ToString();

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

        private void LoadExpenseTypes()
        {
            try
            {
                var objs = expenseTypeLogic.GetAll();
                cboExpenseType.Items.Clear();
                foreach (var item in objs)
                {
                    cboExpenseType.Items.Add(new ItemX(item.Description, item.ID.ToString()));
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        private void LoadMainGridList()
        {
            try
            {
                var criteria = txtSearch.Text;
                var date = dtSearchDate.Value;
                IEnumerable<ExpenseListModel> models;
                if (radioByDate.Checked)
                {
                    models = mainLogic.GetAllBy("date",date, criteria);
                }
                else
                {
                    models = mainLogic.GetAllBy("month", date, criteria);
                }
               
                gridList.Rows.Clear();
                int count = 0;
                foreach (var item in models)
                {
                        count++;
                        gridList.Rows.Add(new string[] { item.ID.ToString(), count.ToString(), 
                            item.Date.ToShortDateString(),
                            item.Particular, 
                            item.ExpenseTypeDescription,
                            item.UnitPrice.ToString(), 
                            item.Quantity.ToString(),
                            item.Total.ToString()});
                }
                // use 0 id to avoid null exception while click edit button
                //Adds space
                gridList.Rows.Add(new string[] {"0" });

                    gridList.Rows.Add(new string[] { "0","", 
                            "Summaries",
                           "Description",
                            "Total", 
                           });
              
                var summaryModels = mainLogic.GetAllSummary(date);
                int summaryCount = 0;
                foreach (var item in summaryModels)
                {
                    summaryCount++;
                    gridList.Rows.Add(new string[] { "0","", 
                            "",
                            item.ExpenseTypeDescription,
                            item.Total.ToString(), 
                           });
                }

            }
            catch (Exception)
            {

                throw;
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
        
        private void SetMainData()
        {
            try
            {
                if (gridList.SelectedRows.Count > 0)
                {
                    // get id
                    expenseID = int.Parse(gridList.SelectedRows[0].Cells[0].Value.ToString());
                    if (expenseID != 0)
                    {
                        //show and hide controls
                        mainPage.SetPage(tabMainData);
                        lblAddEditTitle.Text = "Edit Record";
                        bunifuTransition1.HideSync(pnlSide, false, BunifuAnimatorNS.Animation.Transparent);

                        var obj = mainLogic.GetBy(expenseID);
                        dtDate.Value = obj.Date;
                        txtParticulars.Text = obj.Particular;
                        txtTotal.Text = obj.Total.ToString();
                        txtUnitPrice.Text = obj.UnitPrice.ToString();
                        txtQuantity.Text = obj.Quantity.ToString();
                        cboExpenseType.SelectedItem = LocalUtils.GetSelectedItemX(cboExpenseType.Items, obj.ExpenseTypeID.ToString());


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
            catch (Exception)
            {
                
                throw;
            }
           
        }
        private void SaveMainData()
        {
            try
            {
                if (!ValidateFields())
                {
                    LocalUtils.ShowValidationFailedMessage(this);

                }
                else
                {
                    // get id
                    if (expenseID != 0)
                    {
                        var model = new ExpenseModel();
                        model.Date = dtDate.Value;
                        model.Particular = txtParticulars.Text;
                        model.ExpenseTypeID = int.Parse(((ItemX)cboExpenseType.SelectedItem).Value);
                        model.UnitPrice = double.Parse(txtUnitPrice.Text);
                        model.Quantity = int.Parse(txtQuantity.Text);
                        mainLogic.Edit(expenseID, model);
                        LocalUtils.ShowSaveMessage(this);
                    }
                    else
                    {
                        var model = new ExpenseModel();
                        model.Date = dtDate.Value;
                        model.Particular = txtParticulars.Text;
                        model.ExpenseTypeID = int.Parse(((ItemX)cboExpenseType.SelectedItem).Value);
                        model.UnitPrice = double.Parse(txtUnitPrice.Text);
                        model.Quantity = int.Parse(txtQuantity.Text);
                        mainLogic.Add(model);
                        expenseID = model.ID;
                        LocalUtils.ShowSaveMessage(this);
                    }
                }

            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }
        }

        private void ShowMainGrid()
        {
            mainPage.SetPage(tabGridList);

            LoadMainGridList();
            bunifuTransition1.ShowSync(pnlSide, false, BunifuAnimatorNS.Animation.Transparent);

        }
        #endregion

        private void txtQuantity_TextChanged(object sender, EventArgs e)
        {

        }
        

       












    }
    

}
