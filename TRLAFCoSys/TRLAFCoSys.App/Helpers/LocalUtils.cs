using MetroFramework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TRLAFCoSys.Queries.Persistence
{
    public class LocalUtils
    {

        /// <summary>
        /// Center control horizontally and vertically
        /// </summary>
        /// <param name="parent">Parent Container</param>
        /// <param name="child">Child Container</param>
        public static void CenterControlXY(Control parent, Control child)
        {
            child.Location = new Point((parent.Width - child.Width) / 2, (parent.Height - child.Height) / 2);
        }
        public static IEnumerable<Control> GetAll(Control control, Type type)
        {
            var controls = control.Controls.Cast<Control>();

            return controls.SelectMany(ctrl => GetAll(ctrl, type))
                                      .Concat(controls)
                                      .Where(c => c.GetType() == type);
        }

        public static string GetAliasName(string firstName, string lastName)
        {
            return string.Format("{0} {1}", firstName.Substring(0,1), lastName);
        }
        public static string ToValidFullNameFormat(string firstName, string middleName, string lastName)
        {
            if (middleName != "")
            {
                return string.Format("{0} {1}. {2}", firstName, middleName.Substring(0, 1), lastName);
            }
            return string.Format("{0} {1}", firstName, lastName);
        }
        public static string RandomNumbers(int length)
        {
            const string chars = "0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string GetAppSettingValue(string key)
        {
            return ConfigurationManager.AppSettings[key].ToString();
        }
        public static void EncryptConnectionString(bool encrypt, string fileName)
        {
            Configuration configuration = null;
            try
            {
                // Open the configuration file and retrieve the connectionStrings section.
                configuration = ConfigurationManager.OpenExeConfiguration(fileName);
                ConnectionStringsSection configSection = configuration.GetSection("connectionStrings") as ConnectionStringsSection;
                if ((!(configSection.ElementInformation.IsLocked)) && (!(configSection.SectionInformation.IsLocked)))
                {
                    if (encrypt && !configSection.SectionInformation.IsProtected)
                    {
                        //this line will encrypt the file
                        configSection.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
                    }

                    if (!encrypt && configSection.SectionInformation.IsProtected)//encrypt is true so encrypt
                    {
                        //this line will decrypt the file. 
                        configSection.SectionInformation.UnprotectSection();
                    }
                    //re-save the configuration file section
                    configSection.SectionInformation.ForceSave = true;
                    // Save the current configuration

                    configuration.Save();
                    //configFile.FilePath 
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        internal static DialogResult ShowDeleteMessage(Form parent)
        {
            return MetroMessageBox.Show(parent, "Are you want to delete this record?", "Notification", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        }
        internal static DialogResult ShowDeleteSuccessMessage(Form parent)
        {
            return MetroMessageBox.Show(parent, "Record deleted successfuly!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        internal static DialogResult ShowValidationFailedMessage(Form parent)
        {
            return MetroMessageBox.Show(parent, "Validation Failed for one or more fields!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal static DialogResult ShowAddNewMessage(Form parent)
        {
            return MetroMessageBox.Show(parent, "Ready for adding new record!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        internal static DialogResult ShowSaveMessage(Form parent)
        {
            return MetroMessageBox.Show(parent, "Record succesfully save!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        internal static DialogResult ShowNoRecordFoundMessage(Form parent)
        {
            return MetroMessageBox.Show(parent, "No Record found for selected ID!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        internal static DialogResult ShowNoRecordSelectedMessage(Form parent)
        {
            return MetroMessageBox.Show(parent, "No record selected!", "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        internal static ItemX GetSelectedItemX(ComboBox.ObjectCollection objectCollection, string id)
        {
            return objectCollection.OfType<ItemX>().FirstOrDefault(r => r.Value == id.ToString());
        }
        internal static ItemX GetSelectedByNameItemX(ComboBox.ObjectCollection objectCollection, string name)
        {
            return objectCollection.OfType<ItemX>().FirstOrDefault(r => r.Name == name);
        }
        internal static double ConvertToDouble(string value)
        {
            double outValue = 0.0;
            return !double.TryParse(value, out outValue) ? 0.0 : outValue;
        }

        internal static int ConvertToInteger(string value)
        {
            int outValue = 0;
            return !int.TryParse(value, out outValue) ? 0 : outValue;
        }

        internal static DialogResult ShowErrorMessage(Form parent, string message)
        {
            return MetroMessageBox.Show(parent, "An error occured \n" + message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        internal static DialogResult ShowInfo(Form parent, string message)
        {
            return MetroMessageBox.Show(parent, message, "Notification", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
    public class ItemX
    {
        public string Name;
        public string Value;
        public ItemX(string name, string value)
        {
            Name = name; Value = value;
        }
        public override string ToString()
        {
            // Generates the text shown in the combo box
            return Name;
        }
    }

    public class ItemX2 : ItemX
    {
        
        public string[] Param;
        public ItemX2(string name, string value, params string[] param)
            : base(name, value)
        {
            Param = param;
        }  
    }

    public static class CurrentUser
    {
        public static int UserID { get; set; }
        public static List<string> Permissions { private get; set; }

        public static bool HasPermission(string permissionName)
        {
            if (UserID == 1)
            {
                return true;
            }

            var permission = Permissions.Where(p => p.ToUpper() == permissionName.ToUpper()).FirstOrDefault();
            if (permission != null)
            {
                return true;
            }

            return false;
        }
    }
    internal class ProcessConnection
    {
        public static ConnectionOptions ProcessConnectionOptions()
        {
            ConnectionOptions options = new ConnectionOptions();
            options.Impersonation = ImpersonationLevel.Impersonate;
            options.Authentication = AuthenticationLevel.Default;
            options.EnablePrivileges = true;
            return options;
        }
        public static ManagementScope ConnectionScope(string machineName, ConnectionOptions options, string path)
        {
            ManagementScope connectScope = new ManagementScope();
            connectScope.Path = new ManagementPath(@"\\" + machineName + path);
            connectScope.Options = options;
            connectScope.Connect();
            return connectScope;
        }
    }
    public class COMPortInfo
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public COMPortInfo()
        {

        }
        public static List<COMPortInfo> GetCOMPortsInfo()
        {
            List<COMPortInfo> comPortInfoList = new List<COMPortInfo>();
            ConnectionOptions options = ProcessConnection.ProcessConnectionOptions();
            ManagementScope connectionScope = ProcessConnection.ConnectionScope(Environment.MachineName, options, @"\root\CIMV2");
            ObjectQuery objectQuery = new ObjectQuery("SELECT * FROM Win32_PnPEntity WHERE ConfigManagerErrorCode = 0");
            ManagementObjectSearcher comPortSearcher = new ManagementObjectSearcher(connectionScope, objectQuery);
            using (comPortSearcher)
            {
                string caption = null;
                foreach (ManagementObject obj in comPortSearcher.Get())
                {
                    if (obj != null)
                    {
                        object captionObj = obj["Caption"];
                        if (captionObj != null)
                        {
                            caption = captionObj.ToString();
                            if (caption.Contains("(COM"))
                            {
                                COMPortInfo comPortInfo = new COMPortInfo();
                                comPortInfo.Name = caption.Substring(caption.LastIndexOf("(COM")).Replace("(", string.Empty).Replace(")", string.Empty);
                                comPortInfo.Description = caption;
                                comPortInfoList.Add(comPortInfo);
                            }
                        }
                    }
                }
                return comPortInfoList;
            }

        }
    }
    public enum ReservationStatus
    {
        None,
        Borrowed,
        Returned
    }
}
