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
            if (!string.IsNullOrEmpty( fontName ))
            {
                return new FontFamily(fontName);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {


            var fontFamily = value as FontFamily;
            if (fontFamily != null)
            {
                return fontFamily.Source;
            }

            return null;
        }
    }
}