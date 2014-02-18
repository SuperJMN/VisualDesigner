using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using Glass.Design.Pcl.PlatformAbstraction;
using Glass.Design.WinRT.DesignSurface.VisualAids.Resize;
using Glass.Design.WinRT.DesignSurface.VisualAids.Snapping;

namespace Glass.Design.WinRT.PlatformSpecific
{
    public class WinRTUIElementFactory : IUIElementFactory
    {
        public IControl CreateResizeControl(CanvasItem itemToResize, IUserInputReceiver parent, IEdgeSnappingEngine snappingEngine)
        {            
            return new ControlAdapter(new ResizeControl
                   {
                       CanvasItem = itemToResize, FrameOfReference = parent, SnappingEngine = snappingEngine
                   });
        }

        public IControl CreateMovingControl()
        {            
            return new ControlAdapter(new DesignSurface.VisualAids.Drag.MovingControl());
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
           return new DesignSurface.VisualAids.Selection.SelectionAdorner(designSurface, canvasItem);
        }
    }    
}