using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Glass.Basics.Converters {
    /// <summary>
    /// This class simply converts a image url into a Image
    /// for use within a MenuItem
    /// </summary>
    [ValueConversion(typeof(String), typeof(Image))]
    public class MenuIconConverter : IValueConverter {
        #region IValueConverter implementation
        /// <summary>
        /// Convert string to Image
        /// </summary>
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture) {
            if (value == null)
                return Binding.DoNothing;

            var imageUrl = value.ToString();

            if (String.IsNullOrEmpty(imageUrl))
                return Binding.DoNothing;

            var img = new Image();
            img.Width = 16;
            img.Height = 16;
            var bmp = new BitmapImage(new Uri(imageUrl,
                UriKind.RelativeOrAbsolute));
            img.Source = bmp;
            return img;
        }

        /// <summary>
        /// Convert back, but its not implemented
        /// </summary>
        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture) {
            throw new NotImplementedException("Not implemented");
        }
        #endregion
    }
}
