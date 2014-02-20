using Model;
using StyleMVVM.DependencyInjection;

namespace ComicDesigner.Tooling.Tools
{
    [Export(typeof(ITool))]
    public class SpeechBubbleTool : Tool
    {
        public SpeechBubbleTool()
        {
            Name = "Speech Bubble";
            IconKey = "SpeechBubble";
        }
        public override CanvasItemViewModel CreateItem()
        {
            return new Bubble
                   {
                       Width = 300, 
                       Height = 200, 
                       Text = "Sample Text",
                   };

        }        
    }
}