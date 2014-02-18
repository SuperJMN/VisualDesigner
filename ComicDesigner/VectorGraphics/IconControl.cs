using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;

namespace ComicDesigner.VectorGraphics
{
    /// <summary>
    /// Displays a Path.
    /// </summary>
    public sealed class IconControl : Control
    {
        public static readonly DependencyProperty DataGeometryProperty =
            DependencyProperty.Register("DataGeometry", typeof(PathGeometry), typeof(IconControl), new PropertyMetadata(null));

        public static readonly DependencyProperty DataProperty =
            DependencyProperty.Register("Data", typeof(String), typeof(IconControl), new PropertyMetadata(null, new PropertyChangedCallback(OnDataChanged)));

        public IconControl()
        {
            this.DefaultStyleKey = typeof(IconControl);
        }

        // Write-only to be used in a binding.
        public String Data
        {
            private get { return (String)GetValue(DataProperty); }
            set { SetValue(DataProperty, value); }
        }

        // Read-only to be used in the control's template.
        public PathGeometry DataGeometry
        {
            get { return (PathGeometry)GetValue(DataGeometryProperty); }
            private set { SetValue(DataGeometryProperty, value); }
        }

        private static void OnDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            IconControl ic = d as IconControl;            
            ic.DataGeometry = new StringToPathGeometryConverter().Convert(e.NewValue.ToString());
        }
    }
}