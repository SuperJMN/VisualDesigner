using System.Collections.Generic;
using System.Windows.Input;
using ComicDesigner.Tools;
using StyleMVVM.DependencyInjection;
using StyleMVVM.ViewModel;

namespace ComicDesigner
{
    [Export("ToolbarViewModel")]
    
    public class ToolbarViewModel
    {
        private IToolProvider ToolProvider { get; set; }

        [ImportConstructor]
        public ToolbarViewModel(IToolProvider toolProvider)
        {
            ToolProvider = toolProvider;

            CreateObjectCommand = new DelegateCommand(parameter => CreateObject((ITool)parameter));
        }

        private void CreateObject(ITool tool)
        {
            var item = tool.CreateItem();            
        }

        public IEnumerable<ITool> Tools
        {
            get { return ToolProvider.Tools; }
        }

        public ICommand CreateObjectCommand { get; private set; }
    }
}