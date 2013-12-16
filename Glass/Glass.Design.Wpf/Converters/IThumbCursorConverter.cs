using System.Windows.Input;
using Glass.Design.Pcl.Core;

namespace Glass.Design.Wpf.Converters
{
    public interface IThumbCursorConverter
    {
        Cursor GetCursor(IRect handleRect, IRect parentRect);
    }
}