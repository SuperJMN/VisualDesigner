using StyleMVVM;
using StyleMVVM.DependencyInjection;

namespace ComicDesigner
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ToolbarViewModel = Bootstrapper.Instance.Container.Locate<ToolbarViewModel>();
        }
        public ToolbarViewModel ToolbarViewModel { get; set; }
    }
}