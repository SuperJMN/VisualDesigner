using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Model
{
    public class CanvasItemViewModelCollection : ObservableCollection<CanvasItemViewModel>
    {
        public CanvasItemViewModelCollection()
        {
            
        }

        public CanvasItemViewModelCollection(IEnumerable<CanvasItemViewModel> collection) : base(collection)
        {
            
        }
    }
}