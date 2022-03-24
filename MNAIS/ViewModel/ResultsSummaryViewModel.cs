using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Collections.ObjectModel;
using Microsoft.Windows.Controls;
using System.Text;
using System.Xml;
using System.IO;
using MNAIS.Utility;
using System.Windows;
using System.Windows.Markup;
using MNAIS.Views;
using System.Windows.Input;
using System.Windows.Forms;

namespace MNAIS
{
    public class ResultsSummaryViewModel : ViewModelBase
    {
        DataTable objResultsData { get; set; }
        RelayCommand exportResults;

        internal ObservableCollection<Analytics.Result> SimulationResult { get; set; }
        internal ObservableCollection<Analytics.Result> HistoricalResult { get; set; }
        internal MainWindowViewModel mainVM { get; set; }

        #region Public Properties And Methods

        public ICommand ExportResults
        {
            get
            {
                if (exportResults == null)
                    exportResults = new RelayCommand(param => this.Export());
                return exportResults;
            }
        }

        public DataTable Results
        {
            get
            {
                FillData();
                return objResultsData;
            }

            set
            {
                objResultsData = value;
                OnPropertyChanged("Results");
            }
        }

        public ResultsSummaryViewModel() { } 
        
        #endregion

        #region Private Properties And Methods
        
        void FillData()
        {
            ObservableCollection<ResultStructure> data = new ObservableCollection<ResultStructure>();
            ResultStructure newEntry;

            #region General Metrics

            List<StateClass> area = new List<StateClass>();
            List<StateClass> validYears = new List<StateClass>();
            List<StateClass> Years = new List<StateClass>();
            List<StateClass> blank1 = new List<StateClass>();

            foreach (Analytics.Result entry in HistoricalResult)
            {
                area.Add(new StateClass() { Header = entry.BlockName, Value = entry.AreaSown.ToString() });
                validYears.Add(new StateClass() { Header = entry.BlockName, Value = entry.ValidYears.ToString() });
                Years.Add(new StateClass() { Header = entry.BlockName, Value = entry.Years.ToString() });
            }

            newEntry = new ResultStructure() { Catagory = "Data", Description = "Area", Data = area };
            data.Add(newEntry);

            newEntry = new ResultStructure() { Catagory = "", Description = "Number of valid years", Data = validYears };
            data.Add(newEntry);

            newEntry = new ResultStructure() { Catagory = "", Description = "Valid years/optimal years", Data = Years };
            data.Add(newEntry);

            newEntry = new ResultStructure() { Catagory = "", Description = "", Data = blank1 };
            data.Add(newEntry);

            #endregion
                        
            # region Historical Metrics

            List<StateClass> hMeanLossRatio = new List<StateClass>();
            List<StateClass> hStdDev = new List<StateClass>();
            List<StateClass> hPML = new List<StateClass>();
            List<StateClass> hPayoutFreq = new List<StateClass>();
            List<StateClass> blank2 = new List<StateClass>();
            List<StateClass> hDataUncer = new List<StateClass>();
            List<StateClass> hAML = new List<StateClass>();
            List<StateClass> hCatLoad = new List<StateClass>();
            List<StateClass> hTechPremium = new List<StateClass>();
            List<StateClass> hGrossPremium = new List<StateClass>();
            List<StateClass> hCommericalPremium = new List<StateClass>();
            List<StateClass> blank3 = new List<StateClass>();

            foreach (Analytics.Result entry in HistoricalResult)
            {
                hMeanLossRatio.Add(new StateClass() { Header = entry.BlockName, Value = entry.MeanLossRatio.ToString() });
                hStdDev.Add(new StateClass() { Header = entry.BlockName, Value = entry.StdDevLossRatio.ToString() });
                hPML.Add(new StateClass() { Header = entry.BlockName, Value = entry.PML.ToString() });
                hPayoutFreq.Add(new StateClass() { Header = entry.BlockName, Value = entry.Frequency.ToString() });
                hDataUncer.Add(new StateClass() { Header = entry.BlockName, Value = entry.DUL.ToString() });
                hAML.Add(new StateClass() { Header = entry.BlockName, Value = entry.AEL.ToString() });
                hCatLoad.Add(new StateClass() { Header = entry.BlockName, Value = entry.CATLoad.ToString() });
                hTechPremium.Add(new StateClass() { Header = entry.BlockName, Value = entry.TRP.ToString() });
                hGrossPremium.Add(new StateClass() { Header = entry.BlockName, Value = entry.GP.ToString() });
                hCommericalPremium.Add(new StateClass() { Header = entry.BlockName, Value = entry.CPR.ToString() });
            }

            newEntry = new ResultStructure() { Catagory = "Historical Pricing Metrics", Description = "Mean Loss Ratio - Historical", Data = hMeanLossRatio };
            data.Add(newEntry);

            newEntry = new ResultStructure() { Catagory = "", Description = "Standard Deviation", Data = hStdDev };
            data.Add(newEntry);

            newEntry = new ResultStructure() { Catagory = "", Description = "PML", Data = hPML };
            data.Add(newEntry);

            newEntry = new ResultStructure() { Catagory = "", Description = "Frequency of Payout > Mean", Data = hPayoutFreq };
            data.Add(newEntry);
                        
            newEntry = new ResultStructure() { Catagory = "", Description = "", Data = blank2 };
            data.Add(newEntry);

            //Expected Mean Loss = Mean Loss Ratio (Historical)
            newEntry = new ResultStructure() { Catagory = "Historical Premium Calculation", Description = "Expected Mean Loss", Data = hMeanLossRatio };
            data.Add(newEntry);

            newEntry = new ResultStructure() { Catagory = "", Description = "Data Uncertainity", Data = hDataUncer };
            data.Add(newEntry);

            newEntry = new ResultStructure() { Catagory = "", Description = "Adjusted Mean Loss", Data = hAML };
            data.Add(newEntry);

            newEntry = new ResultStructure() { Catagory = "", Description = "Charge for Cost of Capital - Cat Load", Data = hCatLoad };
            data.Add(newEntry);
            
            newEntry = new ResultStructure() { Catagory = "", Description = "Technical Risk Premium", Data = hTechPremium };
            data.Add(newEntry);
            
            newEntry = new ResultStructure() { Catagory = "", Description = "Gross Premium - Stochastic (A&O Expenses + Tax)", Data = hGrossPremium };
            data.Add(newEntry);

            newEntry = new ResultStructure() { Catagory = "", Description = "Commercial Premium Rate with Profit", Data = hCommericalPremium };
            data.Add(newEntry);

            newEntry = new ResultStructure() { Catagory = "", Description = "", Data = blank3 };
            data.Add(newEntry);

            #endregion

            #region Simulation Metrics

            List<StateClass> meanLossRatio = new List<StateClass>();
            List<StateClass> stdDevLossRatio = new List<StateClass>();
            List<StateClass> PML = new List<StateClass>();
            List<StateClass> PayoutFreq = new List<StateClass>();
            List<StateClass> blank4 = new List<StateClass>();            
            List<StateClass> DataUncertainity = new List<StateClass>();
            List<StateClass> AdjustedMeanLoss = new List<StateClass>();
            List<StateClass> CATLoad = new List<StateClass>();
            List<StateClass> TechPremium = new List<StateClass>();
            List<StateClass> AOExpenses = new List<StateClass>();
            List<StateClass> CommercialPremiumRate = new List<StateClass>();
            List<StateClass> blank5 = new List<StateClass>();

            foreach (Analytics.Result entry in SimulationResult)
            {
                meanLossRatio.Add(new StateClass() { Header = entry.BlockName, Value = entry.MeanLossRatio.ToString() });
                stdDevLossRatio.Add(new StateClass() { Header = entry.BlockName, Value = entry.StdDevLossRatio.ToString() });
                PML.Add(new StateClass() { Header = entry.BlockName, Value = entry.PML.ToString() });
                PayoutFreq.Add(new StateClass() { Header = entry.BlockName, Value = entry.Frequency.ToString() });                
                DataUncertainity.Add(new StateClass() { Header = entry.BlockName, Value = entry.DUL.ToString() });
                AdjustedMeanLoss.Add(new StateClass() { Header = entry.BlockName, Value = entry.AEL.ToString() });
                CATLoad.Add(new StateClass() { Header = entry.BlockName, Value = entry.CATLoad.ToString() });
                TechPremium.Add(new StateClass() { Header = entry.BlockName, Value = entry.TRP.ToString() });
                AOExpenses.Add(new StateClass() { Header = entry.BlockName, Value = entry.GP.ToString() });
                CommercialPremiumRate.Add(new StateClass() { Header = entry.BlockName, Value = entry.CPR.ToString() });
            }

            newEntry = new ResultStructure() { Catagory = "Simulation Pricing Metrics", Description = "Mean Loss Ratio - Simulation", Data = meanLossRatio };
            data.Add(newEntry);

            newEntry = new ResultStructure() { Catagory = "", Description = "Standard Deviation", Data = stdDevLossRatio };
            data.Add(newEntry);
            
            newEntry = new ResultStructure() { Catagory = "", Description = "PML", Data = PML };
            data.Add(newEntry);

            newEntry = new ResultStructure() { Catagory = "", Description = "Frequency of Payout > Mean", Data = PayoutFreq };
            data.Add(newEntry);

            newEntry = new ResultStructure() { Catagory = "", Description = "", Data = blank4 };
            data.Add(newEntry);

            //Expected Mean Loss = Mean Loss Ratio (Historical)
            newEntry = new ResultStructure() { Catagory = "Simulation Premium Calculation", Description = "Expected Mean Loss", Data = meanLossRatio };
            data.Add(newEntry);

            newEntry = new ResultStructure() { Catagory = "", Description = "Data Uncertainity", Data = DataUncertainity };
            data.Add(newEntry);

            newEntry = new ResultStructure() { Catagory = "", Description = "Adjusted Mean Loss", Data = AdjustedMeanLoss };
            data.Add(newEntry);

            newEntry = new ResultStructure() { Catagory = "", Description = "Charge for Cost of Capital - Cat Load", Data = CATLoad };
            data.Add(newEntry);

            newEntry = new ResultStructure() { Catagory = "", Description = "Technical Premium", Data = TechPremium };
            data.Add(newEntry);
                        
            newEntry = new ResultStructure() { Catagory = "", Description = "Gross Premium - Historical (A&O Expenses + Tax)", Data = AOExpenses };
            data.Add(newEntry);

            newEntry = new ResultStructure() { Catagory = "", Description = "Commercial Premium Rate with Profit", Data = CommercialPremiumRate };
            data.Add(newEntry);

            newEntry = new ResultStructure() { Catagory = "", Description = "", Data = blank5 };
            data.Add(newEntry);

            # endregion

            ConvertData(data);
        }

