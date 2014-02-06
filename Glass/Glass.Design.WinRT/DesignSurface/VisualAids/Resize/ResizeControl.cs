using System.Collections.Generic;
using System.Linq;
using Windows.Foundation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Glass.Design.Pcl;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using ImpromptuInterface;

namespace Glass.Design.WinRT.DesignSurface.VisualAids.Resize
{
    public sealed class ResizeControl : Control
    {
        public IEdgeSnappingEngine SnappingEngine { get; set; }



        public ResizeControl(CanvasItem itemToResize, UIElement parent, IEdgeSnappingEngine snappingEngine)
        {
            DefaultStyleKey = typeof(ResizeControl);
            SnappingEngine = snappingEngine;
            FrameOfReference = parent;
            CanvasItem = itemToResize;
        }

        //private WindowsSizeCursorsThumbCursorConverter WindowsSizeCursorsThumbCursorConverter { get; set; }
        private WpfUIResizeOperationHandleConnector WpfUIResizeOperationHandleConnector { get; set; }

        #region CreateHostingItem

        public static readonly DependencyProperty CanvasItemProperty =
            DependencyProperty.Register("CreateHostingItem", typeof(ICanvasItem), typeof(ResizeControl),
                new PropertyMetadata(null, OnCanvasItemChanged));

        public ICanvasItem CanvasItem
        {
            get { return (ICanvasItem)GetValue(CanvasItemProperty); }
            set { SetValue(CanvasItemProperty, value); }
        }

        private static void OnCanvasItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (ResizeControl)d;
            var oldCanvasItem = (ICanvasItem)e.OldValue;
            var newCanvasItem = target.CanvasItem;
            target.OnCanvasItemChanged(oldCanvasItem, newCanvasItem);
        }

        private void OnCanvasItemChanged(ICanvasItem oldCanvasItem, ICanvasItem newCanvasItem)
        {
            //if (IsLoaded)
            //{
            //    RegisterHandles();
            //}
            //else
            //{
            Loaded += OnLoaded;
            //}
        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            RegisterHandles();
            Loaded -= OnLoaded;
        }

        private void RegisterHandles()
        {
            WpfUIResizeOperationHandleConnector = new WpfUIResizeOperationHandleConnector(CanvasItem, FrameOfReference, SnappingEngine);
            //WindowsSizeCursorsThumbCursorConverter = new WindowsSizeCursorsThumbCursorConverter();


            var thumbContainer = (UIElement)FindName("PART_ThumbContainer");

            var visualChildren = new List<DependencyObject>();
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(thumbContainer); i++)
            {
                visualChildren.Add(VisualTreeHelper.GetChild(thumbContainer, i));
            }

            var logicalChildren = visualChildren.OfType<FrameworkElement>();
            foreach (var logicalChild in logicalChildren)
            {


                var childRect = this.GetRectRelativeToParent(logicalChild).ActLike<IRect>();

                var parentRect = CanvasItem.Rect().ActLike<IRect>();

                var handlePoint = childRect.GetHandlePoint(parentRect.Size);

                WpfUIResizeOperationHandleConnector.RegisterHandle(logicalChild, handlePoint);
                SetCursorToHandle(logicalChild);
            }
        }

        private void SetCursorToHandle(FrameworkElement handle)
        {
            var handleRect = this.GetRectRelativeToParent(handle).ActLike<IRect>();
            var parentRect = ServiceLocator.CoreTypesFactory.CreateRect(0, 0, ActualWidth, ActualHeight);
            //handle.Cursor = WindowsSizeCursorsThumbCursorConverter.GetCursor(handleRect, parentRect);
        }

        #endregion

        #region FrameOfReference

        public static readonly DependencyProperty FrameOfReferenceProperty =
            DependencyProperty.Register("FrameOfReference", typeof(UIElement), typeof(ResizeControl),
                new PropertyMetadata(null, OnFrameOfReferenceChanged));

        public UIElement FrameOfReference
        {
            get { return (UIElement)GetValue(FrameOfReferenceProperty); }
            set { SetValue(FrameOfReferenceProperty, value); }
        }

        private static void OnFrameOfReferenceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (ResizeControl)d;
            var oldFrameOfReference = (UIElement)e.OldValue;
            var newFrameOfReference = target.FrameOfReference;
            target.OnFrameOfReferenceChanged(oldFrameOfReference, newFrameOfReference);
        }

        protected void OnFrameOfReferenceChanged(UIElement oldFrameOfReference, UIElement newFrameOfReference)
        {
            //if (IsLoaded)
            //{
            //    RegisterHandles();
            //}
            //else
            //{
                Loaded += OnLoaded;
            //}
        }

        #endregion
    }



    static public class VisualExtensions
    {
        public static Rect GetRectRelativeToParent(this UIElement parent, UIElement child)
        {
            var transform = parent.TransformToVisual(child);
            var point = transform.TransformPoint(new Point());
            return new Rect(point, child.RenderSize);
        }


        /// <summary>

    }

}
