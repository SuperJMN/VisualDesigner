using System.Collections.Generic;
using System.Collections.ObjectModel;
using PostSharp.Patterns.Collections;

namespace Model
{
    public class CanvasItemViewModelCollection : AdvisableCollection<CanvasItemViewModel>
    {
        public CanvasItemViewModelCollection()
        {
            
        }

        public CanvasItemViewModelCollection(IEnumerable<CanvasItemViewModel> collection) : base(collection)
        {
            
        }
    }
}