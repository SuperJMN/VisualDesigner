using System;
using System.ComponentModel;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.WinRT.PlatformSpecific
{
    public class WinRTUIElementFactory : IUIElementFactory
    {
        public IUIElement CreateResizeControl(CanvasItem itemToResize, IUserInputReceiver parent, IEdgeSnappingEngine snappingEngine)
        {
            //return new ResizeControl(itemToResize, parent, snappingEngine);
            throw new NotImplementedException();
        }

        public IUIElement CreateMovingControl()
        {
            //return new MovingControl();
            throw new NotImplementedException();
        }

        public IAdorner CreateWrappingAdorner(IUIElement adornerElement, IUIElement chrome, ICanvasItem canvasItem)
        {
            //return new WrappingAdorner(adornerElement, chrome, canvasItem);
            throw new NotImplementedException();
        }

        public IAdorner CreateEdgeAdorner(IUIElement adornedElement, ICanvasItem item, Edge edge)
        {
            //return new EdgeAdorner(adornedElement, item, edge);
            throw new NotImplementedException();
        }

        public IAdorner CreateSelectionAdorner(IDesignSurface designSurface, ICanvasItem canvasItem)
        {
           return new DesignSurface.VisualAids.Selection.SelectionAdorner(designSurface, canvasItem);
        }
    }    
}