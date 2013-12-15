namespace Glass.Design.Pcl.CanvasItem
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
            get { return positionDelta; }
        }

        public double SizeDelta
        {
            get { return sizeDelta; }
        }
    }
}