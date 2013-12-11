using System;
using System.Globalization;
using System.Windows.Data;

namespace Glass.Basics.Converters.Designer
{
    public class PercentageConverter : IValueConverter
    {
        private const double Epsilon = 0.1;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = value as string;
            if (s != null)
            {
                var @string = s;
                var percentFormatted = false;
                if (@string.Contains("%"))
                {
                    @string = @string.Replace("%", string.Empty);
                    percentFormatted = true;
                }

                double convertedValue;

                var success = double.TryParse(@string, out convertedValue);

                if (success)
                {
                    if (Math.Abs(convertedValue - 0) < Epsilon)
                    {
                        return 0;
                    }

                    return percentFormatted ? convertedValue / 100 : convertedValue;
                }
                return 0;
            }

            return Binding.DoNothing;
        }
    }
}