using Model;
using PostSharp.Patterns.Recording;

namespace ComicDesigner.Tooling
{
    public interface ITool
    {
        string Name { get; set; }
        string IconKey { get; set; }
        CanvasItemViewModel CreateItem(IEditingContext editingContext);
    }

    public abstract class Tool : ITool
    {
        public string Name { get; set; }
        public abstract CanvasItemViewModel CreateItem();

        public string IconKey { get; set; }
        public CanvasItemViewModel CreateItem(IEditingContext editingContext)
        {
            
            var items = editingContext.Document.Children;
            var canvasItemViewModel = CreateItem();

            if ( this.InsertOrder == InsertOrder.ToEnd )
            {
                items.Add( canvasItemViewModel );
            }
            else
            {
                items.Insert( 0, canvasItemViewModel );
            }

            return canvasItemViewModel;
            
        }

        public InsertOrder InsertOrder { get; set; }
    }

    public enum InsertOrder
    {
        ToEnd = 0,
        ToBeginning,        
    }
}