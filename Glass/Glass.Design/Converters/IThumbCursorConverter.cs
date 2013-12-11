using System.Windows;
using System.Windows.Input;

namespace Glass.Design.Converters
{
    public interface IThumbCursorConverter
    {
        Cursor GetCursor(Rect handleRect, Rect parentRect);
    }
}