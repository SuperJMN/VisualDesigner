using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Glass.Design.Pcl.Canvas;

namespace Glass.Design.Pcl.DesignSurface
{
    public interface IDesignSurface : ICanvasSelector
    {               
        ICommand GroupCommand { get; }
        ICanvasItemContainer CanvasDocument { get; }
    }

    public interface ICanvasSelector
    {
        IList SelectedItems { get; }
    }


    public static class CanvasSelectorExtensions
    {
        public static IEnumerable<ICanvasItem> GetSelectedCanvasItems(this ICanvasSelector canvasSelector)
        {
            return canvasSelector.SelectedItems.Cast<ICanvasItem>();
        }
    }
}