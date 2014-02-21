using System.Windows.Input;

namespace ComicDesigner
{
    
    public interface IDesignCommandHandler
    {
        IEditingContext EditingContext { get; }
        ICommand UndoCommand { get; set; }
        ICommand RedoCommand { get; }
        ICommand LoadItemsCommand { get; }
    }
}