using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace MNAIS.Utility
{
    public class PercentageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var fraction = decimal.Parse(value.ToString());
            return fraction.ToString("P0");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (string.IsNullOrEmpty(value.ToString()))
                return null;

            var valueWithoutPercentage = value.ToString().TrimEnd(' ', '%');
            
            if(string.IsNullOrEmpty(valueWithoutPercentage))
                return null;
            else
                return decimal.Parse(valueWithoutPercentage) / 100;
        }
    }
}
