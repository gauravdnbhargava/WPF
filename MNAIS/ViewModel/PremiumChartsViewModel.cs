using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MNAIS
{    
   public  class PremiumChartsViewModel :ViewModelBase
    {
        public ObservableCollection<KeyValuePair<string, int>> m_barChartValues;

        public ObservableCollection<KeyValuePair<string, int>> BarChartValues
        {
            get
            {
                FillData();               
                return m_barChartValues;
            }

            set
            {
                m_barChartValues = value;
                OnPropertyChanged("BarChartValues");
            }
        }

        public PremiumChartsViewModel()
        { }

        private void FillData()
        {
            m_barChartValues = new ObservableCollection<KeyValuePair<string, int>>();
                        
            m_barChartValues.Add(new KeyValuePair<string, int>("Sample 1", 20));
            m_barChartValues.Add(new KeyValuePair<string, int>("Sample 1", 30));
            m_barChartValues.Add(new KeyValuePair<string, int>("Sample 1", 40));

            m_barChartValues.Add(new KeyValuePair<string, int>("Sample 2", 50));
            m_barChartValues.Add(new KeyValuePair<string, int>("Sample 2", 60));
            m_barChartValues.Add(new KeyValuePair<string, int>("Sample 2", 10));

            m_barChartValues.Add(new KeyValuePair<string, int>("Sample 3", 5));
            m_barChartValues.Add(new KeyValuePair<string, int>("Sample 3", 10));
            m_barChartValues.Add(new KeyValuePair<string, int>("Sample 3", 15));
            
            }
        }
    }

