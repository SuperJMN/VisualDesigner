using System;
using System.ComponentModel;

namespace Glass.Design.Pcl.DesignSurface.VisualAids.Resize
{
    public class AspectRatioKeeper : IDisposable
    {
        
        public AspectRatioKeeper(ISizable sizable)
        {
            Sizable = sizable;
            Sizable.PropertyChanged += SizableOnPropertyChanged;
            Aspect = Sizable.Width / Sizable.Height;
        }

        private void SizableOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            switch (propertyChangedEventArgs.PropertyName)
            {
                case "Height":
                {
                    var newHeight = Sizable.Height;
                    var newWidth = newHeight/Aspect;
                    Sizable.Width = newWidth;
                    break;
                }

                case "Width":
                {
                    var newWidth = Sizable.Width;
                    var newHeight = newWidth/Aspect;
                    Sizable.Height = newHeight;
                    break;
                }
            }
        }

        public ISizable Sizable { get; private set; }

   
        public double Aspect { get; private set; }
        public void Dispose()
        {
            Sizable.PropertyChanged -= SizableOnPropertyChanged;
        }
    }
}