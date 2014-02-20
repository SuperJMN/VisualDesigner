namespace Model
{
    public class Color
    {
        private readonly byte r;
        private readonly byte g;
        private readonly byte b;
        private readonly byte a;

        public Color(byte alpha, byte r, byte g, byte b)
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
            get { return r; }            
        }

        public byte G
        {
            get { return g; }
            
        }

        public byte B
        {
            get { return b; }            
        }

        public byte A
        {
            get { return a; }            
        }
    }
}