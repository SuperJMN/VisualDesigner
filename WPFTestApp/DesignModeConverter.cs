using System;
using System.Globalization;
using System.Windows.Data;
using Glass.Design.DesignSurface;

namespace Glass.Design.WpfTester
{
    public class DesignModeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var v = (DesignOperation)value;
            return v == DesignOperation.Move;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            var boolean = (bool)value;
            if (boolean)
            {
                return DesignOperation.Move;
            }
            return DesignOperation.Resize;
        }
    }
}