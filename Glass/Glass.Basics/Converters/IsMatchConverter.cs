using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Glass.Basics.Converters
{
    /// <summary>
    /// WPF/Silverlight ValueConverter : does Value match the regular expression (=Parameter) ?
    /// </summary>
#if !SILVERLIGHT
    [ValueConversion(typeof(string), typeof(bool), ParameterType = typeof(string))]
#endif
// ReSharper disable UnusedMember.Global
    public class IsMatchConverter : IValueConverter
// ReSharper restore UnusedMember.Global
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value == null) || (parameter == null))
            {
                return false;
            }

            var regex = new Regex((string)parameter);
            return regex.IsMatch((string)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}