using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235
using Windows.UI.Xaml.Media;

namespace ComicDesigner.Controls.FontPicker
{
    public sealed class FontPicker : Control
    {
        public static readonly DependencyProperty FontFamiliesProperty = DependencyProperty.Register("FontFamilies", typeof (IEnumerable<FontFamily>), typeof (FontPicker), new PropertyMetadata(default(IEnumerable<FontFamily>)));

        public FontPicker()
        {
            this.DefaultStyleKey = typeof(FontPicker);

            FontFamilies = new List<FontFamily>
                           {
                               new FontFamily("Arial"),
                               new FontFamily("Times New Roman"),
                               new FontFamily("Courier"),
                               new FontFamily("Tahoma"),
                               new FontFamily("Verdana"),
                               new FontFamily("Comic Sans MS"),
                           };
        }

        public IEnumerable<FontFamily> FontFamilies
        {
            get { return (IEnumerable<FontFamily>) GetValue(FontFamiliesProperty); }
            set { SetValue(FontFamiliesProperty, value); }
        }

        #region SelectedFont
        public static readonly DependencyProperty SelectedFontProperty =
          DependencyProperty.Register("SelectedFont", typeof(FontFamily), typeof(FontPicker),
            new PropertyMetadata(null));

        public FontFamily SelectedFont
        {
            get { return (FontFamily)GetValue(SelectedFontProperty); }
            set { SetValue(SelectedFontProperty, value); }
        }

        #endregion
    }


}
