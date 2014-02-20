using System.Collections.Generic;
using System.Windows.Input;
using ComicDesigner.Tooling;
using Glass.Design.Pcl;
using Model;
using StyleMVVM.DependencyInjection;
using StyleMVVM.ViewModel;

namespace ComicDesigner
{
    
    [Export("ToolbarViewModel")]
    
    public class ToolbarViewModel
    {
        private IToolProvider ToolProvider { get; set; }

        [ImportConstructor]
        public ToolbarViewModel(IToolProvider toolProvider, IEditingContext editingContext)
        {
            ToolProvider = toolProvider;
            EditingContext = editingContext;            

            CreateObjectCommand = new DelegateCommand(parameter => CreateObject((ITool)parameter));
        }

        private void CreateObject(ITool tool)
        {
            var item = tool.CreateItem();    
            item.SetLocation(100, 100);
            Graphics.Add(item);
        }

        private CanvasItemViewModelCollection Graphics
        {
            get { return Document.Graphics; }
        }

        private Document Document
        {
            get { return EditingContext.Document; }
        }

        public IEnumerable<ITool> Tools
        {
            get { return ToolProvider.Tools; }
        }

        public ICommand CreateObjectCommand { get; private set; }
        public IEditingContext EditingContext { get; set; }
    }
}