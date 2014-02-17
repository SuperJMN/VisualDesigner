using Glass.Design.Pcl.Annotations;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.DesignSurface;
using Glass.Design.Pcl.DesignSurface.VisualAids.Snapping;
using Glass.Design.Pcl.PlatformAbstraction;

namespace Glass.Design.Pcl.Core
{
    public static class ServiceLocator
    {
        public static ICoreTypesFactory CoreTypesFactory { get; set; }
        public static IInputProvider InputProvider { get; set; }
        public static IUIElementFactory UIElementFactory { get; set; }
    }

    public interface IUIElementFactory
    {
        IControl CreateResizeControl(CanvasItem itemToResize, IUserInputReceiver parent, IEdgeSnappingEngine snappingEngine);
        IControl CreateMovingControl();
        IAdorner CreateWrappingAdorner(IUIElement adornerElement, IControl chrome, ICanvasItem canvasItem);
        IAdorner CreateEdgeAdorner(IUIElement adornedElement, ICanvasItem item, Edge edge);
        IAdorner CreateSelectionAdorner(IDesignSurface designSurface, ICanvasItem canvasItem);
    }

    public interface IInputProvider
    {
        Point GetMousePositionRelativeTo(IUserInputReceiver inputReceiver); 
    }
}