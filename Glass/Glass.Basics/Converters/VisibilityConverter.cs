using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Glass.Basics.Converters
{

    /// <summary>  
    /// WPF/Silverlight ValueConverter : Convert boolean to XAML Visibility
    /// </summary>  
#if !SILVERLIGHT
    [ValueConversion(typeof(bool), typeof(Visibility))]
#endif
    public class VisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (value != null && (bool)value) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var visibility = (Visibility)value;
            return (visibility == Visibility.Visible);
        }
    }
}