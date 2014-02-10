namespace Glass.Basics.Wpf
{
    public struct Borders
    {
        private bool left;
        private bool top;
        private bool right;
        private bool bottom;

        public Borders(bool uniformValue)
        {
            left = uniformValue;
            top = uniformValue;
            right = uniformValue;
            bottom = uniformValue;
        }

        public bool Left
        {
            get { return left; }
            set { left = value; }
        }

        public bool Top
        {
            get { return top; }
            set { top = value; }
        }

        public bool Right
        {
            get { return right; }
            set { right = value; }
        }

        public bool Bottom
        {
            get { return bottom; }
            set { bottom = value; }
        }

        public static Borders All
        {
            get
            {
                return new Borders(true);
            }
        }
    }
}
