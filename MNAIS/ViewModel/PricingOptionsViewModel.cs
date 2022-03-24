using System;
using System.Windows.Input;
using MNAIS.Analytics;
using System.IO;

namespace MNAIS
{
    [Serializable]
    public class PricingOptionsViewModel : ViewModelBase
    {
        [NonSerialized]
        RelayCommand runCommand;
        
        double? optimalNoOfYears = 30;
        double? returnPeriod = 100;
        double? standardDeviation = 0.6;
        double? aoExpenses;
        double? serviceTax;
        double? profitMargin;
        double? confidenceLevel = 0.9;
        double? uncertainityValue;
        double? franchise;
        double? returnOnCapital;

        DataLoading selectedDataUncertainityLoading;
        HistoricalPML selectedHistoricalPML;
        ReturnOnCapital selectedReturnOnCapital;
        Franchise selectedFranchise;

        bool isRunAnalyticsEnabled;                
        bool isUncertainityEnabled = false;
        bool isReturnonCapitalEnabled = false;

        [NonSerialized]
        internal MainWindowViewModel main;

        public PricingOptionsViewModel() { }

        public ICommand RunCommand
        {
            get
            {
                if (runCommand == null)
                    runCommand = new RelayCommand(param => this.ExecuteAnalytics());
                return runCommand;
            }
        }

        public double? ReturnOnCaptial
        {
            get { return returnOnCapital; }

            set
            {
                if (main != null)
                {
                    if (value != returnOnCapital)
                        main.ClearResults();
                }

                returnOnCapital = value;
                if (value != null)
                {
                    if (!(value >= 0 && value <= 1))
                        throw new ApplicationException("Range is between 0 and 100");
                }
                
                OnPropertyChanged("ReturnOnCaptial");
            }
        }

        public bool IsReturnOnCapitalEnabled
        {
            get { return isReturnonCapitalEnabled; }

            set
            {
                isReturnonCapitalEnabled = value;
                OnPropertyChanged("IsReturnOnCapitalEnabled");
            }
        }

        public double? Franchise
        {
            get { return franchise; }

            set
            {
                if (main != null)
                {
                    if (value != franchise)
                        main.ClearResults();
                }

                franchise = value;
                if (value != null)
                {
                    if (value >= 0 && value <= 1)
                    {
                        isRunAnalyticsEnabled = true;
                    }
                    else
                    {
                        if (isRunAnalyticsEnabled)
                            isRunAnalyticsEnabled = false;

                        throw new ApplicationException("Range is between 0 and 100");
                    }
                }
                else
                    isRunAnalyticsEnabled = false;

                OnPropertyChanged("IsRunAnalyticsEnabled");
                OnPropertyChanged("Franchise");
            }
        }

        public double? UncertainityValue
        {
            get { return uncertainityValue; }

            set
            {
                if (main != null)
                {
                    if (value != uncertainityValue)
                        main.ClearResults();
                }

                uncertainityValue = value;
                if (value != null)
                {
                    if (!(value >= 0 && value <= 10))
                        throw new ApplicationException("Range is between 0 and 1000");
                }
                
                OnPropertyChanged("UncertainityValue");
            }
        }

        public double? ReturnPeriod
        {
            get { return returnPeriod; }

            set 
            {
                if (main != null)
                {
                    if (value != returnPeriod)
                        main.ClearResults();
                }

                returnPeriod = value;
                if (value != null)
                {
                    if (!(value >= 1 && value <= 1000))
                        throw new ApplicationException("Range is between 1 and 1000");
                }
                OnPropertyChanged("ReturnPeriod");
            }
        }
        
        public double? OptimalNoOfYears
        {
            get { return optimalNoOfYears; }

            set
            {
                if (main != null)
                {
                    if (value != optimalNoOfYears)
                        main.ClearResults();
                }

                optimalNoOfYears = value;
                if (value != null)
                {
                    if (!(value > 7 && value < 100))
                        throw new ApplicationException("Value should be between 7 & 100");
                }
                OnPropertyChanged("OptimalNoOfYears");
            }
        }

        public double? ConfidenceLevel
        {
            get { return confidenceLevel; }

            set
            {
                if (main != null)
                {
                    if (value != confidenceLevel)
                        main.ClearResults();
                }

                confidenceLevel = value;
                if (value != null)
                {
                    if (!(value > 0 && value < 1))                    
                        throw new ApplicationException("Value should be between 0 & 99.99");
                }
                OnPropertyChanged("ConfidenceLevel");
            }
        }

