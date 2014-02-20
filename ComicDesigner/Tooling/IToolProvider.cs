using System.Collections.Generic;

namespace ComicDesigner.Tooling
{
    public interface IToolProvider
    {
        IEnumerable<ITool> Tools { get; }
    }
}