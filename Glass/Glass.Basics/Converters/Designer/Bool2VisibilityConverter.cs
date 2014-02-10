using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Glass.Basics.Wpf.Converters.Designer
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class Bool2VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var converted = (bool)value;
            return converted ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var converted = (Visibility)value;
            return converted == Visibility.Visible;
        }
    }
}