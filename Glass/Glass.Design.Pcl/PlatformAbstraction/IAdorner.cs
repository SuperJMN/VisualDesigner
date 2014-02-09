namespace Glass.Design.Pcl.PlatformAbstraction
{
    public interface IAdorner: IUIElement
    {
        IUIElement AdornedElement { get; set; }
    }
}