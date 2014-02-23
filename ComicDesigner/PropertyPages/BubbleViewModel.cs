using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml;
using ComicDesigner.UIUtils;
using Model;
using StyleMVVM.ViewModel;

namespace ComicDesigner.PropertyPages
{
    public class BubbleViewModel : BaseViewModel
    {
        private Bubble bubble;
        
        public IEditingContext EditingContext { get; set; }

        public BubbleViewModel(IEditingContext editingContext)
        {
            EditingContext = editingContext;
            EditingContext.SelectedItems.CollectionChanged += SelectedItemsOnCollectionChanged;            

            ChangeFontSizeCommand = new DelegateCommand(ChangeFontSize);
        }

        private void ChangeFontSize(object parameter)
        {
            var fontSize = (double) parameter;
            FontSize = fontSize;
        }

        public double FontSize
        {
            get { return Bubble.FontSize; }
            set { Bubble.FontSize = value; }
        }

        private void SelectedItemsOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            var bubbles = EditingContext.SelectedItems.OfType<Bubble>();

            var firstBubble = bubbles.FirstOrDefault();

            if (firstBubble != null)
            {
                Bubble = firstBubble;
            }
        }

        public Color Background
        {
            get { return Bubble.Background; }
            set
            {                
                Bubble.Background = value;
                OnPropertyChanged();
            }
        }

        public Color TextColor
        {
            get { return Bubble.TextColor; }
            set
            {
                Bubble.TextColor = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get { return Bubble.Text; }
            set
            {
                Bubble.Text = value;
                OnPropertyChanged();
            }
        }

        public string FontName
        {
            get { return Bubble.FontName; }
            set
            {
                Bubble.FontName = value;
                OnPropertyChanged();
            }
        }

        public Bubble Bubble
        {
            get { return bubble; }
            set
            {
                bubble = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChangeFontSizeCommand { get; set; }

    
    }
}