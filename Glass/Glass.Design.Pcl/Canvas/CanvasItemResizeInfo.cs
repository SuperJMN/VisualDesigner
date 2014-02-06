namespace Glass.Design.Pcl.Canvas
{
    internal struct CanvasItemResizeInfo
    {
        private readonly double positionDelta;
        private readonly double sizeDelta;

        public CanvasItemResizeInfo(double positionDelta, double sizeDelta)
        {
            this.positionDelta = positionDelta;
            this.sizeDelta = sizeDelta;
        }

        public double PositionDelta
        {
            get { return this.positionDelta; }
        }

        public double SizeDelta
        {
            get { return this.sizeDelta; }
        }
    }
}