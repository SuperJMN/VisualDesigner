using System.Collections.Generic;
using Glass.Design.Pcl.DesignSurface;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Recording;

namespace Glass.Design.Pcl.CanvasItem
{
    public interface ICanvasItem : IPositionable, ISizable, ICanvasItemContainer
    {
        double Right { get; }
        double Bottom { get; }

        ICanvasItemContainer Parent { get; }
    }

    public interface ICanvasItemContainer
    {
        CanvasItemCollection Children { get; }
    }

    public static class CanvasItemExtensions
    {
        public static Recorder GetRecorder(this IEnumerable<ICanvasItem> items)
        {
            foreach (ICanvasItem child in items)
            {
                Recorder recorder = child.GetRecorder();
                if (recorder != null)
                    return recorder;
            }

            return null;
        }

        public static Recorder GetRecorder(this ICanvasItemContainer item)
        {
            return item.QueryInterface<IRecordable>(true).Recorder ?? item.Children.GetRecorder();
        }
    }
}