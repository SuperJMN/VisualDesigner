using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Input;
using Windows.System;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface;
using Glass.Design.Pcl.DesignSurface.VisualAids.Selection;
using Glass.Design.Pcl.PlatformAbstraction;
using Glass.Design.WinRT.PlatformSpecific;
using PostSharp.Patterns.Model;
using FoundationPoint = Windows.Foundation.Point;
using DesignSurfaceSelectionMode = Glass.Design.Pcl.DesignSurface.VisualAids.Selection.SelectionMode;

namespace Glass.Design.WinRT.DesignSurface
{
    [NotifyPropertyChanged]
    public sealed class DesignSurface : DesignSurfaceBase, IDesignSurface
    {
        public static readonly DependencyProperty CanvasDocumentProperty = DependencyProperty.Register("CanvasDocument",
            typeof(ICanvasItemContainer), typeof(DesignSurface), new PropertyMetadata(null, OnCanvasDocumentChanged));

        public DesignSurface()
        {
            DefaultStyleKey = typeof(DesignSurface);            

            DesignAidsProvider = new DesignAidsProvider(this);
            SelectionHandler = new SelectionHandler(this);
            CommandHandler = new DesignSurfaceCommandHandler(this, this);

            PopupsDictionary = new Dictionary<IAdorner, Popup>();

            Children = new CanvasItemCollection();
            KeyDown += OnKeyDown;
            KeyUp += OnKeyUp;

            SelectionChanged += OnSelectionChanged;
        }

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

        private DesignSurfaceCommandHandler CommandHandler { get; set; }


        private SelectionHandler SelectionHandler { get; set; }

        private DesignAidsProvider DesignAidsProvider { get; set; }


        [IgnoreAutoChangeNotification]
        public ICanvasItemContainer CanvasDocument
        {
            get { return (ICanvasItemContainer)GetValue(CanvasDocumentProperty); }
            set { SetValue(CanvasDocumentProperty, value); }
        }
        
        public event EventHandler<object> ItemSpecified;

        public event EventHandler SelectionCleared;

        public ICommand GroupCommand { get; private set; }

        public void UnselectAll()
        {
            ClearSelectionPopups();
            
            //SelectedItems.Clear();
        }

        public event FingerManipulationEventHandler FingerDown;

        public event FingerManipulationEventHandler FingerMove;

        public event FingerManipulationEventHandler FingerUp;

        public void CaptureInput()
        {
            //CapturePointer(new Pointer());
        }

        public void ReleaseInput()
        {
            //ReleasePointerCaptures();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public double GetCoordinate(CoordinatePart part)
        {
            throw new NotImplementedException();
        }

        void ICoordinate.SetCoordinate(CoordinatePart part, double value)
        {
            throw new NotImplementedException();
        }

        public double Left { get; set; }
        public double Top { get; set; }

        public CanvasItemCollection Children { get; private set; }

        public double Right { get; private set; }
        public double Bottom { get; private set; }
        public ICanvasItemContainer Parent { get; private set; }

        public void AddAdorner(IAdorner adorner)
        {
            var popup = new Popup();

            var coreInstance = adorner.GetCoreInstance();

            var uiElementAdapter = (FrameworkElementAdapter) coreInstance;

            popup.Child = (UIElement) uiElementAdapter.GetCoreInstance();

            popup.HorizontalOffset = adorner.Left;
            popup.VerticalOffset = adorner.Top;
            popup.IsOpen = true;

            PopupsDictionary.Add(adorner, popup);
        }

        public void RemoveAdorner(IAdorner adorner)
        {
            var popup = PopupsDictionary[adorner];
            popup.IsOpen = false;
            PopupsDictionary.Remove(adorner);
        }

        bool IUIElement.IsVisible { get; set; }

        public object GetCoreInstance()
        {
            return this;
        }

        private static void OnCanvasDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var designSurface = ((DesignSurface)d);
            if (e.NewValue != null)
            {
                designSurface.ItemsSource = ((ICanvasItemContainer)e.NewValue).Children;
            }
            else
            {
                designSurface.ItemsSource = null;
            }
        }


        protected override void OnFingerDown(FingerManipulationEventArgs args)
        {
            base.OnFingerDown(args);
            RaiseNoneSpecified();
        }


        private void ContainerOnPointerPressed(object sender, PointerRoutedEventArgs pointerRoutedEventArgs)
        {
            var currentPoint = pointerRoutedEventArgs.GetCurrentPoint(null);
            if (currentPoint.Properties.IsLeftButtonPressed)
            {
                var item = ItemContainerGenerator.ItemFromContainer((DependencyObject)sender);
                RaiseItemSpecified(item);
                pointerRoutedEventArgs.Handled = true;
            }
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            var designerItem = (CanvasItemControl)element;
            designerItem.PointerPressed += ContainerOnPointerPressed;
        }

        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            base.ClearContainerForItemOverride(element, item);

            var designerItem = (CanvasItemControl)element;
            designerItem.PointerPressed -= ContainerOnPointerPressed;
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is CanvasItemControl;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new CanvasItemControl();
        }

        private void RaiseItemSpecified(object e)
        {
            SelectedItem = e;

            var handler = ItemSpecified;
            if (handler != null) handler(this, e);
        }

        private void RaiseNoneSpecified()
        {
            SelectedItem = null;

            var handler = SelectionCleared;
            if (handler != null) handler(this, EventArgs.Empty);
        }



        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                RaisePropertyChanged(propertyName);
            }
        }

        private void RaisePropertyChanged(string propertyName)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region PlaneOperationMode

        public static readonly DependencyProperty PlaneOperationModeProperty =
            DependencyProperty.Register("PlanePlaneOperationMode", typeof(PlaneOperation), typeof(DesignSurface),
                new PropertyMetadata(PlaneOperation.Resize,
                    OnOperationModeChanged));

        private readonly DesignSurfaceCommandHandler designSurfaceCommandHandler;

        private ICanvasItem _rootCanvasItem;


        [IgnoreAutoChangeNotification]
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

        private CanvasItemCollection children;
        
        
        private Dictionary<IAdorner, Popup> PopupsDictionary { get; set; }
        
        private void ClearSelectionPopups()
        {
            foreach (ICanvasItem item in SelectedItems)
            {
                DesignAidsProvider.RemoveItemFromSelection(item);
            }
        }

        private void OnKeyDown(object sender, KeyRoutedEventArgs keyRoutedEventArgs)
        {

            if (keyRoutedEventArgs.Key == VirtualKey.Control && keyRoutedEventArgs.KeyStatus.WasKeyDown)
            {
                SelectionHandler.SelectionMode = DesignSurfaceSelectionMode.Add;
            }
        }

        private void OnKeyUp(object sender, KeyRoutedEventArgs keyRoutedEventArgs)
        {
            if (keyRoutedEventArgs.Key == VirtualKey.Control && keyRoutedEventArgs.KeyStatus.IsKeyReleased)
            {
                SelectionHandler.SelectionMode = DesignSurfaceSelectionMode.Direct;
            }

        }


    }
}