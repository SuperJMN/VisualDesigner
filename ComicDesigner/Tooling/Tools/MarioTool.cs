using Glass.Design.WinRT.Annotations;
using Model;
using StyleMVVM.DependencyInjection;

namespace ComicDesigner.Tooling.Tools
{
    [Export(typeof(ITool)), UsedImplicitly]
    public class MarioTool : Tool
    {
        public MarioTool()
        {
            Name = "Mario";
            IconKey = "Mario";
        }
        
        public override CanvasItemViewModel CreateItem()
        {
            return new Mario
                   {
                       Width = 170, 
                       Height = 200,
                   };
        }
        
    }
}