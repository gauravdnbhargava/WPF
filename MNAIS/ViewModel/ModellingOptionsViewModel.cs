using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml.Serialization;
using System.Windows.Input;

namespace MNAIS
{
    [Serializable]
    public class ModellingOptionsViewModel : ViewModelBase
    {
        ObservableCollection<int> m_calamityYears = new ObservableCollection<int>();
        ObservableCollection<int> m_excludedYears = new ObservableCollection<int>();
        
        ObservableCollection<int> m_movingAverageYears;
        ObservableCollection<int> m_selectedExcludedYears;
        ObservableCollection<int> m_selectedCalamityYears;
                
        SignificanceForTrending selectedSignificanceForTrending;
        DataProcessed selecteddataProcessed;
        DataGaps selecteddataGaps;
        ResultYieldGraphs correlationInput;
        SimulationDistribution selectedSimulationDistribution;
        StdDev selectedTotalVariationStdDev;
        ExtremeBoundMethod selectedExtremeBoundMethodology = ExtremeBoundMethod.PercentExtreme;
        Calamity selectedCorrelationOutliers = Calamity.Included;
        Model selectedModel;
        HistoricalMethod selectedHistoricalMethod;
        SimulationMethod selectedSimulationMethod;
        SimulationPoints selectedSimulationPoint = SimulationPoints.TwoThousand;

        int movingAverage = 7;
        int minimumMovingAvgYears = 1;
        string selectedText;
        int? above = 100;
        int? below = 0;

        [NonSerialized]
        internal MainWindowViewModel mainVM;

        public ModellingOptionsViewModel() { }

        public ModellingOptionsViewModel(MainWindowViewModel main)
        {
            mainVM = main;
        }

        public int MovingAverage
        {
            get 
            {                
                return movingAverage; 
            }

            set
            {
                if (mainVM != null)
                {
                    if (value != movingAverage)
                        mainVM.ClearResults();
                }

                movingAverage = value;
                OnPropertyChanged("MovingAverage");
            }
        }

        public int MinimumMovingAvgYears
        {
            get 
            {                
                return minimumMovingAvgYears; 
            }

            set
            {
                minimumMovingAvgYears = value;
                OnPropertyChanged("MinimumMovingAvgYears");
            }
        }

        public SimulationPoints SelectedSimulationPoint
        {
            get 
            {
                return selectedSimulationPoint; 
            }

            set
            {
                selectedSimulationPoint = value;
                OnPropertyChanged("SelectedSimulationPoint");
            }
        }

        public SignificanceForTrending SelectedSignificanceForTrending
        {
            get { return selectedSignificanceForTrending; }

            set
            {
                if (mainVM != null)
                {
                    if (value != selectedSignificanceForTrending)
                        mainVM.ClearResults();
                }
               
                selectedSignificanceForTrending = value;                
                OnPropertyChanged("SelectedSignificanceForTrending");
            }
        }

        public int? Above
        {
            get { return above; }

            set
            {
                above = value;

                if (value != null)
                {
                    if (SelectedExtremeBoundMethodology == ExtremeBoundMethod.PercentExtreme)
                    {
                        if (!(value >= 0 && value <= 100))
                            throw new ApplicationException("Range is between 0 and 100");
                    }
                    else
                    {
                        if (!(value >= 0 && value <= 10))
                            throw new ApplicationException("Range is between 0 and 10");
                    }                    
                }
                OnPropertyChanged("Above");
            }
        }

        public int? Below
        {
            get { return below; }

            set
            {
                below = value;

                if (value != null)
                {
                    if (SelectedExtremeBoundMethodology == ExtremeBoundMethod.PercentExtreme)
                    {
                        if (!(value >= 0 && value <= 100))
                            throw new ApplicationException("Range is between 0 and 100");
                    }
                    else
                    {
                        if (!(value >= 0 && value <= 10))
                            throw new ApplicationException("Range is between 0 and 10");
                    }                    
                }
                OnPropertyChanged("Below");
            }
        }

        public DataProcessed SelectedDataProcessed
        {
            get { return selecteddataProcessed; }

            set
            {
                selecteddataProcessed = value;
                OnPropertyChanged("SelectedDataProcessed");
            }
        }

        public DataGaps SelectedDataGaps
        {
            get { return selecteddataGaps; }

            set
            {
                if(value!=selecteddataGaps)
                    mainVM.ClearResults();

                selecteddataGaps = value;
                OnPropertyChanged("SelectedDataGaps");
            }
        }

        public ResultYieldGraphs CorrelationInput
        {
            get { return correlationInput; }

            set
            {
                if (mainVM != null)
                {
                    if (value != correlationInput)
                        mainVM.ClearResults();
                }

                correlationInput = value;
                OnPropertyChanged("CorrelationInput");
            }
        }

        public Calamity SelectedCorrelationOutliers
        {
            get { return selectedCorrelationOutliers; }

            set
            {
                selectedCorrelationOutliers = value;
                OnPropertyChanged("SelectedCorrelationOutliers");
            }
        }

        public StdDev SelectedTotalVariationStdDev
        {
            get { return selectedTotalVariationStdDev; }

            set
            {
                selectedTotalVariationStdDev = value;
                OnPropertyChanged("SelectedTotalVariationStdDev");
            }
        }

        public SimulationDistribution SelectedSimulationDistribution
        {
            get { return selectedSimulationDistribution; }

            set
            {
                if (value != selectedSimulationDistribution)
                    mainVM.ClearResults();

                selectedSimulationDistribution = value;
                OnPropertyChanged("SelectedSimulationDistribution");
            }
        }

