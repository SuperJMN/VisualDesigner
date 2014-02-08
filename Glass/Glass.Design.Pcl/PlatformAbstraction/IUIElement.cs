using Glass.Design.Pcl.Canvas;

namespace Glass.Design.Pcl.PlatformAbstraction
{
    public interface IUIElement : IUserInputReceiver, ICanvasItem
    {
        void AddAdorner(IAdorner adorner);
        bool IsVisible { get; set; }
        bool IsHitTestVisible { get; set; }
        object GetCoreInstance();
    }
}