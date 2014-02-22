using System;
using Windows.UI;
using Windows.UI.Xaml.Data;

namespace ComicDesigner.PropertyPages
{
    public class ColorToModelColorConverter : IValueConverter 
    {
        public object Convert(object value, Type targetType, object parameter, string culture)
        {
            if (value != null)
            {
                var modelColor = (Model.Color) value;
                var color = Color.FromArgb(modelColor.A, modelColor.R, modelColor.G, modelColor.B);
                return color;
            }
            else
            {
                return new Color();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, string culture)
        {
            var color = (Color) value;
            var modelColor = new Model.Color(color.A, color.R, color.G, color.B);
            return modelColor;            
        }
    }
}