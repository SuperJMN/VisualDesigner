using System;
using Windows.UI.Xaml;
using Glass.Design.Pcl.Canvas;

namespace ComicDesigner
{
    public sealed partial class PropertiesControl
    {
        public PropertiesControl()
        {
            this.InitializeComponent();
        }

        #region SelectedItem
        public static readonly DependencyProperty SelectedItemProperty =
          DependencyProperty.Register("SelectedItem", typeof(object), typeof(PropertiesControl),
            new PropertyMetadata(null, PropertyChangedCallback));

        private static void PropertyChangedCallback(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs dependencyPropertyChangedEventArgs)
        {
            
        }

        public bool SelectedItem
        {
            get { return (bool)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        #endregion        
    }
}
