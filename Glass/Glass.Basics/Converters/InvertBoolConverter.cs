using System;
using System.Windows.Data;

namespace Glass.Basics.Converters
{
    /// <summary>
    /// WPF/Silverlight ValueConverter : Return inverted boolean (=Value)
    /// </summary>
#if !SILVERLIGHT
    [ValueConversion(typeof(bool), typeof(bool))]
#endif
    public class InvertBoolConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}