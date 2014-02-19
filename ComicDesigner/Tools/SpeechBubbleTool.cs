using Model;
using StyleMVVM.DependencyInjection;

namespace ComicDesigner.Tools
{
    [Export(typeof(ITool))]
    public class SpeechBubbleTool : ITool
    {
        public string Name { get; set; }
        public CanvasItemViewModel CreateItem()
        {
            return new Bubble
                   {
                       Width = 300, 
                       Height = 300, 
                       Text = "Sample Text",
                   };

        }
    }
}