using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Media;

namespace Glass.Basics.Wpf.Extensions
{
    static public class VisualExtensions
    {
        public static Rect GetRectRelativeToParent(this Visual child)
        {
            var rect = VisualTreeHelper.GetDescendantBounds(child);
            var offset = VisualTreeHelper.GetOffset(child);
            var rectRelativeToParent = Rect.Offset(rect, offset);
            return rectRelativeToParent;
        }


        /// <summary>
        /// ¡Esto es un jodido hack! Pero funciona...
        /// </summary>
        /// <param name="visual"></param>
        /// <returns></returns>
        public static Panel FindItemsPanel(this Visual visual)
        {
            return (Panel)typeof(MultiSelector).InvokeMember("ItemsHost",
                BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.Instance,
                null, visual, null);    
        }      
    }
}