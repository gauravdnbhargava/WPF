using System.Windows.Input;
using System.IO;
using System.Windows;
using System.Configuration;
using System.Xml.Serialization;
using System;
using System.Runtime.Serialization.Formatters.Binary;

namespace MNAIS
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MNAISWindow m_MainWindow { get; set; }

        PremiumChartsViewModel m_premiumChartsViewModel;        
        AdministrationViewModel m_adminViewModel;
        YieldGraphsViewModel m_yieldGraphsViewModel;
        
        internal ImportDataViewModel m_importdataViewModel;
        internal ResultsSummaryViewModel m_resultsSummaryViewModel;        
        internal ModellingOptionsViewModel m_modellingoptionsViewModel;
        internal PricingOptionsViewModel m_pricingOptionsViewModel;
        internal CreateContractViewModel m_createContractViewModel;

        RelayCommand _resultsSummaryLoadCommand;
        RelayCommand _premiumChartsLoadCommand;
        RelayCommand _yieldGraphsLoadCommand;
        RelayCommand _modellingOptionsLoadCommand;
        RelayCommand _pricingOptionsLoadCommand;
        RelayCommand _administrationLoadCommand;
        RelayCommand _importedYieldDataLoadCommand;

        RelayCommand helpLoadCommand;
        RelayCommand aboutLoadCommand;
        RelayCommand exitCommand;
        RelayCommand deleteContract;

        UIElement m_presenterContent;
        UIElement m_projectInfoPresenter;

        string _contractExportPath;
        ErrorType errType = ErrorType.Debug;
        string message = "mNAIS initialised successfully, create/import a contract to continue.";

        #region Public Methods & Properties

        public UIElement PresenterContent
        {
            get
            {
                return m_presenterContent;
            }
            set
            {
                if (value != m_presenterContent)
                    m_presenterContent = value;
                OnPropertyChanged("PresenterContent");
            }
        }

        public UIElement ProjectInfoPresenter
        {
            get
            {
                return m_projectInfoPresenter;
            }
            set
            {
                if (value != m_projectInfoPresenter)
                    m_projectInfoPresenter = value;
                OnPropertyChanged("ProjectInfoPresenter");
            }
        }

        public PremiumChartsViewModel PremiumChartsVM
        {
            get
            {
                if (m_premiumChartsViewModel == null)
                    m_premiumChartsViewModel = new PremiumChartsViewModel();
                return m_premiumChartsViewModel;
            }
        }

        public CreateContractViewModel CreateContractVM
        {
            get
            {
                if (m_createContractViewModel == null)
                    m_createContractViewModel = new CreateContractViewModel(this);

                if (m_createContractViewModel.mainVM == null)
                    m_createContractViewModel.mainVM = this;

                return m_createContractViewModel;
            }

            set
            {
                m_createContractViewModel = value;
                OnPropertyChanged("CreateContractVM");
            }
        }

        public ImportDataViewModel ImportDataVM
        {
            get
            {
                if (m_importdataViewModel == null)
                    m_importdataViewModel = new ImportDataViewModel(m_MainWindow.ImportDataScreen,this);
                return m_importdataViewModel;
            }

            set
            {
                m_importdataViewModel = value;
                OnPropertyChanged("ImportDataVM");
            }
        }

        public YieldGraphsViewModel YieldGraphsVM
        {
            get
            {
                if (m_yieldGraphsViewModel == null)
                    m_yieldGraphsViewModel = new YieldGraphsViewModel();
                return m_yieldGraphsViewModel;
            }
        }

        public ResultsSummaryViewModel ResultsSummaryVM
        {
            get
            {
                if (m_resultsSummaryViewModel == null)
                    m_resultsSummaryViewModel = new ResultsSummaryViewModel();
                return m_resultsSummaryViewModel;
            }

            set
            {
                m_resultsSummaryViewModel = value;
                OnPropertyChanged("ResultsSummaryVM");
            }
        }

        public AdministrationViewModel AdminVM
        {
            get
            {
                if (m_adminViewModel == null)
                    m_adminViewModel = new AdministrationViewModel();
                return m_adminViewModel;
            }
        }

        public ModellingOptionsViewModel ModellingVM
        {
            get
            {
                if (m_modellingoptionsViewModel == null)
                    m_modellingoptionsViewModel = new ModellingOptionsViewModel(this);

                if (m_modellingoptionsViewModel.mainVM == null)
                    m_modellingoptionsViewModel.mainVM = this;

                return m_modellingoptionsViewModel;
            }

            set
            {
                m_modellingoptionsViewModel = value;
                OnPropertyChanged("ModellingVM");
            }
        }

        public PricingOptionsViewModel PricingVM
        {
            get
            {
                if (m_pricingOptionsViewModel == null)
                    m_pricingOptionsViewModel = new PricingOptionsViewModel(this);

                if (m_pricingOptionsViewModel.main == null)
                    m_pricingOptionsViewModel.main = this;

                return m_pricingOptionsViewModel;
            }

            set
            {
                m_pricingOptionsViewModel = value;
                OnPropertyChanged("PricingVM");
            }
        }

        public MainWindowViewModel(MNAISWindow _mainWindow)
        {
            m_MainWindow = _mainWindow;
            Logging.mainVM = this;
        }

        public ICommand ResultSummaryLoadCommand
        {
            get
            {
                if (_resultsSummaryLoadCommand == null)
                {
                    _resultsSummaryLoadCommand = new RelayCommand(param => this.LoadResultSummary());
                }
                return _resultsSummaryLoadCommand;
            }
        }

        public ICommand PremiumChartsLoadCommand
        {
            get
            {
                if (_premiumChartsLoadCommand == null)
                    _premiumChartsLoadCommand = new RelayCommand(param => this.LoadPremiumCharts());

                return _premiumChartsLoadCommand;
            }
        }

        public ICommand YieldGraphsLoadCommand
        {
            get
            {
                if (_yieldGraphsLoadCommand == null)
                    _yieldGraphsLoadCommand = new RelayCommand(param => this.LoadYieldGraphs());

                return _yieldGraphsLoadCommand;
            }
        }

        public ICommand ModellingOptionsLoadCommand
        {
            get
            {
                if (_modellingOptionsLoadCommand == null)
                {
                    _modellingOptionsLoadCommand = new RelayCommand(param => this.LoadModellingOptionsControl());
                }
                return _modellingOptionsLoadCommand;
            }
        }

        public ICommand PricingOptionsLoadCommand
        {
            get
            {
                if (_pricingOptionsLoadCommand == null)
                {
                    _pricingOptionsLoadCommand = new RelayCommand(param => this.LoadPricingOptionsControl());
                }
                return _pricingOptionsLoadCommand;
            }
        }

        public ICommand ImportedYieldDataLoadCommand
        {
            get
            {
                if (_importedYieldDataLoadCommand == null)
                {
                    _importedYieldDataLoadCommand = new RelayCommand(param => this.LoadImportedYieldData());
                }
                return _importedYieldDataLoadCommand;
            }
        }

        public ICommand AdministrationLoadCommand
        {
            get
            {
                if (_administrationLoadCommand == null)
                    _administrationLoadCommand = new RelayCommand(param => this.LoadAdministration());

                return _administrationLoadCommand;
            }
        }

        public ICommand HelpLoadCommand
        {
            get
            {
                if (helpLoadCommand == null)
                    helpLoadCommand = new RelayCommand(param => this.LoadHelp());

                return helpLoadCommand;
            }
        }

        public ICommand AboutLoadCommand
        {
            get
            {
                if (aboutLoadCommand == null)
                    aboutLoadCommand = new RelayCommand(param => this.LoadAbout());

                return aboutLoadCommand;
            }
        }

        public ICommand ExitCommand
        {
            get
            {
                if (exitCommand == null)
                    exitCommand = new RelayCommand(param => this.Exit());

                return exitCommand;
            }
        }

        public ICommand DeleteContractCommand
        {
            get
            {
                if (deleteContract == null)
                    deleteContract = new RelayCommand(param => this.DeleteContract());

                return deleteContract;
            }
        }

        public string ContractExportPath
        {
            get
            {                
                return _contractExportPath;
            }

            set
            {
                _contractExportPath = value;                
                OnPropertyChanged("ContractExportPath");
            }
        }

        public void ExportContract()
        {
            try
            {
                ContractStructure structure = new ContractStructure() { contractVM = CreateContractVM, dataVM = ImportDataVM, modellingVM = ModellingVM, pricingVM = PricingVM };
                
                Stream TestFileStream = File.Create(ContractExportPath);
                BinaryFormatter serializer = new BinaryFormatter();
                serializer.Serialize(TestFileStream, structure);                
                TestFileStream.Close();
            }
            catch (Exception ex)
            {
                ErrType = ErrorType.Error;
                Message = "Contract Export Failed. Reason : " + ex.InnerException.Message;
            }
        }

        public ContractStructure ImportContract()
        {
            ContractStructure importedContract = new ContractStructure();

            try
            {
                Stream TestFileStream = File.OpenRead(ContractExportPath);
                BinaryFormatter deserializer = new BinaryFormatter();
                importedContract = (ContractStructure)deserializer.Deserialize(TestFileStream);
                TestFileStream.Close();
            }
            catch (Exception ex)
            {
                ErrType = ErrorType.Error;
                Message = "Contract Import Failed. Reason : " + ex.InnerException.Message;
            }

            return importedContract;
        }

        public string Message
        {
            get { return message; }

            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        public ErrorType ErrType
        {
            get { return errType; }

            set
            {
                errType = value;
                OnPropertyChanged("ErrType");
            }
        }

        bool isDataImportEnabled;

        public bool IsDataImportEnabled
        {
            get { return isDataImportEnabled; }

            set 
            {
                isDataImportEnabled = value;
                OnPropertyChanged("IsDataImportEnabled");
            }
        }

        bool isAnalysisOptionsEnabled;

        public bool IsAnalysisOptionsEnabled
        {
            get { return isAnalysisOptionsEnabled; }

            set
            {
                isAnalysisOptionsEnabled = value;
                OnPropertyChanged("IsAnalysisOptionsEnabled");
            }
        }

        bool isResultsEnabled;

        public bool IsResultsEnabled
        {
            get { return isResultsEnabled; }

            set
            {
                isResultsEnabled = value;
                OnPropertyChanged("IsResultsEnabled");
            }
        }

        bool isCreateEnabled = true;

        public bool IsCreateEnabled
        {
            get { return isCreateEnabled; }

            set
            {
                isCreateEnabled = value;
                OnPropertyChanged("IsCreateEnabled");
            }
        }

        bool isDeleteEnabled = false;

        public bool IsDeleteEnabled
        {
            get { return isDeleteEnabled; }

            set
            {
                isDeleteEnabled = value;
                OnPropertyChanged("IsDeleteEnabled");
            }
        }

        #endregion

        # region Private Methods

        internal void DeleteContract()
        {
            m_MainWindow.m_createContract = null;
            CreateContractVM = null;

            m_MainWindow.m_importDataView = null;
            ImportDataVM = null;

            m_MainWindow.m_ModellingOptionsView = null;
            ModellingVM = null;

            m_MainWindow.m_PricingOptionsView = null;
            PricingVM = null;

            m_MainWindow.m_ResultSummaryView = null;
            ResultsSummaryVM = null;

            m_MainWindow.m_contractInformation = null;

            IsCreateEnabled = true;
            IsAnalysisOptionsEnabled = false;
            IsResultsEnabled = false;
            IsDataImportEnabled = false;

            ProjectInfoPresenter = null;
            PresenterContent = m_MainWindow.HomeScreen;

            isDeleteEnabled = false;
            OnPropertyChanged("IsDeleteEnabled");

            message = "mNAIS re initialised successfully, create/import a contract to continue.";
            OnPropertyChanged("Message");            
        }

        void LoadYieldGraphs()
        {
            PresenterContent = m_MainWindow.YieldGraphScreen;
        }

        void LoadPremiumCharts()
        {
            PresenterContent = m_MainWindow.PremiumChartScreen;
        }

        void LoadModellingOptionsControl()
        {
            PresenterContent = m_MainWindow.ModellingOptionsScreen;
        }

        void LoadPricingOptionsControl()
        {
            PresenterContent = m_MainWindow.PricingOptionsScreen;
        }

        void LoadResultSummary()
        {
            PresenterContent = m_MainWindow.ResultsScreen;
        }

        void LoadImportedYieldData()
        {
            PresenterContent = m_MainWindow.ImportDataScreen;
            ProjectInfoPresenter = m_MainWindow.ContractInfoScreen;
        }

        void LoadAdministration()
        {
            ProjectInfoPresenter = null;
            PresenterContent = m_MainWindow.AdminScreen;
        }

        void LoadAbout()
        {
            MNAIS.Views.About aboutWindow = new Views.About();
            aboutWindow.ShowDialog();
        }

        void LoadHelp()
        {
            try
            {
                System.Diagnostics.Process.Start(Utility.Constants.helpFile);
            }
            catch (Exception ex)
            {
                ErrType = ErrorType.Error;
                Message = ex.Message;
            }
        }

        void Exit()
        {
            App.Current.Shutdown();
        }

        # endregion
        
        #region Internal Methods
        
        internal void ClearResults()
        {
            if (isResultsEnabled)
            {
                m_resultsSummaryViewModel = null;
                m_MainWindow.m_ResultSummaryView = null;
                isResultsEnabled = false;
                OnPropertyChanged("IsResultsEnabled");
                ErrType = ErrorType.Debug;
                Message = "Results cleared since the value is changed.";
            }
        }

        internal void ClearImportedData()
        {
            m_importdataViewModel = null;
            m_MainWindow.m_importDataView = null;
            isAnalysisOptionsEnabled = false;
            OnPropertyChanged("IsAnalysisOptionsEnabled");
            ErrType = ErrorType.Debug;
            Message = "Imported data cleared since the value is changed.";
        } 
        
        #endregion
    }
}
