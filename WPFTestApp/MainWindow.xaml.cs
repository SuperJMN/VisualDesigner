using System.Collections;
using System.Windows;
using System.Windows.Input;
using PostSharp.Patterns.Recording;
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
            this.DesignSurface.CanvasDocument = ((MainWindowViewModel) this.DataContext).Document;
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

        
        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Operation operation = (Operation) ((FrameworkElement) e.Source).DataContext;
            // TODO: There should be a better way to get the recorder.
            ((MainWindowViewModel) this.DataContext).Recorder.UndoTo(operation);
        }
    }
}
