using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Design.Interfaces;
using Glass.Design.DesignSurface.VisualAids;

namespace Glass.Design.DesignSurface
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
        }

        private SelectionHandler SelectionHandler { get; set; }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            OnNoneSpecified();
        }

        private DesignAidsProvider DesignAidsProvider { get; set; }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            var newItems = selectionChangedEventArgs.AddedItems;
            var removedItems = selectionChangedEventArgs.RemovedItems;
            foreach (var newItem in newItems)
            {
                var container = ItemContainerGenerator.ContainerFromItem(newItem);
                DesignAidsProvider.AddItemToSelection((ICanvasItem)container);                
            }
            foreach (var removedItem in removedItems)
            {
                var container = ItemContainerGenerator.ContainerFromItem(removedItem);
                DesignAidsProvider.RemoveItemFromSelection((ICanvasItem)container);
            }
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            var designerItem = (DesignerItem)element;
            designerItem.PreviewMouseLeftButtonDown += ContainerOnLeftButtonDown;
            base.PrepareContainerForItemOverride(element, item);
        }

        private void ContainerOnLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var item = ItemContainerGenerator.ItemFromContainer((DependencyObject)sender);
            OnItemSpecified(item);            
            mouseButtonEventArgs.Handled = true;
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
            DependencyProperty.Register("OperationMode", typeof(DesignOperation), typeof(DesignSurface),
                new FrameworkPropertyMetadata(DesignOperation.Resize,
                    OnOperationModeChanged));

        public DesignOperation OperationMode
        {
            get { return (DesignOperation)GetValue(OperationModeProperty); }
            set { SetValue(OperationModeProperty, value); }
        }

        private static void OnOperationModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (DesignSurface)d;
            var oldOperationMode = (DesignOperation)e.OldValue;
            var newOperationMode = target.OperationMode;
            target.OnOperationModeChanged(oldOperationMode, newOperationMode);
        }

        private void OnOperationModeChanged(DesignOperation oldOperationMode, DesignOperation newOperationMode)
        {
            DesignAidsProvider.DesignOperation = newOperationMode;
        }

        #endregion

        public event EventHandler<object> ItemSpecified;

        private void OnItemSpecified(object e)
        {
            var handler = ItemSpecified;
            if (handler != null) handler(this, e);
        }

        public event EventHandler NoneSpecified;

        private void OnNoneSpecified()
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
            base.OnKeyDown(e);
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
    }
}
