using System.Collections.Generic;
using System.Windows.Input;
using ComicDesigner.Tooling;
using Glass.Design.Pcl;
using Glass.Design.Pcl.Canvas;
using Model;
using PostSharp.Patterns.Recording;
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
            using ( var scope = RecordingServices.AmbientRecorder.StartAtomicScope( string.Format( "Creating {0}", tool.Name ) ) )
            {
                var newItem = tool.CreateItem( EditingContext );
                newItem.SetPosition( 100, 100 );

                scope.Complete();
            }
        }

        private CanvasItemCollection Graphics
        {
            get { return Document.Children; }
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