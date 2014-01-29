using System.ComponentModel;
using Glass.Design.Pcl.CanvasItem;

namespace SampleModel
{
    public class Label : CanvasItem, INotifyPropertyChanged
    {
        public string Text { get; set; }
    }
}