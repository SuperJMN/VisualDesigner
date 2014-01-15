namespace SampleModel
{
    public class Color
    {
        public Color(byte alpha, byte r, byte g, byte b)
        {
            A = alpha;
            R = r;
            G = g;
            B = b;
        }

        public Color(byte r, byte g, byte b) : this(255, r, g, b)
        {
            
        }

        public byte R { get; set; }
        public byte G { get; set; }
        public byte B { get; set; }
        public byte A { get; set; }
    }
}