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
    public partial class frmFarmersRecord : Form
    {
        IFarmerLogic farmerLogic;
        private int id;
        private string messageTitle;
        public frmFarmersRecord()
        {
            InitializeComponent();
            farmerLogic = new FarmerLogic();
            messageTitle = "Farmers Record";
        }

        private void frmMilkProductRecord_Load(object sender, EventArgs e)
        {
            LoadFarmerList();
            AddHandlers();
            SetToolTip();
        }
        
        private void gridList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_OnIconRightClick(object sender, EventArgs e)
        {
            LoadFarmerList();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(tabPage2);
            ResetInputs();
            lblAddEditTitle.Text = "Add New Record";
            bunifuTransition1.HideSync(pnlSide,false,BunifuAnimatorNS.Animation.HorizBlind);
            
        }

        private void btnAddEditClose_Click(object sender, EventArgs e)
        {
            LoadDefaultUI();
        }
       
        private void LoadFarmerList()
        {
            try
            {
                var criteria = txtSearch.Text;
                var farmerList = farmerLogic.GetRecords(criteria);
                gridList.Rows.Clear();
                int count = 0;
                foreach (var item in farmerList)
                {
                    count++;
                    gridList.Rows.Add(new string[] { item.ID.ToString(),count.ToString(), item.FullName, item.Address, item.PhoneNumber});
                }
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
        private void AddHandlers()
        {
            var textboxes = LocalUtils.GetAll(pnlAddEdit, typeof(BunifuTextBox))
                     .Where(c => c.CausesValidation == true).OrderBy(c => c.TabIndex);
            foreach (BunifuTextBox item in textboxes)
            {
                if (item.Tag != null)
                {


                    if (item.Tag.ToString().Contains("integer"))
                    {
                        item.TextChange += item_IntCheckTextChange;
                    }
                    else
                        if (item.Tag.ToString().Contains("double"))
                        {
                            item.TextChange += item_DoubleCheckTextChange;
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
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateFields())
                {
                    //id greater than zero = edit
                    //id equal to zero = add
                    if (id == 0)
                    {
                        var model = new AddFarmerModel();
                        model.Address = txtAddress.Text;
                        model.FullName = txtFullName.Text;
                        model.PhoneNumber = txtPhoneNumber.Text;
                        farmerLogic.Add(model);
                        MetroMessageBox.Show(this, "Record has been saved!", messageTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDefaultUI();
                        LoadFarmerList();
                    }
                    else
                    {
                        var model = new EditFarmerModel();
                        model.Address = txtAddress.Text;
                        model.FullName = txtFullName.Text;
                        model.PhoneNumber = txtPhoneNumber.Text;
                        farmerLogic.Edit(id,model);
                        MetroMessageBox.Show(this, "Record has been saved!",messageTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDefaultUI();
                        LoadFarmerList();
                    }
                }
                else
                {
                    // Validation error
                    MetroMessageBox.Show(this, "Invalid Field(s)!", messageTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        private void LoadDefaultUI()
        {
            bunifuPages1.SetPage(tabPage1);
           
            
            bunifuTransition1.ShowSync(pnlSide,false,BunifuAnimatorNS.Animation.Transparent);

        }
       

        private void btnEdit_Click(object sender, EventArgs e)
        {
            id = gridList.SelectedRows[0].Cells[0].Value.ToString() == "" 
                ? 0 
                : int.Parse(gridList.SelectedRows[0].Cells[0].Value.ToString());
            if (id == 0)
            {
                MetroMessageBox.Show(this, "No record selected!", "Milk Collection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            
            bunifuPages1.SetPage(tabPage2);
            lblAddEditTitle.Text = "Edit Record";
            SetData();
            bunifuTransition1.HideSync(pnlSide, false, BunifuAnimatorNS.Animation.Transparent);
           
        }
        void SetToolTip()
        {
            bunifuToolTip1.SetToolTip(btnAdd, "Add");
            bunifuToolTip1.SetToolTip(btnEdit, "Edit");
            bunifuToolTip1.SetToolTip(btnDelete, "Delete");
        }
        private void SetData()
        {
            try
            {
                var model = farmerLogic.GetRecord(id);
                txtAddress.Text = model.Address;
                txtFullName.Text = model.FullName;
                txtPhoneNumber.Text = model.PhoneNumber;

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
            LocalUtils.CenterControlXY(tabPage2, pnlAddEdit);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                id = gridList.SelectedRows[0].Cells[0].Value.ToString() == ""
                ? 0
                : int.Parse(gridList.SelectedRows[0].Cells[0].Value.ToString());
                if (id == 0)
                {
                    MetroMessageBox.Show(this, "No record selected!", messageTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Milk Collection", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    farmerLogic.Delete(id);
                    LoadFarmerList();
                    MetroMessageBox.Show(this, "Record has been deleted!", messageTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDefaultUI();
                    id = 0;
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

        private void txtPhoneNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            //if ((e.KeyChar == '.') && ((sender as TextBox).Text.IndexOf('.') > -1))
            //{
            //    e.Handled = true;
            //}
        }

    }
    

}
