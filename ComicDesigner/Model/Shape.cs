using PostSharp.Patterns.Model;

namespace Model
{
    public class Shape : CanvasItemViewModel
    {
        [Reference]
        public Color Background { get; set; }
        [Reference]
        public Color Stroke { get; set; }
        [Reference]
        public Color StrokeThickness { get; set; }
    }
}