        void Export()
        {
            SaveFileDialog exportDialog = new SaveFileDialog();
            exportDialog.InitialDirectory = Convert.ToString(Environment.SpecialFolder.MyDocuments);
            exportDialog.Filter = "Result Files (*.csv)|*.csv";
            exportDialog.FilterIndex = 1;
            exportDialog.OverwritePrompt = true;

            if (exportDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {   
                Utility.Utility.ExportToCSV(Results, exportDialog.FileName);
            }
        }

        void ConvertData(ObservableCollection<ResultStructure> import)
        {
            try
            {
                objResultsData = new DataTable();
                DataRow objDataRow;

                Type itemType = typeof(ResultStructure);
                PropertyInfo[] props = itemType.GetProperties(BindingFlags.Public | BindingFlags.Instance);

                for (int colPropCount = 0; colPropCount < props.Length - 1; colPropCount++)
                    objResultsData.Columns.Add(props[colPropCount].Name);

                for (int colCount = 0; colCount < import[0].Data.Count; colCount++)
                    objResultsData.Columns.Add(import[0].Data[colCount].Header);

                foreach (ResultStructure entry in import)
                {
                    objDataRow = objResultsData.NewRow();
                    objDataRow[0] = entry.Catagory;
                    objDataRow[1] = entry.Description;

                    for (int value = 0; value < entry.Data.Count; value++)
                    {
                        if (entry.Description == "Area" || entry.Description == "Number of valid years" || entry.Description == "Valid years/optimal years")
                        {
                            double dec = Math.Round(Convert.ToDouble(entry.Data[value].Value), mainVM.AdminVM.SelectedPrecision, MidpointRounding.AwayFromZero);
                            objDataRow[value + 2] = dec;
                        }
                        else
                        {
                            if (mainVM.AdminVM.SelectedResultsDisplayOption == ResultsDisplay.Decimal)
                            {
                                double dec = Math.Round(Convert.ToDouble(entry.Data[value].Value), mainVM.AdminVM.SelectedPrecision, MidpointRounding.AwayFromZero);
                                objDataRow[value + 2] = dec;
                            }
                            else if (mainVM.AdminVM.SelectedResultsDisplayOption == ResultsDisplay.Percentage)
                            {
                                double percent = Convert.ToDouble(entry.Data[value].Value) * 100;
                                objDataRow[value + 2] = Math.Round(percent, mainVM.AdminVM.SelectedPrecision, MidpointRounding.AwayFromZero) + " %";
                            }
                        }
                    }

                    objResultsData.Rows.Add(objDataRow);
                }
                mainVM.ErrType = ErrorType.Debug;
                mainVM.Message = "Results successfully generated.";
            }
            catch (Exception ex)
            {
                mainVM.ErrType = ErrorType.Error;
                mainVM.Message = ex.Message;
                objResultsData = null;
            }
        } 
        
        #endregion
    }
}
