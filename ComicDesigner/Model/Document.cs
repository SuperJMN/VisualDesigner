namespace Model
{
    public class Document
    {
        public Document()
        {
            Graphics = new CanvasItemViewModelCollection();
        }
        public CanvasItemViewModelCollection Graphics { get; set; }
    }
}