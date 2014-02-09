using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Glass.Design.Pcl;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Resize;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using Glass.Design.Pcl.PlatformAbstraction;
using Glass.Design.WinRT.PlatformSpecific;
using FoundationPoint = Windows.Foundation.Point;

namespace Glass.Design.WinRT.DesignSurface.VisualAids.Resize
{
    public sealed class ResizeControl : Control
    {
        public IEdgeSnappingEngine SnappingEngine { get; set; }

        static ResizeControl()
        {

        }

        public ResizeControl(ICanvasItem itemToResize, IUserInputReceiver parent, IEdgeSnappingEngine snappingEngine)
        {
            SnappingEngine = snappingEngine;
            FrameOfReference = parent;
            CanvasItem = itemToResize;
            DefaultStyleKey = typeof (ResizeControl);
        }


        private UIResizeOperationHandleConnector UIResizeOperationHandleConnector { get; set; }

        #region CreateHostingItem

        public static readonly DependencyProperty CanvasItemProperty =
            DependencyProperty.Register("CreateHostingItem", typeof (ICanvasItem), typeof (ResizeControl),
                new PropertyMetadata(null, OnCanvasItemChanged));

        public ICanvasItem CanvasItem
        {
            get { return (ICanvasItem) GetValue(CanvasItemProperty); }
            set { SetValue(CanvasItemProperty, value); }
        }

        private static void OnCanvasItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (ResizeControl) d;
            var oldCanvasItem = (ICanvasItem) e.OldValue;
            var newCanvasItem = target.CanvasItem;
            target.OnCanvasItemChanged(oldCanvasItem, newCanvasItem);
        }

        private void OnCanvasItemChanged(ICanvasItem oldCanvasItem, ICanvasItem newCanvasItem)
        {
            //if (IsLoaded)
            //{
            RegisterHandles();
            //}
            //else
            //{
            //    Loaded += OnLoaded;
            //}
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            RegisterHandles();
            Loaded -= OnLoaded;
        }

        private void RegisterHandles()
        {
            UIResizeOperationHandleConnector = new UIResizeOperationHandleConnector(CanvasItem, FrameOfReference,
                SnappingEngine);


            var thumbContainer = (UIElement) FindName("PART_ThumbContainer");

            var visualChildren = new List<DependencyObject>();
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(thumbContainer); i++)
            {
                visualChildren.Add(VisualTreeHelper.GetChild(thumbContainer, i));
            }

            var logicalChildren = visualChildren.OfType<FrameworkElement>();
            foreach (var logicalChild in logicalChildren)
            {


                var childRect = this.GetRectRelativeToParent(logicalChild);

                var parentRect = CanvasItem.Rect();

                var handlePoint = childRect.GetHandlePoint(parentRect.Size);

                UIResizeOperationHandleConnector.RegisterHandle(new UIElementAdapter(logicalChild), handlePoint);
                //SetCursorToHandle(logicalChild);
            }
        }

        //private void SetCursorToHandle(FrameworkElement handle)
        //{
        //    var handleRect = Mapper.Map<Rect>(handle.GetRectRelativeToParent());
        //    var parentRect = new Rect(0, 0, ActualWidth, ActualHeight);
        //    handle.Cursor = WindowsSizeCursorsThumbCursorConverter.GetCursor(handleRect, parentRect);
        //}

        #endregion

        #region FrameOfReference

        public static readonly DependencyProperty FrameOfReferenceProperty =
            DependencyProperty.Register("FrameOfReference", typeof (IUserInputReceiver), typeof (ResizeControl),
                new PropertyMetadata(null, OnFrameOfReferenceChanged));

        public IUserInputReceiver FrameOfReference
        {
            get { return (IUserInputReceiver) GetValue(FrameOfReferenceProperty); }
            set { SetValue(FrameOfReferenceProperty, value); }
        }

        private static void OnFrameOfReferenceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (ResizeControl) d;
            var oldFrameOfReference = (IUserInputReceiver) e.OldValue;
            var newFrameOfReference = target.FrameOfReference;
            target.OnFrameOfReferenceChanged(oldFrameOfReference, newFrameOfReference);
        }

        protected void OnFrameOfReferenceChanged(IUserInputReceiver oldFrameOfReference,
            IUserInputReceiver newFrameOfReference)
        {
            //if (IsLoaded)
            //{
            RegisterHandles();
            //}
            //else
            //{
            //    Loaded += OnLoaded;
            //}
        }

        #endregion

        public event FingerManipulationEventHandler FingerDown;
        public event FingerManipulationEventHandler FingerMove;
        public event FingerManipulationEventHandler FingerUp;

        public void CaptureInput()
        {
            throw new System.NotImplementedException();
        }

        public void ReleaseInput()
        {
            throw new System.NotImplementedException();
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
        public CanvasItemCollection Children { get; set; }
        public double Right { get; set; }
        public double Bottom { get; set; }
        public ICanvasItemContainer Parent { get; set; }

        public void AddAdorner(IAdorner adorner)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveAdorner(IAdorner adorner)
        {
            throw new System.NotImplementedException();
        }

        public bool IsVisible { get; set; }

        public object GetCoreInstance()
        {
            return this;
        }
    }
}
