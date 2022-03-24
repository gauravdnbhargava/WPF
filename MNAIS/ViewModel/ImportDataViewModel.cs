using System;
using System.Text;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Controls;
using System.Xml;
using System.IO;
using System.Windows;
using System.Windows.Markup;
using MNAIS.Views;
using MNAIS.Utility;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;

namespace MNAIS
{
    [Serializable]    
    public class ImportDataViewModel : ViewModelBase
    {
        internal ObservableCollection<ImportedData> import;

        ObservableCollection<string> yieldUnit;

        AreaSownUnit selectedAreaSownUnit;
        string selectedYieldUnit;
        YieldAvailable isYieldAvailable = YieldAvailable.No;
        ImportedData importedData;
        string importFilePath;
        int? startYear;
        int? endYear;
        int? noOfBlocks;

        [NonSerialized]
        MainWindowViewModel _mainVM;

        [NonSerialized]
        ImportData _importView;

        [NonSerialized]
        RelayCommand browseUserData;

        [NonSerialized]
        RelayCommand exportUserData;

        [NonSerialized]
        RelayCommand generateTemplate;
        
        #region Public Methods & Properties

        public MainWindowViewModel mainVM
        {
            get { return _mainVM; }

            set { _mainVM = value; OnPropertyChanged("mainVM"); }
        }

        public ImportData importView
        {
            get { return _importView; }

            set { _importView = value; OnPropertyChanged("importView"); }
        }

        public int? StartYear
        {
            get { return startYear; }

            set
            {
                startYear = value;

                if (value != null)
                {
                    if (Utility.Utility.GetIntegerDigitCount(value) > 4)
                        throw new ApplicationException("Length cannot be greater than 4");

                    if (Utility.Utility.GetIntegerDigitCount(value) == 4)
                    {
                        if (Convert.ToInt32(value) < 1900)
                            throw new ApplicationException("Year cannot be less than 1900");

                        if (Convert.ToInt32(value) > 2005)
                            throw new ApplicationException("Year cannot be greater than 2005");
                    }
                }
                
                OnPropertyChanged("StartYear");
            }
        }

        public int? EndYear
        {
            get { return endYear; }

            set
            {
                endYear = value;
                if (value != null)
                {
                    if (Utility.Utility.GetIntegerDigitCount(value) == 4)
                    {
                        if (Convert.ToInt32(StartYear) == 0)
                            throw new ApplicationException("Start Year cannot be blank");

                        if (Convert.ToInt32(StartYear) > Convert.ToInt32(value))
                            throw new ApplicationException("End Year cannot be less than Start Year");

                        if (Convert.ToInt32(mainVM.CreateContractVM.ContractYear) < Convert.ToInt32(value))
                            throw new ApplicationException("Contract Year cannot be less than End Year");
                    }
                }
                
                OnPropertyChanged("EndYear");
            }
        }

        public int? NoOfBlocks
        {
            get { return noOfBlocks; }

            set
            {
                noOfBlocks = value;
                OnPropertyChanged("NoOfBlocks");
            }
        }

        public string ImportFilePath
        {
            get { return importFilePath; }

            set
            {
                importFilePath = value;
                OnPropertyChanged("ImportFilePath");
            }
        }

        public YieldAvailable IsYieldAvailable
        {
            get { return isYieldAvailable; }

            set
            {
                isYieldAvailable = value;
                OnPropertyChanged("IsYieldAvailable");
            }
        }

        public AreaSownUnit SelectedAreaSownUnit
        {
            get { return selectedAreaSownUnit; }

            set
            {
                selectedAreaSownUnit = value;
                OnPropertyChanged("SelectedAreaSownUnit");
                OnPropertyChanged("YieldUnit");
                OnPropertyChanged("SelectedYieldUnit");
            }
        }

        public string SelectedYieldUnit
        {
            get 
            {
                selectedYieldUnit = yieldUnit[0];
                return selectedYieldUnit; 
            }

            set
            {
                selectedYieldUnit = value;
                OnPropertyChanged("SelectedYieldUnit");
            }
        }

        public ObservableCollection<string> YieldUnit
        {
            get
            {
                if (selectedAreaSownUnit == AreaSownUnit.Ha)
                {
                    yieldUnit = new ObservableCollection<string>();
                    yieldUnit.Add("Kg/Ha");
                    yieldUnit.Add("Quintal/Ha");
                    yieldUnit.Add("Ton/Ha");
                }
                else
                {
                    yieldUnit = new ObservableCollection<string>();
                    yieldUnit.Add("Kg/Acre");
                    yieldUnit.Add("Quintal/Acre");
                    yieldUnit.Add("Ton/Acre");
                }

                return yieldUnit;
            }
        }