        public ExtremeBoundMethod SelectedExtremeBoundMethodology
        {
            get { return selectedExtremeBoundMethodology; }

            set
            {
                selectedExtremeBoundMethodology = value;
                OnPropertyChanged("SelectedExtremeBoundMethodology");
            }
        }

        public Model SelectedModel
        {
            get { return selectedModel; }

            set
            {
                if (mainVM != null)
                {
                    if (value != selectedModel)
                        mainVM.ClearResults();
                }

                selectedModel = value;
                OnPropertyChanged("SelectedModel");
            }
        }

        public SimulationMethod SelectedSimulationMethod
        {
            get { return selectedSimulationMethod; }

            set
            {
                if (mainVM != null)
                {
                    if (value != selectedSimulationMethod)
                        mainVM.ClearResults();
                }

                selectedSimulationMethod = value;
                OnPropertyChanged("SelectedSimulationMethod");
            }
        }

        public HistoricalMethod SelectedHistoricalMethod
        {
            get { return selectedHistoricalMethod; }

            set
            {
                if (mainVM != null)
                {
                    if (value != selectedHistoricalMethod)
                        mainVM.ClearResults();
                }

                selectedHistoricalMethod = value;
                OnPropertyChanged("SelectedHistoricalMethod");
            }
        }

        public ObservableCollection<int> SelectedExcludedYears
        {
            get 
            {
                if (m_selectedExcludedYears == null)
                {
                    m_selectedExcludedYears = new ObservableCollection<int>();

                    SelectedText = WriteSelectedString(m_selectedExcludedYears);

                    m_selectedExcludedYears.CollectionChanged +=
                        (s, e) =>
                        {
                            SelectedText = WriteSelectedString(m_selectedExcludedYears);
                            OnPropertyChanged("SelectedExcludedYears");
                        };
                }
                return m_selectedExcludedYears; 
            }

            set
            {
                m_selectedExcludedYears = value;
                OnPropertyChanged("SelectedExcludedYears");
            }
        }

        public ObservableCollection<int> SelectedCalamityYears
        {
            get
            {
                if (m_selectedCalamityYears == null)
                {
                    m_selectedCalamityYears = new ObservableCollection<int>();

                    SelectedText = WriteSelectedString(m_selectedCalamityYears);

                    m_selectedExcludedYears.CollectionChanged +=
                        (s, e) =>
                        {
                            SelectedText = WriteSelectedString(m_selectedCalamityYears);
                            OnPropertyChanged("SelectedCalamityYears");
                        };
                }
                return m_selectedCalamityYears;
            }

            set
            {
                m_selectedCalamityYears = value;
                OnPropertyChanged("SelectedCalamityYears");
            }
        }

        public string SelectedText
        {
            get { return selectedText; }
            set
            {
                selectedText = value;
                OnPropertyChanged("SelectedText");
            }
        }
        
        public ObservableCollection<int> CalamityYears
        {
            get
            {
                if (m_calamityYears.Count == 0)
                {
                    FillCalamityYears();
                }
                return m_calamityYears;
            }

            set
            {
                m_calamityYears = value;
                OnPropertyChanged("CalamityYears");
            }
        }

        public ObservableCollection<int> ExcludedYears
        {
            get
            {
                if (m_excludedYears.Count == 0)
                {
                    FillExcludedYears();
                }
                return m_excludedYears;
            }

            set
            {
                m_excludedYears = value;
                OnPropertyChanged("ExcludedYears");
            }
        }
                
        public ObservableCollection<int> MovingAverageYears
        {
            get
            {
                m_movingAverageYears = new ObservableCollection<int>();
                m_movingAverageYears.Add(1);
                m_movingAverageYears.Add(2);
                m_movingAverageYears.Add(3);
                m_movingAverageYears.Add(4);
                m_movingAverageYears.Add(5);
                m_movingAverageYears.Add(6);
                m_movingAverageYears.Add(7);
                m_movingAverageYears.Add(8);
                m_movingAverageYears.Add(9);
                m_movingAverageYears.Add(10);

                return m_movingAverageYears;
            }
        }
        
        internal void FillCalamityYears()
        {
            //Data Start Year  to (Contract Year-1)
            if (mainVM.ImportDataVM.StartYear != null)
            {
                int dataStartYear = Convert.ToInt32(mainVM.ImportDataVM.StartYear);
                int contractYear = Convert.ToInt32(mainVM.CreateContractVM.ContractYear) - 1;

                int count = contractYear - dataStartYear;

                for (int i = 1; i <= count; i++)
                {
                    m_calamityYears.Add(dataStartYear);
                    dataStartYear++;
                }
            }
        }

        internal void FillExcludedYears()
        {
            //TODO : Temp logic to be clarified later
            if (mainVM.ImportDataVM.StartYear != null)
            {
                int dataStartYear = Convert.ToInt32(mainVM.ImportDataVM.StartYear);
                int contractYear = Convert.ToInt32(mainVM.CreateContractVM.ContractYear) - 1;

                int count = contractYear - dataStartYear;

                for (int i = 1; i <= count; i++)
                {
                    m_excludedYears.Add(dataStartYear);
                    dataStartYear++;
                }
            }            
        }
                
        string WriteSelectedString(IList<int> list)
        {
            if (list.Count == 0)
                return String.Empty;

            StringBuilder builder = new StringBuilder(list[0]);

            for (int i = 1; i < list.Count; i++)
            {
                builder.Append(",");
                builder.Append(list[i]);
            }

            return builder.ToString();
        }     
    }
}
