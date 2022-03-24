using System;
using System.Windows.Input;
using System.Windows.Forms;
using System.Configuration;
using System.Windows;

namespace MNAIS
{
   public class AdministrationViewModel : ViewModelBase
    {
       RelayCommand logfileCommand;
       string licenseStatus = "Not issued.";

       string logFilePath = ConfigurationManager.AppSettings["LogFile"];       
       int selectedprecision = Convert.ToInt32(ConfigurationManager.AppSettings["Precision"]);
       bool? generateLogFile = Convert.ToBoolean(ConfigurationManager.AppSettings["GenerateLogFile"]);
       bool? showMessages = Convert.ToBoolean(ConfigurationManager.AppSettings["ShowMessages"]); 
       bool? isLogFileLocationEnabled = Convert.ToBoolean(ConfigurationManager.AppSettings["GenerateLogFile"]);

       Language selectedLanguage = GetLanguage();
       ResultsDisplay selectedDisplayOption = GetResultsDisplay();    

       public bool? IsLogFileLocationEnabled
       {
           get { return isLogFileLocationEnabled; }

           set
           {
               isLogFileLocationEnabled = value;
               OnPropertyChanged("IsLogFileLocationEnabled");
           }
       }

       public bool? GenerateLogFile
       {
           get { return generateLogFile; }

           set
           {
               generateLogFile = value;
               isLogFileLocationEnabled = value;

               UpdateConfiguration.UpdateKey("GenerateLogFile", value.ToString());
               OnPropertyChanged("IsLogFileLocationEnabled");
               OnPropertyChanged("GenerateLogFile");
           }
       }

       public bool? ShowMessages
       {
           get { return showMessages; }

           set
           {
               showMessages = value;
               UpdateConfiguration.UpdateKey("ShowMessages", value.ToString());
               OnPropertyChanged("ShowMessages");
           }
       }
       
       public ICommand LogFileBrowseCommand
       {
           get
           {
               if (logfileCommand == null)
                   logfileCommand = new RelayCommand(param => this.SelectLogFile());               
               return logfileCommand;
           }
       }

       public AdministrationViewModel()
       {
           
       }

       public string LogFilePath
       {
           get { return logFilePath; }

           set
           {
               logFilePath = value;
               UpdateConfiguration.UpdateKey("LogFile", value);
               OnPropertyChanged("LogFilePath");
           }
       }

       public string LicenseStatus
       {
           get { return licenseStatus; }

           set
           {
               licenseStatus = value;
               OnPropertyChanged("LicenseStatus");
           }
       }

       public int SelectedPrecision
       {
           get { return selectedprecision; }

           set
           {
               selectedprecision = value;
               UpdateConfiguration.UpdateKey("Precision", value.ToString());
               OnPropertyChanged("SelectedPrecision");
           }
       }

       public Language SelectedLanguage
       {
           get
           {
               return selectedLanguage;
           }

           set
           {
               if (value != selectedLanguage)
               {
                   System.Windows.MessageBoxResult dlgResult = System.Windows.MessageBox.Show("Language changed, application needs to restart to apply changes.", "Information", 
                       System.Windows.MessageBoxButton.YesNo);

                   if (dlgResult == MessageBoxResult.Yes)
                   {
                       selectedLanguage = value;
                       UpdateConfiguration.UpdateKey("Language", value.ToString());
                       OnPropertyChanged("SelectedLanguage");

                       App.Current.Exit += delegate(object _sender, ExitEventArgs _e)
                       {
                           System.Diagnostics.Process.Start(App.ResourceAssembly.Location);
                       };
                       App.Current.Shutdown();
                   }                   
               }               
           }
       }

       public ResultsDisplay SelectedResultsDisplayOption
       {
           get 
           {
               return selectedDisplayOption; 
           }

           set
           {
               selectedDisplayOption = value;
               UpdateConfiguration.UpdateKey("ResultDisplayFormat", value.ToString());
               OnPropertyChanged("SelectedResultsDisplayOption");
           }
       }

       static Language GetLanguage()
       {
           if (ConfigurationManager.AppSettings["Language"] == Language.Russian.ToString())
               return MNAIS.Language.Russian;
           else
               return MNAIS.Language.English;
       }

       static ResultsDisplay GetResultsDisplay()
       {
           if (ConfigurationManager.AppSettings["ResultDisplayFormat"] == ResultsDisplay.Decimal.ToString())
               return ResultsDisplay.Decimal;
           else
               return ResultsDisplay.Percentage;
       }

       void SelectLogFile()
       {
           using (FolderBrowserDialog dialog = new FolderBrowserDialog())
           {
               dialog.Description = "Choose location for the Log file";
               dialog.ShowNewFolderButton = true;
               dialog.RootFolder = Environment.SpecialFolder.Desktop;

               if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                   LogFilePath = dialog.SelectedPath + "\\Log.txt";               
           }
       }
    }
}
