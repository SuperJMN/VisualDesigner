using System.Runtime.Remoting.Channels;
using System.Windows;
using Glass.Design.Pcl.Core;
using ImpromptuInterface;

namespace Glass.Design.Wpf.Core
{
    public class CoreTypesFactoryWpf : ICoreTypesFactory
    {
        public IPoint CreatePoint(double x, double y)
        {
            return new Point(x, y).ActLike<IPoint>();
        }

        public IRect CreateRect(double left, double top, double width, double height)
        {
            return new Rect(left, top, width, height).ActLike<IRect>();
        }

        public ISize CreateSize(double width, double height)
        {
            return new Size(width, height).ActLike<ISize>();
        }

        public IVector CreateVector(double x, double y)
        {
            return new Vector(x, y).ActLike<IVector>();
        }
    }
}