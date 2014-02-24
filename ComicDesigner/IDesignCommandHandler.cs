using System.Windows.Input;
using ComicDesigner.UIUtils;

namespace ComicDesigner
{
    
    public interface IDesignCommandHandler
    {
        IEditingContext EditingContext { get; }
        RelayCommand UndoCommand { get; }
        RelayCommand RedoCommand { get; }
        RelayCommand LoadItemsCommand { get; }
        RelayCommand SendToBackCommand { get; }
        RelayCommand BringToFrontCommand { get; }
    }
}