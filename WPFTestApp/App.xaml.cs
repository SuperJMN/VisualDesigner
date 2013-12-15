using System.Windows;
using Glass.Design.Pcl.Core;
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

    public class CoreTypesFactoryWpf : CoreTypesFactory
    {
        public override IPoint CreatePoint(double x, double y)
        {
            return new Point(x, y).ActLike<IPoint>();
        }

        public override IRect CreateRect(double left, double top, double width, double height)
        {
            return new Rect(left, top, width, height).ActLike<IRect>();
        }
    }
}
