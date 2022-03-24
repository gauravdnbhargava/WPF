using System;
using System.Windows;
using System.Windows.Input;
using MNAIS.Views;
using System.Windows.Forms;
using Fluent;
using MNAIS.Utility;
using msgBox = System.Windows.MessageBox;


namespace MNAIS
{
    /// <summary>
    /// Interaction logic for MNAISWindow.xaml
    /// </summary>
    public partial class MNAISWindow : RibbonWindow
    {        
        PremiumCharts m_PremiumChartsView;
        YieldGraphs m_YieldGraphsView;
        
        internal MainWindowViewModel m_mainVM;        
        internal ResultsSummary m_ResultSummaryView;
        internal PricingOptions m_PricingOptionsView;
        internal ModellingOptions m_ModellingOptionsView;               
        
        internal ImportData m_importDataView;        
        internal Adminstration m_adminView;        
        internal CreateContract m_createContract;
        internal ContractInformation m_contractInformation;        
        internal Home m_Home;

        public MNAISWindow()
        {
            InitializeComponent();
            MainWindowVM.PresenterContent = HomeScreen;
        }

        public MainWindowViewModel MainWindowVM
        {
            get
            {
                if (m_mainVM == null)
                {
                    m_mainVM = new MainWindowViewModel(this);
                    this.DataContext = m_mainVM;
                }
                return m_mainVM;
            }
        }

        public Home HomeScreen
        {
            get
            {
                if (m_Home == null)
                {
                    m_Home = new Home();
                }
                return m_Home;
            }
        }

        public ContractInformation ContractInfoScreen
        {
            get 
            {
                if (m_contractInformation == null)
                {
                    m_contractInformation = new ContractInformation();
                    m_contractInformation.DataContext = m_mainVM.CreateContractVM;
                }
                return m_contractInformation; 
            }
        }

        public CreateContract ContractScreen
        {
            get
            {
                if (m_createContract == null)
                {
                    m_createContract = new CreateContract();
                    m_createContract.DataContext = m_mainVM.CreateContractVM;
                }
                return m_createContract;
            }
        }

        public ImportData ImportDataScreen
        {
            get
            {
                if (m_importDataView == null)
                {
                    m_importDataView = new ImportData();
                    m_importDataView.DataContext = m_mainVM.ImportDataVM;
                    m_importDataView.mainVM = m_mainVM;
                }
                return m_importDataView;
            }

            set
            {
                m_importDataView = value;                
            }
        }

        public Adminstration AdminScreen
        {
            get
            {
                if (m_adminView == null)
                {
                    m_adminView = new Adminstration();
                    m_adminView.DataContext = m_mainVM.AdminVM;
                }
                return m_adminView;
            }
        }

        public PremiumCharts PremiumChartScreen
        {
            get
            {
                if (m_PremiumChartsView == null)
                {
                    m_PremiumChartsView = new PremiumCharts();
                    m_PremiumChartsView.DataContext = m_mainVM.PremiumChartsVM;
                }
                return m_PremiumChartsView;
            }
        }

        public YieldGraphs YieldGraphScreen
        {
            get
            {
                if (m_YieldGraphsView == null)
                {
                    m_YieldGraphsView = new YieldGraphs();
                    m_YieldGraphsView.DataContext = m_mainVM.YieldGraphsVM;
                }
                return m_YieldGraphsView;
            }
        }

        public ResultsSummary ResultsScreen
        {
            get
            {
                if (m_ResultSummaryView == null)
                {
                    m_ResultSummaryView = new ResultsSummary();
                    m_ResultSummaryView.DataContext = m_mainVM.ResultsSummaryVM;
                }
                return m_ResultSummaryView;
            }

            set
            {
                m_ResultSummaryView = value;                
            }
        }

        public PricingOptions PricingOptionsScreen
        {
            get
            {
                if (m_PricingOptionsView == null)
                {
                    m_PricingOptionsView = new PricingOptions();
                    m_PricingOptionsView.DataContext = m_mainVM.PricingVM;
                }
                return m_PricingOptionsView;
            }
        }

        public ModellingOptions ModellingOptionsScreen
        {
            get
            {
                if (m_ModellingOptionsView == null)
                {
                    m_ModellingOptionsView = new ModellingOptions();
                    m_ModellingOptionsView.DataContext = m_mainVM.ModellingVM; 
                }
                return m_ModellingOptionsView;
            }
        }

