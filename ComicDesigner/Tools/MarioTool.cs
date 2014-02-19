using Model;
using StyleMVVM.DependencyInjection;

namespace ComicDesigner.Tools
{
    [Export(typeof(ITool))]
    public class MarioTool : ITool
    {
        public string Name { get; set; }
        public CanvasItemViewModel CreateItem()
        {
            return new Mario
                   {
                       Width = 200, 
                       Height = 240,
                   };
        }
    }
}