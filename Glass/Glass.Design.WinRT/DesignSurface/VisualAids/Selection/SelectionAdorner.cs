using System;
using System.ComponentModel;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.PlatformAbstraction;
using IUIElement = Glass.Design.Pcl.PlatformAbstraction.IUIElement;

namespace Glass.Design.WinRT.DesignSurface.VisualAids.Selection
{
    public abstract class CanvasItemAdorner : IUIElement
    {
        private ICanvasItem canvasItem;

        public CanvasItemAdorner(IUIElement adornedElement, ICanvasItem canvasItem)
         //   : base(adornedElement)
        {        
            CanvasItem = canvasItem;
        }

        protected ICanvasItem CanvasItem
        {
            get { return canvasItem; }
            set
            {
                if (canvasItem != null)
                {
                    canvasItem.PropertyChanged -= CanvasItemOnPropertyChanged;
                }
                canvasItem = value;
                if (canvasItem != null)
                {
                    canvasItem.PropertyChanged += CanvasItemOnPropertyChanged;
                }
            }
        }

        private void CanvasItemOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            switch (propertyChangedEventArgs.PropertyName)
            {
                case "Top":
                case "Left":
                case "Width":
                case "Height":
                    InvalidateVisual();
                    break;
            }
        }

        private void InvalidateVisual()
        {
            throw new System.NotImplementedException();
        }

        public event FingerManipulationEventHandler FingerDown;
        public event FingerManipulationEventHandler FingerMove;
        public event FingerManipulationEventHandler FingerUp;
        public void CaptureInput()
        {
            throw new System.NotImplementedException();
        }

        public void ReleaseInput()
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public double GetCoordinate(CoordinatePart part)
        {
            throw new System.NotImplementedException();
        }

        public void SetCoordinate(CoordinatePart part, double value)
        {
            throw new System.NotImplementedException();
        }

        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public CanvasItemCollection Children { get; set; }
        public double Right { get; set; }
        public double Bottom { get; set; }
        public ICanvasItemContainer Parent { get; set; }
        public abstract bool IsHitTestVisible { get; set; }
       
        public object GetCoreInstance()
        {
            throw new NotImplementedException();
        }

        public void AddAdorner(IAdorner adorner)
        {
            throw new System.NotImplementedException();
        }

        public bool IsVisible { get; set; }
    }

    public class Adorner : IUIElement
    {
        public event FingerManipulationEventHandler FingerDown;
        public event FingerManipulationEventHandler FingerMove;
        public event FingerManipulationEventHandler FingerUp;
        public void CaptureInput()
        {
            throw new System.NotImplementedException();
        }

        public void ReleaseInput()
        {
            throw new NotImplementedException();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public double GetCoordinate(CoordinatePart part)
        {
            throw new System.NotImplementedException();
        }

        public void SetCoordinate(CoordinatePart part, double value)
        {
            throw new System.NotImplementedException();
        }

        public double Left { get; set; }
        public double Top { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }
        public CanvasItemCollection Children { get; set; }
        public double Right { get; set; }
        public double Bottom { get; set; }
        public ICanvasItemContainer Parent { get; set; }
        public bool IsVisible { get; set; }
        public bool IsHitTestVisible { get; set; }
        public object GetCoreInstance()
        {
            throw new NotImplementedException();
        }

        public void AddAdorner(IAdorner adorner)
        {
            throw new System.NotImplementedException();
        }
    }

    internal class SelectionAdorner : CanvasItemAdorner
    {
        private static readonly SolidColorBrush FillBrushInstance = new SolidColorBrush(Color.FromArgb(50, 147, 218, 255));
        private static readonly SolidColorBrush PenBrushInstance = new SolidColorBrush(Color.FromArgb(139, 56, 214, 255));
        private static readonly Pen PenInstance = new Pen(PenBrushInstance, 2);
       

 


        private static SolidColorBrush FillBrush
        {
            get { return FillBrushInstance; }
        }

        private static Pen Pen
        {
            get { return PenInstance; }
        }

        public override bool IsHitTestVisible { get; set; }

        public SelectionAdorner(IUIElement adornedElement, ICanvasItem canvasItem)
            : base(adornedElement, canvasItem)
        {        
        }

        //protected override Size MeasureOverride(Size constraint)
        //{
        //    return new Size(CanvasItem.Width, CanvasItem.Height);
        //}

        //protected override void OnRender(DrawingContext drawingContext)
        //{
        //    base.OnRender(drawingContext);

        //    drawingContext.DrawRectangle(FillBrush, Pen, new Rect(CanvasItem.Left, CanvasItem.Top, CanvasItem.Width, CanvasItem.Height));
        //}
    }

    internal class Pen
    {
        public Pen(SolidColorBrush penBrushInstance, int i)
        {
            throw new System.NotImplementedException();
        }
    }
}