        private void OutputTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Fluent.Button btn = sender as Fluent.Button;

            if (btn != null)
                btn.Focus();

            m_mainVM.ProjectInfoPresenter = ContractInfoScreen;
            m_mainVM.PresenterContent = ResultsScreen;          
        }

        private void ContractTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Fluent.Button btn = sender as Fluent.Button;

            if (btn != null)
                btn.Focus();

            if (m_mainVM.CreateContractVM.ContractName != null)
                m_mainVM.PresenterContent = ContractScreen;
            else
                m_mainVM.PresenterContent = HomeScreen;            
        }

        private void AdminTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            m_mainVM.ProjectInfoPresenter = null;
            m_mainVM.PresenterContent = AdminScreen;
        }

        private void AnalysisTab_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Fluent.Button btn = sender as Fluent.Button;

            if (btn != null)
                btn.Focus();

            m_mainVM.ProjectInfoPresenter = ContractInfoScreen;
            m_mainVM.PresenterContent = ModellingOptionsScreen;           
        }

        private void Create_Contract_Click(object sender, RoutedEventArgs e)
        {
            Fluent.Button btn = sender as Fluent.Button;

            if (btn != null)
                btn.Focus();
                      
            m_mainVM.PresenterContent = ContractScreen;                            
        }

        private void Import_Contract_Click(object sender, RoutedEventArgs e)
        {
            m_mainVM.ProjectInfoPresenter = null;            
            System.Windows.Forms.OpenFileDialog fdlg = new System.Windows.Forms.OpenFileDialog();
            fdlg.Title = "Select Contract to be imported";

            fdlg.InitialDirectory = m_mainVM.ContractExportPath;
            fdlg.Filter = "Data files (*.dat)|*.dat";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            fdlg.Multiselect = true;

            ContractStructure importedContract;

            if (fdlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    m_mainVM.ContractExportPath = fdlg.FileName;

                    importedContract = m_mainVM.ImportContract();

                    ContractInfoScreen.DataContext = ContractScreen.DataContext = m_mainVM.CreateContractVM = importedContract.contractVM;
                    PricingOptionsScreen.DataContext = m_mainVM.PricingVM = importedContract.pricingVM;
                    ModellingOptionsScreen.DataContext = m_mainVM.ModellingVM = importedContract.modellingVM;
                    ImportDataScreen.DataContext = m_mainVM.ImportDataVM = importedContract.dataVM;
                    
                    m_mainVM.PricingVM.main = m_mainVM.ModellingVM.mainVM = m_mainVM.CreateContractVM.mainVM = m_mainVM.ImportDataVM.mainVM = m_mainVM;

                    //since the data is part of the package in export.
                    m_mainVM.ImportDataVM.ImportFilePath = string.Empty;

                    m_mainVM.ImportDataVM.importView = ImportDataScreen;

                    m_mainVM.ImportDataVM.FillGrid();

                    m_mainVM.ProjectInfoPresenter = ContractInfoScreen;
                    
                    m_mainVM.IsCreateEnabled = false;
                    m_mainVM.IsDeleteEnabled = true;
                    m_mainVM.IsDataImportEnabled = true;
                    m_mainVM.IsAnalysisOptionsEnabled = true;

                    m_mainVM.ErrType = ErrorType.Debug;
                    m_mainVM.Message = "Contract imported successfully.";
                }
                catch (Exception ex)
                {
                    m_mainVM.DeleteContract();
                    m_mainVM.ErrType = ErrorType.Error;
                    m_mainVM.Message = ex.Message;
                }
            }
        }

        public void Export_Contract_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog exportDialog = new SaveFileDialog();
            exportDialog.InitialDirectory = Constants.FolderBrowsePath;
            exportDialog.Filter = "Data Files (*.dat)|*.dat";
            exportDialog.FilterIndex = 1;
            exportDialog.OverwritePrompt = true;

            if (exportDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                m_mainVM.ContractExportPath = exportDialog.FileName;
                m_mainVM.ExportContract();

                m_mainVM.ErrType = ErrorType.Debug;
                m_mainVM.Message = "Contract exported successfully.";
            } 
        }
    }
}
