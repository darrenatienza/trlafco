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
    public partial class frmProductSale : Form, IFormActions
    {
        private int productSaleID;

        public frmProductSale()
        {
            InitializeComponent();
        }

        private void frmProductSale_Load(object sender, EventArgs e)
        {
            LoadMainGrid();
            LoadProducts();
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
            LoadMainGrid();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();

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

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }



        private void txtPurchaseQty_OnIconRightClick(object sender, EventArgs e)
        {


        }




        public bool ValidateFields()
        {
            var textboxes = LocalUtils.GetAll(pageDetail, typeof(BunifuTextBox))
                      .Where(c => c.CausesValidation == true).OrderBy(c => c.TabIndex);
            var dropdowns = LocalUtils.GetAll(pageDetail, typeof(BunifuDropdown))
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
            ComputeTotal();
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
        public void Add()
        {
            try
            {
                mainPage.SetPage(pageDetail);
                ResetInputs();
                productSaleID = 0;

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
                if (gridMain.SelectedRows.Count > 0)
                {
                    // get id
                    productSaleID = int.Parse(gridMain.SelectedRows[0].Cells[0].Value.ToString());
                    if (productSaleID != 0)
                    {
                        //show and hide controls
                        mainPage.SetPage(pageDetail);
                        bunifuTransition1.HideSync(pnlSide, false, BunifuAnimatorNS.Animation.Transparent);
                        SetData();
                    }
                    else
                    {
                        LocalUtils.ShowNoRecordFoundMessage(this);
                        mainPage.SetPage(pageGridMain);
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
                if (gridMain.SelectedRows.Count > 0)
                {
                    productSaleID = int.Parse(gridMain.SelectedRows[0].Cells[0].Value.ToString());
                    if (productSaleID != 0)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Milk Collection", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            Factories.CreateProductSale().Delete(productSaleID);
                            LocalUtils.ShowDeleteSuccessMessage(this);
                            ShowMainGrid();
                            productSaleID = 0;
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


        public void LoadMainGrid()
        {
            try
            {
                var criteria = txtSearch.Text;
                var date = dtSearchDate.Value;

                var models = Factories.CreateProductSale().GetAllByMonth(date, criteria);
                gridMain.Rows.Clear();
                int count = 0;
                foreach (var item in models)
                {
                    count++;
                    gridMain.Rows.Add(new string[] { item.ID.ToString(), count.ToString(), 
                            
                            item.Date.ToShortDateString(),
                            item.CustomerName,
                            item.ProductName,
                            item.Quantity.ToString(),
                            item.Total.ToString(),
                            
                    });
                }

            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }
        }

        public void ResetInputs()
        {
            var textboxes = LocalUtils.GetAll(pageDetail, typeof(BunifuTextBox));
            foreach (BunifuTextBox item in textboxes)
            {
                item.Text = string.Empty;
            }
            var comboboxes = LocalUtils.GetAll(pageDetail, typeof(BunifuDropdown));
            foreach (BunifuDropdown item in comboboxes)
            {
                item.Text = string.Empty;
            }
            var chks = LocalUtils.GetAll(pageDetail, typeof(BunifuCheckBox));
            foreach (BunifuCheckBox item in chks)
            {
                item.Checked = false;
            }
        }

        public void SetData()
        {
            try
            {

                // populate data
                var model = Factories.CreateProductSale().Get(productSaleID);
                dtDate.Value = model.Date;

                cboProducts.SelectedItem = LocalUtils.GetSelectedItemX(cboProducts.Items, model.ProductID.ToString());
                txtCustomerName.Text = model.CustomerName;
                txtQuantity.Text = model.Quantity.ToString();
                txtUnitPrice.Text = model.UnitPrice.ToString();
                txtDiscount.Text = model.Discount.ToString();
                cboOutletSaleName.Text = model.OutletSaleName;
                chkIsBuyOneTakeOne.Checked = model.IsBuyOneTakeOne;
                txtAdditionalQuantity.Text = model.AdditionalQty.ToString();
                

            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }

        }
        public void Save()
        {
            try
            {
                if (!ValidateFields(pnlUpdate))
                {
                    LocalUtils.ShowValidationFailedMessage(this);

                }
                else
                {
                    var model = new ProductSaleModel();
                    model.CustomerName = txtCustomerName.Text;
                    model.Date = dtDate.Value;
                    model.Discount = LocalUtils.ConvertToDouble(txtDiscount.Text);
                    model.IsBuyOneTakeOne = chkIsBuyOneTakeOne.Checked;
                    model.Quantity = LocalUtils.ConvertToDouble(txtQuantity.Text);
                    model.UnitPrice = LocalUtils.ConvertToDouble(txtUnitPrice.Text);
                    model.OutletSaleName = cboOutletSaleName.Text;
                    model.ProductID = int.Parse(((ItemX)cboProducts.SelectedItem).Value);
                    // get id
                    if (productSaleID != 0)
                    {

                        Factories.CreateProductSale().Edit(productSaleID, model);
                        LocalUtils.ShowSaveMessage(this);
                    }
                    else
                    {

                        Factories.CreateProductSale().Add(model);
                        productSaleID = model.ID;
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
            mainPage.SetPage(pageGridMain);
            LoadMainGrid();
            bunifuTransition1.ShowSync(pnlSide, false, BunifuAnimatorNS.Animation.Transparent);

        }
        void LoadProducts()
        {
            try
            {
                var models = Factories.CreateProduct().GetAll("");
                foreach (var item in models)
                {
                    cboProducts.Items.Add(new ItemX(item.Name, item.ID.ToString()));
                }
            }
            catch (Exception ex)
            {
                
                throw;
            }
        }
        void ComputeTotal()
        {
            var quantity = LocalUtils.ConvertToDouble(txtQuantity.Text);
            var unitPrice = LocalUtils.ConvertToDouble(txtUnitPrice.Text);
            var discount = LocalUtils.ConvertToDouble(txtDiscount.Text);
            double total = Factories.CreateProductSale().ComputeTotal(quantity,unitPrice,discount);
            txtTotal.Text = total.ToString();
        }

        private void chkIsBuyOneTakeOne_CheckedChanged(object sender, BunifuCheckBox.CheckedChangedEventArgs e)
        {
            
            if (chkIsBuyOneTakeOne.Checked)
            {
                if (txtQuantity.Text != string.Empty)
                {
                    txtAdditionalQuantity.Text = (LocalUtils.ConvertToDouble(txtQuantity.Text) * 2.0).ToString();

                }
                else
                {
                    LocalUtils.ShowErrorMessage(this, "Quantity cannot be empty!");
                    chkIsBuyOneTakeOne.Checked = false;
                    txtQuantity.Text = "0";
                }
            }
            else
            {
                txtAdditionalQuantity.Text = "0";
            }
        }

        private void btnFormClose_Click(object sender, EventArgs e)
        {
            ShowMainGrid();
        }

        private void cboProducts_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetProductUnitPrice();
        }

        private void SetProductUnitPrice()
        {
            var productID = int.Parse(((ItemX)cboProducts.SelectedItem).Value);
            if (productID > 0)
            {
                var product = Factories.CreateProduct().Get(productID);
                txtUnitPrice.Text = product.UnitPrice.ToString();
            }
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUnitPrice_TextChanged(object sender, EventArgs e)
        {

        }

       
    }
    

}
