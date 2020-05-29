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
    public partial class frmMilkCollectionRecord : Form
    {
        IMilkCollectionLogic milkCollectionLogic;
        IMilkClassLogic milkClassLogic;
        private IEnumerable<Logic.Models.MilkClassModel> milkClassList;
        private IFarmerLogic farmerLogic;
        private IEnumerable<Logic.Models.FarmerListModel> farmerList;
        private int id;
        public frmMilkCollectionRecord()
        {
            InitializeComponent();
            milkCollectionLogic = new MilkCollectionLogic();
        }

        private void frmMilkProductRecord_Load(object sender, EventArgs e)
        {
            LoadFarmerList();
            LoadMilkClass();
            LoadMilkCollections();
            AddHandlers();
            SetToolTip();
        }
        private void LoadMilkCollections()
        {
            DateTime date = dtDateSearch.Value;
            string criteria = txtSearch.Text;
            IEnumerable<MilkCollectionModel> records;
            MilkCollectionSummaryModel summary;
            try
            {
                if (radioByDate.Checked)
                {
                    records = milkCollectionLogic.GetAllRecordsByDate(date, criteria);
                   
                }
                else
                {
                    records = milkCollectionLogic.GetAllRecordsByMonth(date, criteria);
                    
                }
                int count = 0;
                gridList.Rows.Clear();
                foreach (var item in records)
                {
                    count++;
                    gridList.Rows.Add(new string[] { 
                            item.ID.ToString(),
                            count.ToString(),
                            item.ActualDate.ToShortDateString(),
                            item.MilkClass,
                            item.FullName,
                            item.Volume.ToString(),
                            item.Amount.ToString()});
                }

                //Shows summary of the list
               
                if (radioByDate.Checked)
                {
                    summary = milkCollectionLogic.GetMilkProductSummaryDate(date);
                }
                else
                {
                    summary = milkCollectionLogic.GetMilkProductSummaryMonth(date);
                }
                gridList.Rows.Add(new string[] { "", "", "", "", "Summary Per Day", "Total Volume", "Total Amount" });
                foreach (var item in summary.MilkProductSubTotalModels)
                {
                    var subTotalAmount = item.MilkAmountSubTotal;
                    var subTotalVolume = item.MilkVolumeSubTotal;
                    gridList.Rows.Add(new string[] { 
                            "",
                            "",
                            "",
                            "",
                            item.MilkClass,
                            subTotalVolume.ToString(),
                            subTotalAmount.ToString(),
                            });
                }
                gridList.Rows.Add(new string[] { 
                            "",
                            "",
                            "",
                            "",
                            "Total",
                            summary.MilkVolumeTotal.ToString(),
                           summary.MilkAmountTotal.ToString(),
                            });
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
            var dropdowns = LocalUtils.GetAll(pnlAddEdit, typeof(BunifuDropdown));
            foreach (BunifuDropdown item in dropdowns)
            {
                item.Text = string.Empty;
            }
            var datepickers = LocalUtils.GetAll(pnlAddEdit, typeof(BunifuDatePicker));
            foreach (BunifuDatePicker item in datepickers)
            {
                item.Value = DateTime.Now;
            }
        }
        private void gridList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void txtSearch_OnIconRightClick(object sender, EventArgs e)
        {
            LoadMilkCollections();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            bunifuPages1.SetPage(tabPage2);
            ResetInputs();
            lblAddEditTitle.Text = "Add New Record";
            id = 0;
            bunifuTransition1.HideSync(pnlSide,false,BunifuAnimatorNS.Animation.HorizBlind);
            
        }

        private void btnAddEditClose_Click(object sender, EventArgs e)
        {
            LoadDefaultUI();
        }
        private void LoadMilkClass()
        {
            try
            {

                var milkTypes = Factories.CreateSupplyType().GetAllBy(1);
                foreach (var item in milkTypes)
                {
                    cboMilkClass.Items.Add(new ItemX(item.Description,item.ID.ToString()));
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        private void LoadFarmerList()
        {
            try
            {
                var farmers = Factories.CreateFarmers().GetAllRecord();
                foreach (var item in farmers)
                {
                    cboFarmer.Items.Add(new ItemX(item.FullName, item.ID.ToString()));
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                milkCollectionLogic = new MilkCollectionLogic();

                if (ValidateFields())
                {
                    //id greater than zero = edit
                    //id equal to zero = add
                    
                    if (id == 0)
                    {
                        var model = new AddMilkCollectionModel();
                        model.ActualDate = dtDate.Value;
                        model.FarmerID = int.Parse(((ItemX)cboFarmer.SelectedItem).Value);
                        model.MilkClassID = int.Parse(((ItemX)cboMilkClass.SelectedItem).Value);
                        model.Volume = double.Parse(txtVolume.Text);
                        milkCollectionLogic.Add(model);
                        MetroMessageBox.Show(this, "Record has been saved!", "Milk Collection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDefaultUI();
                    }
                    else
                    {
                        var model = new EditMilkCollectionModel();
                        model.ActualDate = dtDate.Value;
                        model.FarmerID = int.Parse(((ItemX)cboFarmer.SelectedItem).Value);
                        model.MilkClassID = int.Parse(((ItemX)cboMilkClass.SelectedItem).Value);
                        model.Volume = double.Parse(txtVolume.Text);
                        milkCollectionLogic.Edit(id,model);
                        MetroMessageBox.Show(this, "Record has been saved!", "Milk Collection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadDefaultUI();
                    }
                }
                else
                {
                    // Validation error
                    MetroMessageBox.Show(this, "Invalid Field(s)!", "Milk Collection", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            LoadMilkCollections();
            
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
                milkCollectionLogic = new MilkCollectionLogic();
                var model = Factories.CreateMilkCollection().Get(id);
                
                dtDate.Value = model.ActualDate;
                cboFarmer.SelectedItem = LocalUtils.GetSelectedByNameItemX(cboFarmer.Items, model.FullName);
                cboMilkClass.SelectedItem = LocalUtils.GetSelectedByNameItemX(cboMilkClass.Items, model.MilkClass);
                txtVolume.Text = model.Volume.ToString(); ;
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
                    MetroMessageBox.Show(this, "No record selected!", "Milk Collection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                DialogResult result = MetroMessageBox.Show(this, "Are you sure you want to delete this record?", "Milk Collection", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    milkCollectionLogic.Delete(id);
                    LoadMilkCollections();
                    MetroMessageBox.Show(this, "Record has been deleted!", "Milk Collection", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    id = 0;
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }



    }
}
