using System;

namespace Glass.Design.DesignSurface.VisualAids.Resize
{
    public class AspectRatioKeeper : IDisposable
    {
        
        public AspectRatioKeeper(ISizable sizable)
        {
            Sizable = sizable;
            Sizable.WidthChanged += SizableOnWidthChanged;
            Sizable.HeightChanged += SizableOnHeightChanged;
            Aspect = Sizable.Width / Sizable.Height;
        }

        public ISizable Sizable { get; private set; }

        private void SizableOnHeightChanged(object sender, EventArgs eventArgs)
        {
            var newHeight = Sizable.Height;
            var newWidth = newHeight / Aspect;
            Sizable.Width = newWidth;
        }

        private void SizableOnWidthChanged(object sender, EventArgs eventArgs)
        {
            var newWidth = Sizable.Width;
            var newHeight = newWidth / Aspect;
            Sizable.Height = newHeight;
        }

        public double Aspect { get; private set; }
        public void Dispose()
        {
            Sizable.WidthChanged -= SizableOnWidthChanged;
            Sizable.HeightChanged -= SizableOnHeightChanged;
        }
    }
}