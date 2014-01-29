using System.ComponentModel;
using Glass.Design.Pcl.CanvasItem;

namespace SampleModel
{
    public class Shape : CanvasItem, INotifyPropertyChanged
    {
        public Color FillColor { get; set; }
    }
}
