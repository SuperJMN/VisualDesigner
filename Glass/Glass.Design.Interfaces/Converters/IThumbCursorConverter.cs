using Glass.Design.Pcl.Core;

namespace Glass.Design.Pcl.Converters
{
    public interface IThumbCursorConverter
    {
        Cursor GetCursor(Rect handleRect, Rect parentRect);
    }
}