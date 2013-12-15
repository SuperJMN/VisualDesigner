using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.Converters
{
    public interface IThumbCursorConverter
    {
        ICursor GetCursor(IRect handleRect, IRect parentRect);
    }
}