using System;
using Design.Interfaces;

namespace SampleModel
{
    public class CanvasItem : ICanvasItem
    {
        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public event EventHandler LocationChanged;
        
        public event EventHandler HeightChanged;
        public event EventHandler WidthChanged;
    }
}