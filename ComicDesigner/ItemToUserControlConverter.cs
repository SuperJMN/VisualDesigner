using System;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using ComicDesigner.PropertyPages;
using Model;

namespace ComicDesigner
{
    public class ItemToUserControlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is Bubble)
            {
                return new SpeechBubbleControl();
            }

            return new ContentControl();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotSupportedException();
        }
    }
}