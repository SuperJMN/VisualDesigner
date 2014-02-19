using PostSharp.Patterns.Model;

namespace SampleModel
{
    public class Shape : CanvasItemViewModel
    {
        [Reference]
        public Color FillColor { get; set; }
    }
}
