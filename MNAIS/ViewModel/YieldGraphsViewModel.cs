using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MNAIS
{
    public class YieldGraphsViewModel:ViewModelBase
    {
        public ObservableCollection<KeyValuePair<string, int>> m_lineChartValues;

        public ObservableCollection<KeyValuePair<string, int>> LineChartValues
        {
            get
            {
                FillData();     
                return m_lineChartValues;
            }

            set
            {
                m_lineChartValues = value;
                OnPropertyChanged("LineChartValues");
            }
        }

        public YieldGraphsViewModel()
        { }

        private void FillData()
        {
            m_lineChartValues = new ObservableCollection<KeyValuePair<string, int>>();

            m_lineChartValues.Add(new KeyValuePair<string, int>("Sample 1", 2));
            m_lineChartValues.Add(new KeyValuePair<string, int>("Sample 1", 3));
            m_lineChartValues.Add(new KeyValuePair<string, int>("Sample 1", 4));

            m_lineChartValues.Add(new KeyValuePair<string, int>("Sample 2", 5));
            m_lineChartValues.Add(new KeyValuePair<string, int>("Sample 2", 6));
            m_lineChartValues.Add(new KeyValuePair<string, int>("Sample 2", 7));

            m_lineChartValues.Add(new KeyValuePair<string, int>("Sample 3", 8));
            m_lineChartValues.Add(new KeyValuePair<string, int>("Sample 3", 9));
            m_lineChartValues.Add(new KeyValuePair<string, int>("Sample 3", 10));
        }
    }
}
