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
    public partial class frmProduct : Form
    {
        IMilkClassLogic logic;
        private int productID;
        private string messageTitle;
        private int productRawMaterialID;
        public frmProduct()
        {
            InitializeComponent();
            logic = new MilkClassLogic();
            messageTitle = "Milk Class";
        }

        private void frmMilkProductRecord_Load(object sender, EventArgs e)
        {
            LoadProductsGridList();
            LoadSupplyClasses();
            AddHandlers();
            SetToolTip();
        }
        
        private void gridList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_OnIconRightClick(object sender, EventArgs e)
        {
            LoadProductsGridList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            page.SetPage(pageUpdateProduct);
            ResetInputs();
            lblAddEditTitle.Text = "Add New Record";
            productID = 0;
            gridProductRawMaterials.Rows.Clear();
            bunifuTransition1.HideSync(pnlSide,false,BunifuAnimatorNS.Animation.HorizBlind);
            
        }
        private void LoadSupplyClasses()
        {
            try
            {
                var objs = Factories.CreateSupplyClass().GetAll();
                cboSupplyClass.Items.Clear();
                foreach (var item in objs)
                {
                    cboSupplyClass.Items.Add(new ItemX(item.Description, item.ID.ToString()));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        void SetToolTip()
        {
            bunifuToolTip1.SetToolTip(btnAdd, "Add");
            bunifuToolTip1.SetToolTip(btnEdit, "Edit");
            bunifuToolTip1.SetToolTip(btnDelete, "Delete");
        }
        private  void LoadSupplyTypes()
        {
            try
            {
                var supplyClassID = int.Parse(((ItemX)cboSupplyClass.SelectedItem).Value);
                var objs = Factories.CreateSupplyType().GetAllBy(supplyClassID);
                cboSupplyTypes.Items.Clear();
                cboSupplyTypes.Text = "";
                foreach (var item in objs)
                {
                    cboSupplyTypes.Items.Add(new ItemX(item.Description, item.ID.ToString()));
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
       
        private void LoadProductsGridList()
        {
            try
            {
                var criteria = txtSearch.Text;
                var models = Factories.CreateProduct().GetAll(criteria);
                gridList.Rows.Clear();
                int count = 0;
                foreach (var item in models)
                {
                    count++;
                    gridList.Rows.Add(new string[] 
                    { 
                        item.ID.ToString(), 
                        count.ToString(), 
                        item.Name, 
                        item.RawMaterialsCount.ToString() });
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        private void LoadProductRawMaterialGridList()
        {
                var models = Factories.CreateProduct().GetAllProductRawMaterials(productID);
                gridProductRawMaterials.Rows.Clear();
                int count = 0;
                foreach (var item in models)
                {
                    count++;
                    gridProductRawMaterials.Rows.Add(new string[] 
                    { 
                        item.ID.ToString(), 
                        count.ToString(), 
                        item.Name, 
                        item.Quantity.ToString()});
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
            SetProductData();
        }

        private void SetProductData()
        {
            try
            {
                if (gridList.SelectedRows.Count > 0)
                {
                    // get id
                    productID = int.Parse(gridList.SelectedRows[0].Cells[0].Value.ToString());
                    if (productID != 0)
                    {

                        //show and hide controls
                        page.SetPage(pageUpdateProduct);
                        lblAddEditTitle.Text = "Edit Record";
                        bunifuTransition1.HideSync(pnlSide, false, BunifuAnimatorNS.Animation.Transparent);

                        // populate data
                        var model = Factories.CreateProduct().Get(productID);
                        productID = model.ID;
                        txtProductName.Text = model.Name;
                        txtUnitPrice.Text = model.UnitPrice.ToString();
                        chkProduce.Checked = model.IsProduce;
                        LoadProductRawMaterialGridList();
                        page.SetPage(pageUpdateProduct);
                        lblAddEditTitle.Text = "Edit Record";
                        bunifuTransition1.HideSync(pnlSide, false, BunifuAnimatorNS.Animation.Transparent);
                    }
                    else
                    {
                        LocalUtils.ShowNoRecordFoundMessage(this);
                        page.SetPage(pageUpdateProduct);
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

        private void SetProductRawMaterialData()
        {
            
                
                if (gridProductRawMaterials.SelectedRows.Count > 0)
                {
                    // get id
                    productRawMaterialID = int.Parse(gridProductRawMaterials.SelectedRows[0].Cells[0].Value.ToString());
                    if (productRawMaterialID != 0)
                    {

                        //show and hide controls
                        page.SetPage(pageUpdateProductRawMaterials);
                        lblAddEditTitle.Text = "Edit Record";
                        bunifuTransition1.HideSync(pnlSide, false, BunifuAnimatorNS.Animation.Transparent);

                        // populate data
                        var obj = Factories.CreateProduct().GetProductRawMaterial(productRawMaterialID);
                        cboSupplyClass.SelectedItem = LocalUtils.GetSelectedItemX(cboSupplyClass.Items, obj.SupplyClassID.ToString());
                        cboSupplyTypes.SelectedItem = LocalUtils.GetSelectedItemX(cboSupplyTypes.Items, obj.SupplyTypeID.ToString());
                        txtQuantity.Text = obj.Quantity.ToString();
                        
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
            LocalUtils.CenterControlXY(pageUpdateProduct, pnlAddEdit);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteProduct();

            }
            catch (Exception)
            {

                throw;
            }
        }

        private void DeleteProduct()
        {
            if (gridList.SelectedRows.Count > 0)
            {
                productID = int.Parse(gridList.SelectedRows[0].Cells[0].Value.ToString());
                if (productID != 0)
                {
                    DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Milk Collection", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        Factories.CreateProduct().Delete(productID);
                        LocalUtils.ShowDeleteSuccessMessage(this);
                        LoadProductsGridList();
                        productID = 0;
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
            var controls = LocalUtils.GetAll(pnlAddEdit, typeof(BunifuTextBox));
            foreach (BunifuTextBox item in controls)
            {
                item.Text = string.Empty;
            }
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void bunifuButton1_Click(object sender, EventArgs e)
        {

        }

        private void btnAddRawMaterials_Click(object sender, EventArgs e)
        {
            
            if (productID > 0)
            {
                var product = Factories.CreateProduct().Get(productID);
                if (!product.IsProduce && product.ProductRawMaterialCount > 0)
                {
                    LocalUtils.ShowErrorMessage(this, "Only product which is not produced must contain 1 raw material;");
                }
                else
                {
                    page.SetPage(pageUpdateProductRawMaterials);
                }
               
            }
            else
            {
                LocalUtils.ShowErrorMessage(this, "Save new product first before adding raw materials!");
            }
            
        }

        private void cboSupplyClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            LoadSupplyTypes();
        }

        private void btnSaveProduct_Click(object sender, EventArgs e)
        {
            try
            {
                SaveProduct();
            }
            catch (Exception ex)
            {
                
                throw;
            }
            
        }

        private void SaveProduct()
        {
            try
            {
                if (!ValidateFields(pageUpdateProduct))
                {
                    LocalUtils.ShowValidationFailedMessage(this);

                }
                else
                {

                    var model = new ProductModel();
                    model.Name = txtProductName.Text;
                    model.UnitPrice = LocalUtils.ConvertToDouble(txtUnitPrice.Text);
                    model.IsProduce = chkProduce.Checked;
                    // get id
                    if (productID != 0)
                    {

                        Factories.CreateProduct().Edit(productID, model);
                        LoadProductsGridList();
                        LocalUtils.ShowSaveMessage(this);
                    }
                    else
                    {

                        Factories.CreateProduct().Add(model);
                        productID = model.ID;
                        LoadProductsGridList();
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

        private void gridList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnSaveRawMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                SaveProductRawMaterial();
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.Message);
            }
           
        }

        private void SaveProductRawMaterial()
        {
            try
            {
                if (!ValidateFields(pageUpdateProductRawMaterials))
                {
                    LocalUtils.ShowValidationFailedMessage(this);

                }
                else
                {

                    var model = new AddEditProductRawMaterialModel();
                    model.ProductID = productID;
                    model.SupplyTypeID = int.Parse(((ItemX)cboSupplyTypes.SelectedItem).Value);
                    model.Quantity = LocalUtils.ConvertToDouble(txtQuantity.Text);
                    // get id
                    if (productRawMaterialID != 0)
                    {
                        Factories.CreateProduct().EditProductRawMaterial(productRawMaterialID, model);
                    }
                    else
                    {
                        Factories.CreateProduct().AddProductRawMaterial(model);
                    }
                    LoadProductRawMaterialGridList();
                    LocalUtils.ShowSaveMessage(this);
                }
                page.SetPage(pageUpdateProduct);

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

        private void btnCloseUpdateRawMaterial_Click(object sender, EventArgs e)
        {
            page.SetPage(pageUpdateProduct);
        }

        private void btnEditRawMaterial_Click(object sender, EventArgs e)
        {
            try
            {
                SetProductRawMaterialData();
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this,ex.ToString());
            }
        }

      

        private void btnDeleteRawMaterials_Click(object sender, EventArgs e)
        {
            try
            {
                DeleteProductRawMaterial();
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.ToString());
            }
        }

        private void DeleteProductRawMaterial()
        {
            if (gridProductRawMaterials.SelectedRows.Count > 0)
            {
                productRawMaterialID = int.Parse(gridProductRawMaterials.SelectedRows[0].Cells[0].Value.ToString());
                if (productRawMaterialID != 0)
                {
                    DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Milk Collection", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        Factories.CreateProduct().DeleteProductRawMaterial(productRawMaterialID);
                        LocalUtils.ShowDeleteSuccessMessage(this);
                        LoadProductRawMaterialGridList();
                        productRawMaterialID = 0;
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

        private void txtQuantity_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void txtUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

    }
    

}
