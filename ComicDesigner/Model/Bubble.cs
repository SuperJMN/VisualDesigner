using Glass.Design.Pcl.Canvas;
using PostSharp.Patterns.Model;

namespace Model
{
    public class Bubble : Shape
    {
        public string Text { get; set; }

        [Reference]
        public object TextColor { get; set; }

    }
}