using System.Collections.ObjectModel;
using System.Xml;
using System.Xml.Schema;
using Glass.Design.Pcl.CanvasItem.NotifyPropertyChanged;

namespace SampleModel
{
    public class Shape : CanvasItemINPC
    {
        public Color FillColor { get; set; }
    }
}
