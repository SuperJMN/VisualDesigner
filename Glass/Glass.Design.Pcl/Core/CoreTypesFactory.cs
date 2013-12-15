using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Glass.Design.Pcl.Core
{
    public abstract class CoreTypesFactory
    {
        public abstract IPoint CreatePoint(double x, double y);

        // ReSharper disable once TooManyArguments
        public abstract IRect CreateRect(double left, double top, double width, double height);
    }
}
