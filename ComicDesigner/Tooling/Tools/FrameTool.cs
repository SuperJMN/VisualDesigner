using Glass.Design.WinRT.Annotations;
using Model;
using StyleMVVM.DependencyInjection;

namespace ComicDesigner.Tooling.Tools
{
    [Export(typeof(ITool)), UsedImplicitly]
    public class FrameTool : Tool
    {
        public FrameTool()
        {
            Name = "Frame";
            IconKey = "Frame";
        }
        
        public override CanvasItemViewModel CreateItem()
        {
            return new Frame
                   {
                       Width = 400, 
                       Height = 300,
                   };
        }
        
    }
}