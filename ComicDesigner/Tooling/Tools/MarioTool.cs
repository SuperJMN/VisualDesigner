using Model;
using StyleMVVM.DependencyInjection;

namespace ComicDesigner.Tooling.Tools
{
    [Export(typeof(ITool))]
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
                       Width = 200, 
                       Height = 240,
                   };
        }
        
    }
}