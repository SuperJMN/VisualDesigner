using System.ComponentModel;
using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.DesignSurface
{
    public interface ISizable : INotifyPropertyChanged, ICoordinate
    {
        double Width { get; set; }
        double Height { get; set; }
     
    }

}