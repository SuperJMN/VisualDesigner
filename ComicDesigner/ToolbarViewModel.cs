using System.Collections.Generic;
using System.Windows.Input;
using ComicDesigner.Tooling;
using Glass.Design.Pcl;
using Glass.Design.Pcl.Canvas;
using Model;
using PostSharp.Patterns.Recording;
using PostSharp.Patterns.Recording.Operations;
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
            using ( var scope = RecordingServices.DefaultRecorder.OpenScope() )
            {
                var newItem = tool.CreateItem( EditingContext );
                newItem.SetPosition( 100, 100 );

                // We name the scope after we created.
                scope.OperationDescriptor = new NamedOperationDescriptor(string.Format("Creating {0}", newItem.Name));

            }
        }

        public IEnumerable<ITool> Tools
        {
            get { return ToolProvider.Tools; }
        }

        public ICommand CreateObjectCommand { get; private set; }
        public IEditingContext EditingContext { get; set; }
    }
}