        public ICommand BrowseUserData
        {
            get
            {
                if (browseUserData == null)
                    browseUserData = new RelayCommand(param => this.FillData());
                return browseUserData;
            }
        }

        public ICommand ExportUserData
        {
            get
            {
                if (exportUserData == null)
                    exportUserData = new RelayCommand(param => this.ExportData());
                return exportUserData;
            }
        }

        public ICommand GenerateTemplate
        {
            get            
            {
                if (generateTemplate == null)
                    generateTemplate = new RelayCommand(param => this.GenerateFile());
                return generateTemplate;
            }
        }

        public ImportDataViewModel(ImportData uiInstance,MainWindowViewModel main)
        {   
            importView = uiInstance;
            mainVM = main;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Export User Data
        /// </summary>
        /// <param name="filePath"></param>
        void SaveData(string filePath)
        {
            List<StringBuilder> items = new List<StringBuilder>();
            StringBuilder header = new StringBuilder();

            header.Append("Block,");
            header.Append("Block Name,");
            header.Append("Area Sown,");

            foreach (YearClass yr in import.FirstOrDefault().YearData)
                header.Append(yr.Header + ",");

            items.Add(header);

            StringBuilder values;

            foreach (ImportedData entry in import)
            {
                values = new StringBuilder();
                values.Append(entry.Block + ",");
                values.Append(entry.BlockName + ",");
                values.Append(entry.AreaSown + ",");                               

                foreach (YearClass yrData in entry.YearData)
                {
                    values.Append(yrData.Data + ",");
                }
                items.Add(values);
            }

            using (StreamWriter writer = new StreamWriter(filePath))
            {
                foreach (StringBuilder entry in items)
                    writer.WriteLine(entry);

                writer.Close();
            }

            mainVM.ErrType = ErrorType.Debug;
            mainVM.Message = "Yield Data saved successfully at :" + filePath;
        }

        /// <summary>
        /// This is used to populate the Structures
        /// </summary>
        void FillData()
        {
            System.Windows.Forms.OpenFileDialog fdlg = new System.Windows.Forms.OpenFileDialog();
            fdlg.Title = "Select Data file";

            fdlg.InitialDirectory = Constants.FolderBrowsePath;
            fdlg.Filter = "Data files (*.csv)|*.csv";
            fdlg.FilterIndex = 2;
            fdlg.RestoreDirectory = true;
            fdlg.Multiselect = false;

            if (fdlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    ImportFilePath = fdlg.FileName;
                    importedData = new ImportedData();
                    import = importedData.AddImportedData(ImportFilePath);

                    //populate start year, end year and # of blocks.
                    StartYear = Convert.ToInt32(import.FirstOrDefault().YearData.FirstOrDefault().Header.Split('-')[0]);
                    EndYear = Convert.ToInt32(import.FirstOrDefault().YearData.LastOrDefault().Header.Split('-')[0]);
                    NoOfBlocks = Convert.ToInt32(import.LastOrDefault().Block);

                    FillGrid();                   
                    
                    //update the import data object
                    mainVM.ImportDataVM = this;

                    //fill calamity years and excluded years
                    mainVM.ModellingVM.FillCalamityYears();
                    mainVM.ModellingVM.FillExcludedYears();

                    //enable analysis options
                    mainVM.ErrType = ErrorType.Debug;
                    mainVM.IsAnalysisOptionsEnabled = true;
                    mainVM.Message = "Yield Data imported successfully.";
                    
                    //clear results if present.
                    if (mainVM.m_MainWindow.m_ResultSummaryView != null && mainVM.m_resultsSummaryViewModel != null)
                        mainVM.ClearResults();                    
                }
                catch (Exception)
                {
                    if (mainVM.IsAnalysisOptionsEnabled)
                        mainVM.IsAnalysisOptionsEnabled = false;

                    mainVM.ErrType = ErrorType.Warning;
                    mainVM.Message = "Import Data Failed. Please check your file and try again.";

                    //clear import values if import failed.
                    ImportFilePath = string.Empty;
                    StartYear = null;
                    EndYear = null;
                    NoOfBlocks = null;
                }
            }
        }

        /// <summary>
        /// This is used to populate the datagrid in UI.
        /// </summary>
        internal void FillGrid()
        {
            importView.dgImportedData.AutoGenerateColumns = false;
            importView.dgImportedData.Columns.Clear();
            importView.dgImportedData.Columns.Add(CreateTextColumn("Block", "Block"));
            importView.dgImportedData.Columns.Add(CreateTextColumn("BlockName", "Block Name"));
            importView.dgImportedData.Columns.Add(CreateTextColumn("AreaSown", "Area Sown"));

            int RowCount = import[0].YearData.Count;

            for (int i = 0; i < RowCount; i++)
                importView.dgImportedData.Columns.Add(CreateTemplateColumn(i, import[0].YearData[i]));
            

            importView.dgImportedData.ItemsSource = import;
            importView.dgImportedData.CanUserAddRows = false;
            importView.dgImportedData.CanUserResizeColumns = false;
        }
        
