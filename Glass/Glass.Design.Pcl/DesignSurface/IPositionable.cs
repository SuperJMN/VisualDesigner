using System.ComponentModel;
using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface
{
    public interface IPositionable : INotifyPropertyChanged, ICoordinate
    {
        double Left { get; set; }
        double Top { get; set; }
  
    }
}