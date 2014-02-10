using System;
using System.Globalization;
using System.Windows.Data;

namespace Glass.Basics.Wpf.Converters.Designer
{
    [ValueConversion(typeof(bool), typeof(bool))]
    public class NegConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return !((bool)value);
        }
    }
}