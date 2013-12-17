using System.Windows;
using Glass.Design.Pcl.Core;
using Glass.Design.Wpf.Core;
using ImpromptuInterface;
using Point = System.Windows.Point;

namespace Glass.Design.WpfTester
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            ServiceLocator.CoreTypesFactory = new CoreTypesFactoryWpf();
        }
    }

    
}
