using System;
using Windows.UI.Popups;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using Glass.Design.Pcl.PlatformAbstraction;
using Glass.Design.WinRT.DesignSurface.VisualAids.Snapping;

namespace Glass.Design.WinRT.PlatformSpecific
{
    public class WinRTUIElementFactory : IUIElementFactory
    {
        public IUIElement CreateResizeControl(CanvasItem itemToResize, IUserInputReceiver parent, IEdgeSnappingEngine snappingEngine)
        {            
            return new ResizeControl
                   {
                       CanvasItem = itemToResize, FrameOfReference = parent, SnappingEngine = snappingEngine
                   };
        }

        public IUIElement CreateMovingControl()
        {            
            return new DesignSurface.VisualAids.Drag.MovingControl();
        }

        public IAdorner CreateWrappingAdorner(IUIElement adornerElement, IUIElement chrome, ICanvasItem canvasItem)
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