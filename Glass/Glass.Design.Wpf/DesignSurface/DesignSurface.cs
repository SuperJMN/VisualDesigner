using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

            this.CommandBindings.Add(new CommandBinding(DesignSurfaceCommands.GroupCommand, Executed));
        }

        private void CanExecute(object sender, CanExecuteRoutedEventArgs canExecuteRoutedEventArgs)
        {
            canExecuteRoutedEventArgs.CanExecute = false;
        }

        private void Executed(object sender, ExecutedRoutedEventArgs executedRoutedEventArgs)
        {
            var groupCommandArgs = (GroupCommandArgs) executedRoutedEventArgs.Parameter;
            var group = groupCommandArgs.CreateCanvasItem();

            Mover.Move(CanvasItems, group);            
            
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
            var designerItem = (DesignerItem)element;
            designerItem.PreviewMouseLeftButtonDown += ContainerOnLeftButtonDown;
            base.PrepareContainerForItemOverride(element, item);
        }

        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            var designerItem = (DesignerItem)element;
            designerItem.PreviewMouseLeftButtonDown -= ContainerOnLeftButtonDown;
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DesignerItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DesignerItem();
        }

        #region OperationMode

        public static readonly DependencyProperty OperationModeProperty =
            DependencyProperty.Register("OperationMode", typeof(PlaneOperation), typeof(DesignSurface),
                new FrameworkPropertyMetadata(PlaneOperation.Resize,
                    OnOperationModeChanged));

        public PlaneOperation OperationMode
        {
            get { return (PlaneOperation)GetValue(OperationModeProperty); }
            set { SetValue(OperationModeProperty, value); }
        }

        private static void OnOperationModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (DesignSurface)d;
            var oldOperationMode = (PlaneOperation)e.OldValue;
            var newOperationMode = target.OperationMode;
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

        public new IEnumerable<ICanvasItem> CanvasItems
        {
            get
            {
                return Items.Cast<ICanvasItem>();                 
            }
        }

        public ICommand GroupCommand { get; private set; }
    }
}
