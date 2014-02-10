using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Glass.Basics.Wpf.Converters.Designer
{
    [ValueConversion(typeof(Visibility), typeof(bool))]
    public class Visibility2BoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var converted = (Visibility)value;
            return converted == Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var converted = (bool)value;
            return converted ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}