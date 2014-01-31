using System;
using System.Collections;
using System.Windows;
using Glass.Design.Pcl.DesignSurface;
using SampleModel;
using Xceed.Wpf.Toolkit.PropertyGrid;

namespace Glass.Design.WpfTester
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            this.PropertyGrid.SelectedObjectChanged += PropertyGridOnSelectedObjectChanged;
        }

        private void PropertyGridOnSelectedObjectChanged(object sender, RoutedPropertyChangedEventArgs<object> routedPropertyChangedEventArgs)
        {
            // This is a hack to palliate the limitation that PCL does not have the System.ComponentModel custom attributes.

            foreach (PropertyItem property in new ArrayList( this.PropertyGrid.Properties ))
            {
                switch (property.PropertyDescriptor.Name)
                {
                    case "Left":
                    case "Right":
                    case "Top":
                    case "Width":
                    case "Height":
                    case "Bottom":
                        property.Category = "Positioning";
                        break;

                    case "Children":
                    case "ChildrenPositioning":
                    case "Parent":
                        property.Visibility = Visibility.Collapsed;
                        break;
                }
            }
        }
    }
}
