using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace MNAIS.Utility
{ 
    public class BoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {            
            if (null == value)
            {
                return null;
            }
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {            
            if (null == value)
            {
                return null;
            }
            return !(bool)value;
        }
    } 
}
