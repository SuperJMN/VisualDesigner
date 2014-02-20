using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using AutoMapper;

namespace ComicDesigner.PropertyPages
{
    public class ModelColorToSolidColorBrushConverter : IValueConverter 
    {
        static ModelColorToSolidColorBrushConverter()
        {
            Mapper.CreateMap<Color, Model.Color>().ForMember(color => color.A, expression => expression.UseValue(255));
            Mapper.CreateMap<Model.Color, Color>().ForMember(color => color.A, expression => expression.UseValue(255));
        }

        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            var color = Mapper.Map<Color>(value);
            var solidColorBrush = new SolidColorBrush(color);
            return solidColorBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            var solidColorBrush = (SolidColorBrush) value;
            var colorFromBrush = solidColorBrush.Color;
            var color = Mapper.Map<Color>(colorFromBrush);
            return color;            
        }
    }
}