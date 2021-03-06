using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using AutoMapper;

namespace ComicDesigner.PropertyPages
{
    public class ModelColorToSolidColorBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (value != null)
            {

                var modelColor = (Model.Color)value;
                var color = Color.FromArgb(modelColor.A, modelColor.R, modelColor.G, modelColor.B);
                var solidColorBrush = new SolidColorBrush(color);
                return solidColorBrush;
            }
            return new SolidColorBrush();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            var solidColorBrush = (SolidColorBrush)value;
            var colorFromBrush = solidColorBrush.Color;
            var color = new Model.Color(colorFromBrush.A, colorFromBrush.R, colorFromBrush.G, colorFromBrush.B);
            return color;
        }
    }
}