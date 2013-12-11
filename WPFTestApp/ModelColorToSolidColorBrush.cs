using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;
using AutoMapper;
using Color = SampleModel.Color;

namespace Glass.Design.WpfTester
{
    public class ModelColorToSolidColorBrush : IValueConverter 
    {
        static ModelColorToSolidColorBrush()
        {
            Mapper.CreateMap<Color, System.Windows.Media.Color>().ForMember(color => color.A, expression => expression.UseValue(255));
            Mapper.CreateMap<System.Windows.Media.Color, Color>().ForMember(color => color.A, expression => expression.UseValue(255));
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var color = Mapper.Map<System.Windows.Media.Color>(value);
            var solidColorBrush = new SolidColorBrush(color);
            return solidColorBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var solidColorBrush = (SolidColorBrush) value;
            var colorFromBrush = solidColorBrush.Color;
            var color = Mapper.Map<Color>(colorFromBrush);
            return color;            
        }
    }
}