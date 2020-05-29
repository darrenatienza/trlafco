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
using TRLAFCoSys.Logic;
using TRLAFCoSys.Logic.Contracts;
using TRLAFCoSys.Logic.Implementors;
using TRLAFCoSys.Logic.Models;
using TRLAFCoSys.Queries.Persistence;


namespace TRLAFCoSys.App.Forms
{
    public partial class frmProduction : Form
    {
        IMilkClassLogic logic;
        private int productionID;
        private string messageTitle;
        private int productRawMaterialID;
        public frmProduction()
        {
            InitializeComponent();
            logic = new MilkClassLogic();
            messageTitle = "Milk Class";
        }

        private void frmProduction_Load(object sender, EventArgs e)
        {
            LoadProductionGridList();
            LoadProducts();
            SetToolTip();
            AddHandlers();
        }
        
        private void gridList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_OnIconRightClick(object sender, EventArgs e)
        {
            LoadProductionGridList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            page.SetPage(pageUpdateProduction);
            ResetInputs(pageUpdateProduction);
            lblAddEditTitle.Text = "Add New Record";
            bunifuTransition1.HideSync(pnlSide,false,BunifuAnimatorNS.Animation.HorizBlind);
            
        }
       
        private  void LoadProducts()
        {
            try
            {
                var objs = Factories.CreateProduct().GetAll("");
                cboProducts.Items.Clear();
                cboProducts.Text = "";
                foreach (var item in objs)
                {
                    cboProducts.Items.Add(new ItemX(item.Name, item.ID.ToString()));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnAddEditClose_Click(object sender, EventArgs e)
        {
            LoadDefaultUI();
        }
       
        private void LoadProductionGridList()
        {
            try
            {
                var criteria = txtSearch.Text;
                var models = Factories.CreateProduction().GetAll(dtSearchDate.Value.Month,dtSearchDate.Value.Year,criteria);
                gridProductionList.Rows.Clear();
                int count = 0;
                foreach (var item in models)
                {
                    count++;
                    gridProductionList.Rows.Add(new string[] 
                    { 
                        item.ID.ToString(), 
                        count.ToString(), 
                        item.Date.ToShortDateString(),
                        item.ProductName, 
                        item.ProductionQuantity.ToString() });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
       
        #region Validations
        private bool ValidateFields(Control parent)
        {
            var textboxes = LocalUtils.GetAll(parent, typeof(BunifuTextBox))
                      .Where(c => c.CausesValidation == true).OrderBy(c => c.TabIndex);
            var dropdowns = LocalUtils.GetAll(parent, typeof(BunifuDropdown))
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
            // Use to automatically add text change validation on special type inputs like int, double etc value
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
        private void item_IntCheckTextChange(object sender, EventArgs e)
        {
            var control = (PlaceholderTextBox)sender;

            int value = 0;
            if (!int.TryParse(control.Text, out value))
            {
                control.Text = "0";
            }
        }

        void item_DoubleCheckTextChange(object sender, EventArgs e)
        {
            var control = (PlaceholderTextBox)sender;

            double value = 0;
            if (!double.TryParse(control.Text, out value))
            {
                control.Text = "0.0";
            }
        }

        #endregion
        
        private void LoadDefaultUI()
        {
            page.SetPage(pageMain);
           
            
            bunifuTransition1.ShowSync(pnlSide,false,BunifuAnimatorNS.Animation.Transparent);

        }
       

        private void btnEdit_Click(object sender, EventArgs e)
        {
            SetProductionData();
        }

        private void SetProductionData()
        {
            try
            {
                if (gridProductionList.SelectedRows.Count > 0)
                {
                    // get id
                    productionID = int.Parse(gridProductionList.SelectedRows[0].Cells[0].Value.ToString());
                    if (productionID != 0)
                    {

                        //show and hide controls
                        page.SetPage(pageUpdateProduction);
                        lblAddEditTitle.Text = "Edit Record";
                        bunifuTransition1.HideSync(pnlSide, false, BunifuAnimatorNS.Animation.Transparent);

                        // populate data
                        var obj = Factories.CreateProduction().Get(productionID);
                        dtDate.Value = obj.Date;
                        cboProducts.SelectedItem = LocalUtils.GetSelectedItemX(cboProducts.Items, obj.ProductID.ToString());
                        txtQuantity.Text = obj.Quantity.ToString();
                        
                        page.SetPage(pageUpdateProduction);
                        lblAddEditTitle.Text = "Edit Record";
                        bunifuTransition1.HideSync(pnlSide, false, BunifuAnimatorNS.Animation.Transparent);
                    }
                    else
                    {
                        LocalUtils.ShowNoRecordFoundMessage(this);
                        page.SetPage(pageUpdateProduction);
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
            LocalUtils.CenterControlXY(pageUpdateProduction, pnlAddEdit);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteProduction();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void DeleteProduction()
        {
            if (gridProductionList.SelectedRows.Count > 0)
            {
                productionID = int.Parse(gridProductionList.SelectedRows[0].Cells[0].Value.ToString());
                if (productionID != 0)
                {
                    DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Milk Collection", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        Factories.CreateProduction().Delete(productionID);
                        LocalUtils.ShowDeleteSuccessMessage(this);
                        LoadProductionGridList();
                        productionID = 0;
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
        private void ResetInputs()
        {
            var controls = LocalUtils.GetAll(pageUpdateProduction, typeof(BunifuTextBox));
            foreach (BunifuTextBox item in controls)
            {
                item.Text = string.Empty;
            }
        }
        private void ResetInputs(Control parent)
        {
            var textboxes = LocalUtils.GetAll(parent, typeof(BunifuTextBox));
            var dropdowns = LocalUtils.GetAll(parent, typeof(BunifuDropdown));
            var datepickers = LocalUtils.GetAll(parent, typeof(BunifuDatePicker));
            foreach (BunifuTextBox item in textboxes)
            {
                item.Text = string.Empty;
            }
            foreach (BunifuDropdown item in dropdowns)
            {
                item.Text = string.Empty;
            }
            foreach (BunifuDatePicker item in datepickers)
            {
                item.Value = DateTime.Now;
            }
        }
        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {

        }


        private void btnSaveProduction_Click(object sender, EventArgs e)
        {
            try
            {
                SaveProduction();
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
        }

        private void SaveProduction()
        {
           
                if (!ValidateFields(pageUpdateProduction))
                {
                    LocalUtils.ShowValidationFailedMessage(this);

                }
                else
                {

                    var model = new AddEditProductionModel();
                    model.Date = dtDate.Value;
                    model.ProductID = int.Parse(((ItemX)cboProducts.SelectedItem).Value);
                    model.Quantity = LocalUtils.ConvertToInteger(txtQuantity.Text);
                    // get id
                    if (productionID != 0)
                    {

                        Factories.CreateProduction().Edit(productionID, model);
                        
                    }
                    else
                    {

                        Factories.CreateProduction().Add(model);
                        productionID = model.ID;
                        
                    }
                    LoadProductionGridList();
                    LocalUtils.ShowSaveMessage(this);
                }

            
            
        }
        void SetToolTip()
        {
            bunifuToolTip1.SetToolTip(btnAdd, "Add");
            bunifuToolTip1.SetToolTip(btnEdit, "Edit");
            bunifuToolTip1.SetToolTip(btnDelete, "Delete");
        }
        private void gridList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

      

       

        private void btnCloseUpdateRawMaterial_Click(object sender, EventArgs e)
        {
            page.SetPage(pageUpdateProduction);
        }

       

      

        

        

    }
    

}
