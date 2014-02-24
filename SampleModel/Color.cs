namespace SampleModel
{

    public struct Color
    {
        public Color(byte alpha, byte r, byte g, byte b) : this()
        {
            A = alpha;
            R = r;
            G = g;
            B = b;
        }

        public Color(byte r, byte g, byte b) : this(255, r, g, b)
        {
            
        }

        public byte R { get; private set; }
        public byte G { get; private set; }
        public byte B { get; private set; }
        public byte A { get; private set; }
    }
}