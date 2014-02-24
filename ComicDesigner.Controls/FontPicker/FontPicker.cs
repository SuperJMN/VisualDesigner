using System;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Templated Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234235
using Windows.UI.Xaml.Media;

namespace ComicDesigner.Controls.FontPicker
{
    public sealed class FontPicker : Control
    {
        public static readonly DependencyProperty FontFamiliesProperty = DependencyProperty.Register("FontFamilies", typeof (IEnumerable<string>), typeof (FontPicker), new PropertyMetadata(default(IEnumerable<string>)));

        public FontPicker()
        {
            this.DefaultStyleKey = typeof(FontPicker);

            FontFamilies = new List<string>
                           {
                               "Arial",
                               "Times New Roman",
                               "Courier",
                               "Tahoma",
                               "Verdana",
                               "Comic Sans MS",
                           };
        }

        public IEnumerable<string> FontFamilies
        {
            get { return (IEnumerable<string>) GetValue(FontFamiliesProperty); }
            set { SetValue(FontFamiliesProperty, value); }
        }

        #region SelectedFont
        public static readonly DependencyProperty SelectedFontProperty =
          DependencyProperty.Register("SelectedFont", typeof(string), typeof(FontPicker),
            new PropertyMetadata(null, PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            
        }

        public string SelectedFont
        {
            get { return (string)GetValue(SelectedFontProperty); }
            set { SetValue(SelectedFontProperty, value); }
        }

        #endregion
    }


}
