using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Glass.Basics.Converters {
    public class FlaggedEnumConverter : DependencyObject, IValueConverter {

        public Enum OriginalValue {
            get { return (Enum)GetValue(OriginalValueProperty); }
            set { SetValue(OriginalValueProperty, value); }
        }

        public static readonly DependencyProperty OriginalValueProperty =
            DependencyProperty.Register("OriginalValue", typeof(Enum), typeof(FlaggedEnumConverter), new UIPropertyMetadata(default(Enum)));

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {

            var valueType = value.GetType();

            var originalValue = (Enum) value;
            var converter = new EnumConverter(valueType);
            var flag = (Enum) converter.ConvertFrom(parameter);

            Debug.Assert(flag != null, "flag != null");

            return originalValue.HasFlag(flag);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {

            var booleanValue = (bool) value;

            var converter = new EnumConverter(targetType);
            var flag = (Enum)converter.ConvertFrom(parameter);

            if (booleanValue) {
                dynamic result = OriginalValue;
                result |= flag;
                return result;
            }
            else {
                dynamic result = OriginalValue;
                dynamic dynFlag = flag;
                result &= ~dynFlag;
                return result;
            }
        }
    }
}
