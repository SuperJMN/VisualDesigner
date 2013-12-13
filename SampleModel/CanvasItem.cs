using System;
using System.Collections.ObjectModel;
using Design.Interfaces;

namespace SampleModel
{
    public class CanvasItem : ICanvasItem
    {
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public event EventHandler<LocationChangedEventArgs> LeftChanged;

        public event EventHandler<LocationChangedEventArgs> TopChanged;

        public event EventHandler<SizeChangeEventArgs> HeightChanged;
        public event EventHandler<SizeChangeEventArgs> WidthChanged;
        public ObservableCollection<ICanvasItem> Children { get; private set; }
    }
}