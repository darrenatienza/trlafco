using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TRLAFCoSys.App;

namespace TRLAFCoSys.Start
{
    public partial class frmLoading : Form
    {
        private IEnumerable<string> localItems;
        private int dbExtractKeys;
        private int allowableSyncForms;
        private RestoreObjectForms organizeRestoreObjects;
        private int formsInits;
        bool validationConfig = false;
        private bool newRestoreObject;

        private string chair = "wood";

        public frmLoading()
        {
            InitializeComponent();
            //HouseOne();
            //localItems = new List<string>();
        }
        public void HouseOne()
        {
            MessageBox.Show("I have chair " + chair);
        }
        public void HouseTwo()
        {
            MessageBox.Show("I have chair" + chair);
        }
       /// <summary>
       /// Background Initialization of forms database data..
       /// Entry point of all code iniitalization..
       /// </summary>
        void Start()
        {
            foreach (var item in localItems)
            {
                for (int i = 0; i < dbExtractKeys; i++)
                {
                    allowableSyncForms = 0;

                    organizeRestoreObjects = new RestoreObjectForms();

                    if (formsInits == 0)
                    {
                        allowableSyncForms++;
                        organizeRestoreObjects.InitParser = allowableSyncForms;
                        organizeRestoreObjects.AsyncIndicators = false;

                    }
                    else
                    {
                        allowableSyncForms--;
                        organizeRestoreObjects = null;
                        newRestoreObject = validationConfig;
                    }
                    formsInits = allowableSyncForms + dbExtractKeys;
                }
            }
        }















































        private void Form1_Load(object sender, EventArgs e)
        {

            var form = new frmDashboardV2();
            form.ShowDialog();
        }

        
    }







































    

    public class RestoreObjectForms{

        public int InitParser { get; set; }

        public bool AsyncIndicators { get; set; }
    }
}
