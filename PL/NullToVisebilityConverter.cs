using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Globalization;
using System.Windows;

namespace PL;

public class NullToVisebilityConverter:IValueConverter
{
    public object Convert(object value,Type targetType,object parameter,CultureInfo culture)
    {
        DateTime? dateTimeValue=(DateTime)value;
        if (dateTimeValue==null)
            return Visibility.Visible;
        else
            return Visibility.Hidden;
    }
    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
