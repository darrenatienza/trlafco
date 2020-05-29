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
    public partial class frmUsers : Form
    {
       
        private int userID;
        public frmUsers()
        {
            InitializeComponent();
            
        }

        
       
        private void ResetInputs(Control parent)
        {
            var controls = LocalUtils.GetAll(parent, typeof(BunifuTextBox));
            foreach (BunifuTextBox item in controls)
            {
                item.Text = string.Empty;
            }
            var dropdowns = LocalUtils.GetAll(parent, typeof(BunifuDropdown));
            foreach (BunifuDropdown item in dropdowns)
            {
                item.Text = string.Empty;
            }
            var datepickers = LocalUtils.GetAll(parent, typeof(BunifuDatePicker));
            foreach (BunifuDatePicker item in datepickers)
            {
                item.Value = DateTime.Now;
            }
        }
      

        private void txtSearch_OnIconRightClick(object sender, EventArgs e)
        {
            LoadMainGridList();
        }

        private void LoadMainGridList()
        {
            try
            {
                var models = Factories.CreateUser().GetAll();
                gridList.Rows.Clear();
                int count = 0;
                foreach (var item in models)
                {
                    count++;
                    gridList.Rows.Add(new string[] { item.ID.ToString(), count.ToString(), 
                            
                            item.UserName
                           
                    });
                }
            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.Message);
            }
        }
        
        private void btnAdd_Click(object sender, EventArgs e)
        {
            Add();
            
        }

        private void Add()
        {
            mainPage.SetPage(pageDetail);
            ResetInputs(pnlAddEdit);
            userID = 0;
            bunifuTransition1.HideSync(pnlSide, false, BunifuAnimatorNS.Animation.HorizBlind);
        }

        private void btnAddEditClose_Click(object sender, EventArgs e)
        {
            LoadDefaultUI();
        }
        

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        void SetToolTip()
        {
            bunifuToolTip1.SetToolTip(btnAdd, "Add");
            bunifuToolTip1.SetToolTip(btnEdit, "Edit");
            bunifuToolTip1.SetToolTip(btnDelete, "Delete");
        }
        private void Save()
        {
            try
            {
                if (!ValidateFields())
                {
                    LocalUtils.ShowValidationFailedMessage(this);
                    return;
                }
                
                var model = new UserModel();
                model.Password = txtPassword.Text;
                model.ReTypePassword = txtRetypePassword.Text;
                model.UserName = txtUserName.Text;
                if (userID > 0)
                {
                    Factories.CreateUser().Edit(userID, model);
                }else{
                    Factories.CreateUser().Add(model);
                    userID = model.ID;
                }
                ShowMainGrid();

            }
            catch (Exception ex)
            {

                LocalUtils.ShowErrorMessage(this, ex.Message);
            }
        }
        private void LoadDefaultUI()
        {
            mainPage.SetPage(pageGridMain);            
            bunifuTransition1.ShowSync(pnlSide,false,BunifuAnimatorNS.Animation.Transparent);

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
        
        private void btnEdit_Click(object sender, EventArgs e)
        {
            Edit();
        }

        private void Edit()
        {
            try
            {
                if (gridList.SelectedRows.Count > 0)
                {
                    // get id
                    userID = int.Parse(gridList.SelectedRows[0].Cells[0].Value.ToString());
                    if (userID != 0)
                    {
                        //show and hide controls
                        mainPage.SetPage(pageDetail);
                        bunifuTransition1.HideSync(pnlSide, false, BunifuAnimatorNS.Animation.Transparent);
                        ResetInputs(pnlAddEdit);
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
        private void SetData()
        {
            try
            {
               
                var model = Factories.CreateUser().Get(userID);
                txtUserName.Text = model.UserName;
                
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
            LocalUtils.CenterControlXY(pageDetail, pnlAddEdit);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            try
            {
                if (gridList.SelectedRows.Count > 0)
                {
                    userID = int.Parse(gridList.SelectedRows[0].Cells[0].Value.ToString());
                    if (userID != 0)
                    {
                        DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Milk Collection", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                        if (result == DialogResult.Yes)
                        {
                            Factories.CreateUser().Delete(userID);
                            LocalUtils.ShowDeleteSuccessMessage(this);
                            ShowMainGrid();
                            userID = 0;
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

        private void ShowMainGrid()
        {
            LoadDefaultUI();
            LoadMainGridList();
        }

        private void pnlAddEdit_Paint(object sender, PaintEventArgs e)
        {

        }

        private void frmUsers_Load(object sender, EventArgs e)
        {
            LoadMainGridList();
            SetToolTip();
        }



    }
}
