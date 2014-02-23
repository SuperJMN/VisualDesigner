using PostSharp.Patterns.Model;

namespace Model
{
    public class Bubble : Shape
    {
        public string Text { get; set; }

        [Reference]
        public Color TextColor { get; set; }

        public double FontSize { get; set; }
        public string FontName { get; set; }
    }
}