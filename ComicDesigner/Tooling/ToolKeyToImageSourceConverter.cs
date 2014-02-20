using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media.Imaging;

namespace ComicDesigner.Tooling
{
    public class ToolKeyToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            Debug.Assert(value is string);
            
            var iconKey = (string) value;
            var path = @"Tooling/Tools/Icons/";
            var uriString = string.Format("ms-appx:/{0}{1}.png", path, iconKey);

            var uriSource = new Uri(uriString);
            var bitmapImage = new BitmapImage(uriSource);

            return bitmapImage;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}