        public double? StandardDeviation
        {
            get { return standardDeviation; }

            set
            {
                if (main != null)
                {
                    if (value != standardDeviation)
                        main.ClearResults();
                }

                standardDeviation = value;               
                if (value != null)
                {
                    if (!(value > 0 && value <= 10))
                        throw new ApplicationException("Value should be between 0 & 1000");
                }
                OnPropertyChanged("StandardDeviation");
            }
        }

        public DataLoading SelectedDataUncertainityLoading
        {
            get { return selectedDataUncertainityLoading; }

            set
            {
                if (main != null)
                {
                    if (value != selectedDataUncertainityLoading)
                        main.ClearResults();
                }

                selectedDataUncertainityLoading = value;

                if (value == DataLoading.Calculated)
                    isUncertainityEnabled = false;
                else
                    isUncertainityEnabled = true;

                OnPropertyChanged("IsUncertainityEnabled");
                OnPropertyChanged("SelectedDataUncertainityLoading");
            }
        }

        public bool IsUncertainityEnabled
        {
            get { return isUncertainityEnabled; }

            set
            {
                isUncertainityEnabled = value;
                OnPropertyChanged("IsUncertainityEnabled");
            }
        }

        public HistoricalPML SelectedHistoricalPML
        {
            get { return selectedHistoricalPML; }

            set
            {
                if (main != null)
                {
                    if (value != selectedHistoricalPML)
                        main.ClearResults();
                }

                selectedHistoricalPML = value;               
                OnPropertyChanged("SelectedHistoricalPML");
            }
        }

        public double? AOExpenses
        {
            get { return aoExpenses; }

            set
            {
                if (main != null)
                {
                    if (value != aoExpenses)
                        main.ClearResults();
                }

                aoExpenses = value;
                OnPropertyChanged("AOExpenses");
            }
        }

        public double? ServiceTax
        {
            get { return serviceTax; }

            set
            {
                if (main != null)
                {
                    if (value != serviceTax)
                        main.ClearResults();
                }

                serviceTax = value;
                OnPropertyChanged("ServiceTax");
            }
        }

        public double? ProfitMargin
        {
            get { return profitMargin; }

            set
            {
                if (main != null)
                {
                    if (value != profitMargin)
                        main.ClearResults();
                }

                profitMargin = value;
                OnPropertyChanged("ProfitMargin");
            }
        }
                
        public ReturnOnCapital SelectedReturnOnCapital
        {
            get { return selectedReturnOnCapital; }

            set
            {
                if (main != null)
                {
                    if (value != selectedReturnOnCapital)
                        main.ClearResults();
                }

                selectedReturnOnCapital = value;

                if (value == ReturnOnCapital.UserSpecified)
                    isReturnonCapitalEnabled = true;
                else
                    isReturnonCapitalEnabled = false;

                OnPropertyChanged("IsReturnOnCapitalEnabled");
                OnPropertyChanged("SelectedReturnOnCapital");
            }
        }

        public Franchise SelectedFranchise
        {
            get { return selectedFranchise; }

            set
            {
                if (main != null)
                {
                    if (value != selectedFranchise)
                        main.ClearResults();
                }

                selectedFranchise = value;
                OnPropertyChanged("SelectedFranchise");
            }
        }

        public PricingOptionsViewModel(MainWindowViewModel mainVM)
        {
            main = mainVM;
        }
                
        public bool IsRunAnalyticsEnabled
        {
            get { return isRunAnalyticsEnabled; }

            set
            {
                isRunAnalyticsEnabled = value;
                OnPropertyChanged("IsRunAnalyticsEnabled");
            }
        }

        void ExecuteAnalytics()
        {
            try
            {
                if (main.ImportDataVM.import == null)
                    throw new Exception("Imported Data not found.");

                Analytics.Analytics.mainVM = main;
                Analytics.Analytics.Run();
                main.IsResultsEnabled = true;
            }
            catch (Exception ex)
            {
                if (main.IsResultsEnabled)
                    main.IsResultsEnabled = false;

                main.ErrType = ErrorType.Error;
                main.Message = ex.Message;
                Logging.LogError(ErrorType.Error, ex.Source, ex.Message);
            }
        }       
    }
}
