using System;
using System.Collections.Specialized;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.DesignSurface;
using Glass.Design.Pcl.DesignSurface.VisualAids.Selection;
using Glass.Design.Wpf.DesignSurface.VisualAids;
using SelectionMode = Glass.Design.Pcl.DesignSurface.VisualAids.Selection.SelectionMode;

namespace Glass.Design.Wpf.DesignSurface
{
    public sealed class DesignSurface : MultiSelector, IDesignSurface, IMultiSelector
    {
        static DesignSurface()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(DesignSurface), new FrameworkPropertyMetadata(typeof(DesignSurface)));
        }

        public DesignSurface()
        {
            MouseLeftButtonDown += OnMouseLeftButtonDown;
            SelectionChanged += OnSelectionChanged;
            DesignAidsProvider = new DesignAidsProvider(this);
            SelectionHandler = new SelectionHandler(this);
          
            this.CommandBindings.Add(new CommandBinding(DesignSurfaceCommands.GroupCommand, GroupSelectedItems, CanGroupSelectedItems));            
        }

        private void CanGroupSelectedItems(object sender, CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            canExecuteRoutedEventArgs.CanExecute = SelectedItems.Count > 1;
        }

        private void GroupSelectedItems(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            var groupCommandArgs = (GroupCommandArgs) executedRoutedEventArgs.Parameter;
            var group = groupCommandArgs.CreateHostingItem();

            Children.Move(group);            
            
            Items.Add(group);
        }

        private SelectionHandler SelectionHandler { get; set; }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            RaiseNoneSpecified();
        }

        private DesignAidsProvider DesignAidsProvider { get; set; }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            var newItems = selectionChangedEventArgs.AddedItems;
            var removedItems = selectionChangedEventArgs.RemovedItems;
            foreach (ICanvasItem newItem in newItems)
            {
                DesignAidsProvider.AddItemToSelection(newItem);
            }
            foreach (ICanvasItem removedItem in removedItems)
            {
                DesignAidsProvider.RemoveItemFromSelection(removedItem);
            }
        }

        private void ContainerOnLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var item = ItemContainerGenerator.ItemFromContainer((DependencyObject)sender);
            OnItemSpecified(item);
            mouseButtonEventArgs.Handled = true;
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            var designerItem = (CanvasItemControl)element;
            designerItem.PreviewMouseLeftButtonDown += ContainerOnLeftButtonDown;
            base.PrepareContainerForItemOverride(element, item);
        }

        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            var designerItem = (CanvasItemControl)element;
            designerItem.PreviewMouseLeftButtonDown -= ContainerOnLeftButtonDown;
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is CanvasItemControl;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new CanvasItemControl();
        }

        #region PlaneOperationMode

        public static readonly DependencyProperty PlaneOperationModeProperty =
            DependencyProperty.Register("PlanePlaneOperationMode", typeof(PlaneOperation), typeof(DesignSurface),
                new FrameworkPropertyMetadata(PlaneOperation.Resize,
                    OnOperationModeChanged));

        public PlaneOperation PlaneOperationMode
        {
            get { return (PlaneOperation)GetValue(PlaneOperationModeProperty); }
            set { SetValue(PlaneOperationModeProperty, value); }
        }

        private static void OnOperationModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (DesignSurface)d;
            var oldOperationMode = (PlaneOperation)e.OldValue;
            var newOperationMode = target.PlaneOperationMode;
            target.OnOperationModeChanged(oldOperationMode, newOperationMode);
        }

        private void OnOperationModeChanged(PlaneOperation oldOperationMode, PlaneOperation newOperationMode)
        {
            DesignAidsProvider.PlaneOperation = newOperationMode;
        }

        #endregion

        public event EventHandler<object> ItemSpecified;

        private void OnItemSpecified(object e)
        {
            var handler = ItemSpecified;
            if (handler != null) handler(this, e);
        }

        public event EventHandler NoneSpecified;

        private void RaiseNoneSpecified()
        {
            var handler = NoneSpecified;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            Focus();
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            OnKeyDown(e);
            if (e.Key == Key.LeftCtrl)
            {
                SelectionHandler.SelectionMode = SelectionMode.Add;
            }
        }

        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.Key == Key.LeftCtrl)
            {
                SelectionHandler.SelectionMode = SelectionMode.Direct;
            }
        }

        public CanvasItemCollection Children { get; private set; }

        public ICommand GroupCommand { get; private set; }


        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);

            Children = new CanvasItemCollection(Items.Cast<ICanvasItem>());
            Children.CollectionChanged += ChildrenOnCollectionChanged;

            foreach (ICanvasItem child in Items)
            {
                child.Parent = this;
            }   
        }

        private void ChildrenOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (ICanvasItem newItem in notifyCollectionChangedEventArgs.NewItems)
                {
                    newItem.Parent = this;
                    Items.Add(newItem);
                }
            } else if (notifyCollectionChangedEventArgs.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (ICanvasItem oldItem in notifyCollectionChangedEventArgs.OldItems)
                {                    
                    Items.Remove(oldItem);
                }
            }
        }

        protected override void OnItemsChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnItemsChanged(e);

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (ICanvasItem child in e.NewItems)
                {
                    child.Parent = this;
                }             
            }
        }

    }
}
