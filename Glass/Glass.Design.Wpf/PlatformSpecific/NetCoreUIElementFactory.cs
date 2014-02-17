using System.Windows.Controls;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using Glass.Design.Pcl.PlatformAbstraction;
using Glass.Design.Wpf.DesignSurface.VisualAids.Drag;
using Glass.Design.Wpf.DesignSurface.VisualAids.Resize;
using Glass.Design.Wpf.DesignSurface.VisualAids.Selection;
using Glass.Design.Wpf.DesignSurface.VisualAids.Snapping;

namespace Glass.Design.Wpf.PlatformSpecific
{
    public class NetCoreUIElementFactory : IUIElementFactory
    {
        public IControl CreateResizeControl(CanvasItem itemToResize, IUserInputReceiver parent, IEdgeSnappingEngine snappingEngine)
        {
            return new ControlAdapter(new ResizeControl(itemToResize, parent, snappingEngine));
        }

        public IControl CreateMovingControl()
        {
            return new ControlAdapter(new MovingControl());
        }

        public IAdorner CreateWrappingAdorner(IUIElement adornerElement, IControl chrome, ICanvasItem canvasItem)
        {
            return new WrappingAdorner(adornerElement, chrome, canvasItem);
        }

        public IAdorner CreateEdgeAdorner(IUIElement adornedElement, ICanvasItem item, Edge edge)
        {
            return new EdgeAdorner(adornedElement, item, edge);
        }

        public IAdorner CreateSelectionAdorner(IDesignSurface designSurface, ICanvasItem canvasItem)
        {
            return new SelectionAdorner(designSurface, canvasItem) { IsHitTestVisible = false };
        }
    }
}