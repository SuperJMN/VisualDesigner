using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.DesignSurface;

namespace Glass.Design.Pcl.PlatformAbstraction
{
    public interface IUIElement : IUserInputReceiver, IPositionable, ICanvasItemContainer
    {
        void AddAdorner(IAdorner adorner);
        void RemoveAdorner(IAdorner adorner);
        bool IsVisible { get; set; }
        bool IsHitTestVisible { get; set; }
        object GetCoreInstance();
    }
}