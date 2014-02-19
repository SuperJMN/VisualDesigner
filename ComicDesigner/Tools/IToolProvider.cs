using System.Collections.Generic;

namespace ComicDesigner.Tools
{
    public interface IToolProvider
    {
        IEnumerable<ITool> Tools { get; }
    }
}