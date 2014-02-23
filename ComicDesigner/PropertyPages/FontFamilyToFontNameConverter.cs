using System;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace ComicDesigner.PropertyPages
{
    public class FontFamilyToFontNameConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var fontName = value as string;
            if (fontName != null)
            {
                return new FontFamily(fontName);
            }

            return new FontFamily("Arial");
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {


            var fontFamily = value as FontFamily;
            if (fontFamily != null)
            {
                return fontFamily.Source;
            }

            return string.Empty;
        }
    }
}