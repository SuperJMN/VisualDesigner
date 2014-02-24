using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Windows.UI.Xaml.Controls;
using Glass.Design.Pcl;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Model;
using StyleMVVM.DependencyInjection;
using StyleMVVM.ViewModel;

namespace ComicDesigner
{
    [Export("MainViewModel")]
    public class MainViewModel : BaseViewModel
    {
        private IEditingContext EditingContext { get; set; }
        public IDesignCommandHandler DesignCommandHandler { get; set; }
        private CanvasItemViewModel selectedItem;
        private CanvasItemCollection selectedItems;

        [ImportConstructor]
        public MainViewModel(IEditingContext editingContext, IDesignCommandHandler designCommandHandler)
        {
            EditingContext = editingContext;
            DesignCommandHandler = designCommandHandler;

            // Since we're Main, we have to instance the document into the editing context.
            EditingContext.Document = new Document();            
            ChangeSelectedItemsCommand = new DelegateCommand(ChangeSelectedItems);
        }

        private void ChangeSelectedItems(object param)
        {
            var selectionChangedEventArgs = (SelectionChangedEventArgs) param;

            foreach (var addedItem in selectionChangedEventArgs.AddedItems.Cast<CanvasItemViewModel>())
            {
                SelectedItems.Add(addedItem);
            }
            foreach (var removedItem in selectionChangedEventArgs.RemovedItems.Cast<CanvasItemViewModel>())
            {
                SelectedItems.Remove(removedItem);
            }
        }

        public double SurfaceWidth
        {
            get { return EditingContext.SurfaceWidth; }
            set { EditingContext.SurfaceWidth = value; } 
        }

        public double SurfaceHeight
        {
            get { return EditingContext.SurfaceHeight; }
            set { EditingContext.SurfaceHeight = value; }
        }

        public CanvasItemCollection Items
        {
            get { return EditingContext.Document.Children; }
        }

        public CanvasItemViewModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged();
            }
        }

        public CanvasItemCollection SelectedItems
        {
            get
            {
                return EditingContext.SelectedItems;
            }
            set
            {
                EditingContext.SelectedItems = value;
                OnPropertyChanged();
            }
        }

        public ICommand ChangeSelectedItemsCommand { get; private set; }
    }
}