using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using System.Globalization;
using MNAIS.Views;
using MNAIS.Utility;
using System.IO;
using System.Security;
using VerifyLicense;
using System.Windows.Forms;

namespace MNAIS
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        public static string configFilePath = string.Empty;
        public static MNAISWindow m_MNAISWindow;
               
        protected override void OnStartup(StartupEventArgs e)
        {
            try
            {
                configFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location + ".config";

                if (ConfigurationManager.AppSettings["Language"] == Language.Russian.ToString())
                    MNAIS.Resources.MNAISResources.Culture = new CultureInfo("ru-RU");
                else
                    MNAIS.Resources.MNAISResources.Culture = new CultureInfo("en-US");

                m_MNAISWindow = new MNAISWindow();                
                m_MNAISWindow.Show();
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }

            #region Commented for Development Period.
            //try
            //{
            //    configFilePath = System.Reflection.Assembly.GetExecutingAssembly().Location + ".config";

            //    if (ConfigurationManager.AppSettings["LicenseFile"] == string.Empty || ConfigurationManager.AppSettings["SystemFile"] == string.Empty)
            //    {
            //        LicenseRequest licenseRequest = new LicenseRequest();
            //        licenseRequest.ShowDialog();
            //    }
            //    else
            //    {
            //        LoadMNAIS();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.Forms.MessageBox.Show(ex.Message, "Licensing", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    App.Current.Shutdown();
            //} 
            #endregion
        }
            
        public static void LoadMNAIS()
        {   
            try
            {
                if (File.Exists(ConfigurationManager.AppSettings["LicenseFile"]) && File.Exists(ConfigurationManager.AppSettings["SystemFile"]))
                {
                    string status = VerifyLicense.Verify.VerifyLicense(ConfigurationManager.AppSettings["LicenseFile"], ConfigurationManager.AppSettings["SystemFile"]);

                    if (status == "Valid License")
                    {
                        m_MNAISWindow = new MNAISWindow();
                        m_MNAISWindow.MainWindowVM.AdminVM.LicenseStatus = status;

                        if (ConfigurationManager.AppSettings["Language"] == Language.Russian.ToString())
                        {
                            MNAIS.Resources.MNAISResources.Culture = new CultureInfo("ru-RU");
                        }
                        else
                        {
                            MNAIS.Resources.MNAISResources.Culture = new CultureInfo("en-US");
                        }

                        m_MNAISWindow.Show();
                    }
                    else if (status.StartsWith("License Expired On :"))
                    {
                        DialogResult dlgResult = System.Windows.Forms.MessageBox.Show("License expired, request for a new one ?", "Licensing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (dlgResult == System.Windows.Forms.DialogResult.Yes)
                        {
                            UpdateConfiguration.UpdateKey("LicenseFile", string.Empty);
                            UpdateConfiguration.UpdateKey("SystemFile", string.Empty);

                           // LicenseRequest licenseRequest = new LicenseRequest();
                           // licenseRequest.SelectedLicenseOption = LicenseFile.Request;
                           // licenseRequest.ShowDialog();
                        }
                        else
                            App.Current.Shutdown();
                    }
                    else
                    {
                        System.Windows.Forms.MessageBox.Show(status, "Licensing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        App.Current.Shutdown();
                    }
                }
                else
                {
                    DialogResult dlgResult = System.Windows.Forms.MessageBox.Show("License files not present, request for a new one ?", "Licensing", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (dlgResult == System.Windows.Forms.DialogResult.Yes)
                    {
                        UpdateConfiguration.UpdateKey("LicenseFile", string.Empty);
                        UpdateConfiguration.UpdateKey("SystemFile", string.Empty);
                       // LicenseRequest licenseRequest = new LicenseRequest();
                       // licenseRequest.SelectedLicenseOption = LicenseFile.Request;
                       // licenseRequest.ShowDialog();
                    }
                    else
                        App.Current.Shutdown();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message, "Licensing", MessageBoxButtons.OK, MessageBoxIcon.Information);
                App.Current.Shutdown();
            }            
        }

        private void APP_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            System.Windows.Forms.MessageBox.Show(e.Exception.Message);
            e.Handled = true;
        }        
    }
}