        /// <summary>
        /// Command Method
        /// </summary>
        void GenerateFile()
        {
            SaveFileDialog exportDialog = new SaveFileDialog();
            exportDialog.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
            exportDialog.Filter = "Template Files (*.csv)|*.csv";
            exportDialog.FilterIndex = 1;
            exportDialog.OverwritePrompt = true;

            if (exportDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                CreateTemplate(exportDialog.FileName);
            } 
        }

        /// <summary>
        /// Export User Data
        /// </summary>
        void ExportData()
        {
            SaveFileDialog exportDialog = new SaveFileDialog();
            exportDialog.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
            exportDialog.Filter = "Data Files (*.csv)|*.csv";
            exportDialog.FilterIndex = 1;
            exportDialog.OverwritePrompt = true;

            if (exportDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                SaveData(exportDialog.FileName);
            } 
        }

        /// <summary>
        /// This is used to generate the csv file.
        /// </summary>
        void CreateTemplate(string filename)
        {
            List<string> template = new List<string>();
            int noOfYears = Convert.ToInt32(EndYear) - Convert.ToInt32(StartYear);
            int start = Convert.ToInt32(StartYear);
            int Blocks = Convert.ToInt32(NoOfBlocks);
            int i;
            string temp1 = "Block, Block Name, Area Sown,";
            string temp2 = string.Empty;
            
            for (int j = 0; j <= noOfYears; j++)
            {   
                temp2 += start + ",";
                start++; 
            }

            if (IsYieldAvailable == YieldAvailable.Yes)
                i = 0;
            else
                i = 1;
            
            template.Add(temp1 + temp2);

            for (; i <= Blocks; i++)
            {
                template.Add(i.ToString());
            }

            File.WriteAllLines(filename, template);

            mainVM.ErrType = ErrorType.Debug;
            mainVM.Message = "Template exported successfully to " + filename;
        }

        static DataGridTextColumn CreateTextColumn(string fieldName, string title)
        {
            DataGridTextColumn column = new DataGridTextColumn();
            System.Windows.Data.Binding binding = new System.Windows.Data.Binding(fieldName);
            binding.ValidatesOnDataErrors = true;
            binding.Mode = System.Windows.Data.BindingMode.TwoWay;
            binding.UpdateSourceTrigger = System.Windows.Data.UpdateSourceTrigger.Default;
            column.Header = title;
            column.Binding = binding;
            return column;
        }

        string CreateColumnTemplate(int index, string propertyName)
        {
            StringBuilder CellTemp = new StringBuilder();
            CellTemp.Append("<DataTemplate ");
            CellTemp.Append("xmlns='http://schemas.microsoft.com/winfx/"); CellTemp.Append("2006/xaml/presentation' ");
            CellTemp.Append("xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'>");
            CellTemp.Append(String.Format("<TextBlock Text='{{Binding YearData[{0}].{1},UpdateSourceTrigger=PropertyChanged,Mode=TwoWay, ValidatesOnDataErrors = True}}'/>", index, propertyName));
            CellTemp.Append("</DataTemplate>");
            return CellTemp.ToString();
        }

        string CreateColumnEditTemplate(int index, string propertyName)
        {
            StringBuilder CellTemp = new StringBuilder();
            CellTemp.Append("<DataTemplate ");
            CellTemp.Append("xmlns='http://schemas.microsoft.com/winfx/"); CellTemp.Append("2006/xaml/presentation' ");
            CellTemp.Append("xmlns:x='http://schemas.microsoft.com/winfx/2006/xaml'>");
            CellTemp.Append(String.Format("<TextBox Text='{{Binding YearData[{0}].{1}, UpdateSourceTrigger=PropertyChanged,Mode=TwoWay, ValidatesOnDataErrors = True}}'/>", index, propertyName));
            CellTemp.Append("</DataTemplate>");
            return CellTemp.ToString();
        }

        XmlTextReader GetXMLReader(string xaml)
        {
            StringReader sr = new StringReader(xaml);
            XmlTextReader xmlReader = new XmlTextReader(sr);
            return xmlReader;
        }

        DataGridTemplateColumn CreateTemplateColumn(int i, YearClass propName)
        {
            DataGridDynamicColumn column = new DataGridDynamicColumn();
            column.Header = propName.Header;
            column.CellTemplate = (DataTemplate)XamlReader.Load(GetXMLReader(CreateColumnTemplate(i, "Data")));
            column.CellEditingTemplate = (DataTemplate)XamlReader.Load(GetXMLReader(CreateColumnEditTemplate(i, "Data")));
            return column;
        } 
        
        #endregion
    }
}
