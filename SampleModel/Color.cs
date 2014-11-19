namespace SampleModel
{

    public struct Color
    {
        private readonly byte r;
        private readonly byte g;
        private readonly byte b;
        private readonly byte a;

        public Color(byte alpha, byte r, byte g, byte b) : this()
        {
            this.a = alpha;
            this.r = r;
            this.g = g;
            this.b = b;
        }

        public Color(byte r, byte g, byte b) : this(255, r, g, b)
        {
            
        }

        public byte R
        {
            get { return this.r; }
        }

        public byte G
        {
            get { return this.g; }
        }

        public byte B
        {
            get { return this.b; }
        }

        public byte A
        {
            get { return this.a; }
        }
    }
}