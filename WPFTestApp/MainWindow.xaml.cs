using System.Windows;
using System.Windows.Input;
using PostSharp.Patterns.Recording;

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
            this.DesignSurface.CanvasDocument = ((MainWindowViewModel) this.DataContext).Document;
        }


        private void Control_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Operation operation = (Operation) ((FrameworkElement) e.Source).DataContext;
            // TODO: There should be a better way to get the recorder.
            ((MainWindowViewModel) this.DataContext).Recorder.UndoTo(operation);
        }
    }
}
