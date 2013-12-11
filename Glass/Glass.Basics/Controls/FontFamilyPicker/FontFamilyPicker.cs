using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Glass.Basics.Controls.FontFamilyPicker
{
    public class FontFamilyPicker : Control
    {
        static FontFamilyPicker()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(FontFamilyPicker), new FrameworkPropertyMetadata(typeof(FontFamilyPicker)));
        }

        #region FontFamilies

        public static readonly DependencyProperty FontFamiliesProperty =
            DependencyProperty.Register("FontFamilies", typeof(ICollection<FontFamily>), typeof(FontFamilyPicker),
                new FrameworkPropertyMetadata(Fonts.SystemFontFamilies));

        public ICollection<FontFamily> FontFamilies
        {
            get { return (ICollection<FontFamily>) GetValue(FontFamiliesProperty); }
            set { SetValue(FontFamiliesProperty, value); }
        }

        #endregion

        #region SelectedFontFamily

        public static readonly DependencyProperty SelectedFontFamilyProperty =
            DependencyProperty.Register("SelectedFontFamily", typeof(FontFamily), typeof(FontFamilyPicker),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));


        public FontFamily SelectedFontFamily
        {
            get { return (FontFamily)GetValue(SelectedFontFamilyProperty); }
            set { SetValue(SelectedFontFamilyProperty, value); }
        }

        #endregion



    }
}
