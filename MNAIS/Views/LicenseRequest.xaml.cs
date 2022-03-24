using System;
using System.DirectoryServices.AccountManagement;
using System.Management;
using System.Security.Principal;
using System.Windows;
using System.Windows.Forms;
using System.Configuration;

namespace MNAIS.Views
{
    /// <summary>
    /// Interaction logic for LicenseRequest.xaml
    /// </summary>
    public partial class LicenseRequest : Window
    {
        LicenseFile selectedLicenseOption = LicenseFile.ChooseOption;
                
        public LicenseFile SelectedLicenseOption 
        {
            get 
            { 
                return selectedLicenseOption; 
            }
            
            set
            {
                selectedLicenseOption = value;

                if (selectedLicenseOption == LicenseFile.Present)
                {
                    expUpdateLicense.IsExpanded = expUpdateLicense.IsEnabled = true;
                    expRequestLicense.IsExpanded = expRequestLicense.IsEnabled = false;
                }
                else if (selectedLicenseOption == LicenseFile.Request)
                {
                    expRequestLicense.IsExpanded = expRequestLicense.IsEnabled = true;
                    expUpdateLicense.IsExpanded = expUpdateLicense.IsEnabled = false;
                }
                else if (selectedLicenseOption == LicenseFile.ChooseOption)                
                {
                    expRequestLicense.IsExpanded = expRequestLicense.IsEnabled = false;
                    expUpdateLicense.IsExpanded = expUpdateLicense.IsEnabled = false;
                }
            }
        }

        public LicenseRequest()
        {
            InitializeComponent();
            
            expRequestLicense.IsExpanded = expRequestLicense.IsEnabled = false;
            expUpdateLicense.IsExpanded = expUpdateLicense.IsEnabled = false;
            
            SecurityIdentifier builtinAdminSid = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
            PrincipalContext ctx = new PrincipalContext(ContextType.Machine);
            GroupPrincipal group = GroupPrincipal.FindByIdentity(ctx, builtinAdminSid.Value);

            foreach (Principal p in group.Members)
            {
                cmbUserName.Items.Add(p.SamAccountName);
            }
            this.DataContext = this;
        }

        private void GenerateProductFile_Click(object sender, RoutedEventArgs e)
        {
            if (cmbUserName.SelectedIndex != -1)
            {
                ManagementObjectSearcher searcher;
                string tmpSysName = string.Empty;
                string tmpProId = string.Empty;
                string tmpbiosNo = string.Empty;
                string tmpboardNo = string.Empty;

                searcher = new ManagementObjectSearcher("select * from Win32_Processor");
                foreach (ManagementObject share in searcher.Get())
                {
                    tmpProId = share.Properties["processorId"].Value.ToString();
                    tmpSysName = share.Properties["SystemName"].Value.ToString();
                    break;
                }

                searcher = new ManagementObjectSearcher("select * from Win32_BaseBoard");
                foreach (ManagementObject share in searcher.Get())
                {
                    tmpbiosNo = share.Properties["SerialNumber"].Value.ToString();
                    break;
                }

                searcher = new ManagementObjectSearcher("select * from Win32_BIOS");
                foreach (ManagementObject share in searcher.Get())
                {
                    tmpboardNo = share.Properties["SerialNumber"].Value.ToString();
                    break;
                }

                SystemInfo terms = new SystemInfo()
                {
                    BaseBoardSerialNumber = tmpboardNo,
                    BIOSSerialNumber = tmpbiosNo,
                    ProcessorID = tmpProId,
                    SystemName = tmpSysName,                    
                    UserName = cmbUserName.SelectedValue.ToString(),
                    CurrentTime = Utility.Utility.GetCurrentTime()
                };

                FolderBrowserDialog folderDialog = new FolderBrowserDialog();
                folderDialog.Description = "Choose location";
                folderDialog.RootFolder = Environment.SpecialFolder.Desktop;
                folderDialog.ShowNewFolderButton = true;

                if (folderDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)                
                    Utility.Utility.Serialize<SystemInfo>(terms, folderDialog.SelectedPath + "\\Product.xml");
                    
                 DialogResult dlgResult = System.Windows.Forms.MessageBox.Show("Send Email to mani.sharma@rms.com", "Licensing", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                 if (dlgResult == System.Windows.Forms.DialogResult.Yes)
                 {
                    // SendRequestEmail(folderDialog.SelectedPath + "\\Product.xml");
                     System.IO.File.Delete(folderDialog.SelectedPath + "\\Product.xml"); 
                     App.Current.Shutdown();
                 }
                 else
                     App.Current.Shutdown();
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Required fields cannot be blank.", "Licensing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        //void SendRequestEmail(string filename)
        //{
        //    Outlook.Application oApp;
        //    Outlook.Recipients oRecips;
        //    Outlook.Recipient oRecip;
        //    Outlook.MailItem oMsg;

        //    try
        //    {
        //        oApp = new Outlook.Application();
        //        Outlook.NameSpace ns = oApp.GetNamespace("MAPI");
        //        Outlook.MAPIFolder f = ns.GetDefaultFolder(Outlook.OlDefaultFolders.olFolderInbox);
        //        System.Threading.Thread.Sleep(5000);

        //        oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
        //        oMsg.HTMLBody = "Hi Mani, Please generate a license file for the attached file and revert";

        //        String sDisplayName = filename;
        //        int iPosition = (int)oMsg.HTMLBody.Length + 1;
        //        int iAttachType = (int)Outlook.OlAttachmentType.olByValue;

        //        Outlook.Attachment oAttach = oMsg.Attachments.Add(filename, iAttachType, iPosition, sDisplayName);

        //        oMsg.Subject = "License File Required.";
        //        oRecips = (Outlook.Recipients)oMsg.Recipients;
        //        oRecip = (Outlook.Recipient)oRecips.Add("mani.sharma@rms.com");
        //        oRecip.Resolve();
        //        oMsg.Send();
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.Message);
        //    }
        //    finally
        //    {
        //        oRecip = null;
        //        oRecips = null;
        //        oMsg = null;
        //        oApp = null;
        //    }
        //}

        private void UpdateLicense_Click(object sender, RoutedEventArgs e)
        {
            if (txtLicenseFile.Text != string.Empty && txtPublicKeyFile.Text != string.Empty)
            {
                //update app config
                if (System.IO.File.Exists(App.configFilePath))
                {
                    UpdateConfiguration.UpdateKey("LicenseFile", txtLicenseFile.Text);
                    ConfigurationManager.RefreshSection("appSettings");

                    UpdateConfiguration.UpdateKey("SystemFile", txtPublicKeyFile.Text);
                    ConfigurationManager.RefreshSection("appSettings");
                    
                    this.Hide();
                    App.LoadMNAIS();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("Not found :" + App.configFilePath + ", Please reinstall the application.");
                    App.Current.Shutdown();
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("Required fields cannot be blank.", "Licensing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void BrowseLicense_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDlg = new Microsoft.Win32.OpenFileDialog();
            fileDlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            fileDlg.Filter = "License Files (.xml)|*.xml";

            Nullable<bool> result = fileDlg.ShowDialog();

            if (result == true)
            {
              txtLicenseFile.Text = fileDlg.FileName;
            }
        }

        private void BrowsePublicKey_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog fileDlg = new Microsoft.Win32.OpenFileDialog();
            fileDlg.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
            fileDlg.Filter = "System Files (.xml)|*.xml";

            Nullable<bool> result = fileDlg.ShowDialog();

            if (result == true)
            {
               txtPublicKeyFile.Text = fileDlg.FileName;
            }
        }
    }    
}
