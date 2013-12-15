using System.Collections;
using System.Collections.Generic;
using Glass.Design.CanvasItem;

namespace Glass.Design.DesignSurface
{
    public interface IDesignSurface
    {
        IEnumerable<ICanvasItem> CanvasItems { get; }
    }
}