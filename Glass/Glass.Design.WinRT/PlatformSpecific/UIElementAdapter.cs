using System;
using System.ComponentModel;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Input;
using AutoMapper;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.WinRT.PlatformSpecific
{

    internal class UIElementAdapter : IUIElement
    {
        private double left;
        private double top;
        private double width;
        private double height;
        private CanvasItemCollection children;
        private double right;
        private double bottom;
        private ICanvasItemContainer parent;
        private UIElement uiElement;

        private UIElement UIElement
        {
            get { return uiElement; }
            set
            {
                if (uiElement != null)
                {
                    uiElement.PointerPressed -= UIElementOnPointerPressed;
                    uiElement.PointerMoved -= UIElementOnPointerMoved;
                    uiElement.PointerReleased -= UIElementOnPointerReleased;
                }

                uiElement = value;

                if (uiElement != null)
                {
                    uiElement.PointerPressed += UIElementOnPointerPressed;
                    uiElement.PointerMoved += UIElementOnPointerMoved;
                    uiElement.PointerReleased += UIElementOnPointerReleased;
                }

            }
        }

        private void UIElementOnPointerPressed(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            var corePoint = pointerRoutedEventArgs.GetCurrentPoint(null);

            var fingerManipulationEventArgs = new FingerManipulationEventArgs();

            OnFingerDown(fingerManipulationEventArgs);

            pointerRoutedEventArgs.Handled = fingerManipulationEventArgs.Handled;
        }

        private void UIElementOnPointerReleased(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            var corePoint = pointerRoutedEventArgs.GetCurrentPoint(null);
            var point = Mapper.Map<Point>(corePoint);


            var fingerManipulationEventArgs = new FingerManipulationEventArgs();
            OnFingerUp(fingerManipulationEventArgs);

            pointerRoutedEventArgs.Handled = fingerManipulationEventArgs.Handled;
        }

        private void UIElementOnPointerMoved(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            var corePoint = pointerRoutedEventArgs.GetCurrentPoint(null);
            var point = Mapper.Map<Point>(corePoint);


            var fingerManipulationEventArgs = new FingerManipulationEventArgs();
            OnFingerMove(fingerManipulationEventArgs);
            pointerRoutedEventArgs.Handled = fingerManipulationEventArgs.Handled;
        }

        public UIElementAdapter(UIElement uiElement)
        {
            this.UIElement = uiElement;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public double GetCoordinate(CoordinatePart part)
        {
            throw new NotImplementedException();
        }

        public void SetCoordinate(CoordinatePart part, double value)
        {
            throw new NotImplementedException();
        }

        public double Left
        {
            get { return left; }
            set { left = value; }
        }

        public double Top
        {
            get { return top; }
            set { top = value; }
        }

        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        public double Height
        {
            get { return height; }
            set { height = value; }
        }

        public CanvasItemCollection Children
        {
            get { return children; }
            set { children = value; }
        }

        public double Right
        {
            get { return right; }
            set { right = value; }
        }

        public double Bottom
        {
            get { return bottom; }
            set { bottom = value; }
        }

        public ICanvasItemContainer Parent
        {
            get { return parent; }
            set { parent = value; }
        }

        public void AddAdorner(IAdorner adorner)
        {
            //var adornerLayer = AdornerLayer.GetAdornerLayer(UIElement);
            //adornerLayer.Add((Adorner)adorner);
        }

        public void RemoveAdorner(IAdorner adorner)
        {
            //var adornerLayer = AdornerLayer.GetAdornerLayer(UIElement);

            //var adorners = adornerLayer.GetAdorners(UIElement);
        }

        public bool IsVisible { get; set; }
        public bool IsHitTestVisible { get; set; }
        public object GetCoreInstance()
        {
            return UIElement;
        }

        public event FingerManipulationEventHandler FingerDown;

        protected virtual void OnFingerDown(FingerManipulationEventArgs args)
        {
            var handler = FingerDown;
            if (handler != null) handler(this, args);
        }

        public event FingerManipulationEventHandler FingerMove;

        protected virtual void OnFingerMove(FingerManipulationEventArgs args)
        {
            var handler = FingerMove;
            if (handler != null) handler(this, args);
        }

        public event FingerManipulationEventHandler FingerUp;

        protected virtual void OnFingerUp(FingerManipulationEventArgs args)
        {
            var handler = FingerUp;
            if (handler != null) handler(this, args);
        }

        public void CaptureInput()
        {
            //UIElement.CapturePointer(new Pointer())
        }

        public void ReleaseInput()
        {
            //UIElement.ReleasePointerCaptures();
        }
    }
}
