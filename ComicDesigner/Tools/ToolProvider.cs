using System.Collections.Generic;
using StyleMVVM.DependencyInjection;

namespace ComicDesigner.Tools
{
    [Export(typeof(IToolProvider))]
    public partial class ToolProvider : IToolProvider
    {
        private IEnumerable<ITool> tools;

        [ImportConstructor]
        public ToolProvider(IEnumerable<ITool> tools)
        {
            this.tools = tools;
        }

        public IEnumerable<ITool> Tools
        {
            get { return tools; }
            private set { tools = value;  }
        }
    }
}