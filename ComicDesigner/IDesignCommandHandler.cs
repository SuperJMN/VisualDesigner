using System.Windows.Input;
using ComicDesigner.UIUtils;

namespace ComicDesigner
{
    
    public interface IDesignCommandHandler
    {
        IEditingContext EditingContext { get; }
        ICommand UndoCommand { get; set; }
        ICommand RedoCommand { get; }
        RelayCommand LoadItemsCommand { get; }
        RelayCommand SendToBackCommand { get; }
        RelayCommand BringToFrontCommand { get; }
    }
}