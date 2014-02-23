using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using Model;
using StyleMVVM.ViewModel;

namespace ComicDesigner.PropertyPages
{
    public class FrameViewModel : BaseViewModel
    {
        private Frame frame;
        
        public IEditingContext EditingContext { get; set; }

        public FrameViewModel(IEditingContext editingContext)
        {
            EditingContext = editingContext;
            EditingContext.SelectedItems.CollectionChanged += SelectedItemsOnCollectionChanged;                        
        }

        private void SelectedItemsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            var frames = EditingContext.SelectedItems.OfType<Frame>();

            var firstBubble = frames.FirstOrDefault();

            if (firstBubble != null)
            {
                Frame = firstBubble;
            }
        }

        public Color Background
        {
            get { return Frame.Background; }
            set
            {                
                Frame.Background = value;
                OnPropertyChanged();
            }
        }

        public Color Stroke
        {
            get { return Frame.Stroke; }
            set
            {
                Frame.Stroke = value;
                OnPropertyChanged();
            }
        }


    

        public Frame Frame
        {
            get { return frame; }
            set
            {
                frame = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChangeFontSizeCommand { get; set; }
    }
}