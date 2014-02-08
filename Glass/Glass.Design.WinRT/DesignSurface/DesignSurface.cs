using System;
using System.Collections;
using System.ComponentModel;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Glass.Design.Pcl.Annotations;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface;
using Glass.Design.Pcl.DesignSurface.VisualAids.Selection;
using Glass.Design.Pcl.PlatformAbstraction;
using Glass.Design.WinRT.DesignSurface.VisualAids;
using Glass.Design.WinRT.DesignSurface.VisualAids.Resize;
using PostSharp.Patterns.Model;

namespace Glass.Design.WinRT.DesignSurface
{
    [NotifyPropertyChanged]
    public sealed class DesignSurface : ItemsControl, IDesignSurface, IMultiSelector
    {

        public static readonly DependencyProperty CanvasDocumentProperty = DependencyProperty.Register("CanvasDocument",
            typeof(ICanvasItemContainer), typeof(DesignSurface), new PropertyMetadata(null, OnCanvasDocumentChanged));

        private static void OnCanvasDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            DesignSurface designSurface = ((DesignSurface)d);
            if (e.NewValue != null)
            {
                designSurface.ItemsSource = ((ICanvasItemContainer) e.NewValue).Children;
            }
            else
            {
                designSurface.ItemsSource = null;
            }
        }

        public DesignSurface()
        {
            
            //MouseLeftButtonDown += OnMouseLeftButtonDown;
            //SelectionChanged += OnSelectionChanged;
            DesignAidsProvider = new DesignAidsProvider(this);
            SelectionHandler = new SelectionHandler(this);
            CommandHandler = new DesignSurfaceCommandHandler(this, this);

            this.ManipulationStarted += OnManipulationStarted;

            DefaultStyleKey = typeof (DesignSurface);

        }

        private void OnManipulationStarted(object sender, ManipulationStartedRoutedEventArgs manipulationStartedRoutedEventArgs)
        {
            RaiseNoneSpecified();            
        }

        private DesignSurfaceCommandHandler CommandHandler { get; set; }

        
        private SelectionHandler SelectionHandler { get; set; }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            
        }

        private DesignAidsProvider DesignAidsProvider { get; set; }

        [IgnoreAutoChangeNotification]
        public ICanvasItemContainer CanvasDocument
        {
            get { return (ICanvasItemContainer) GetValue(CanvasDocumentProperty); }
            set { SetValue(CanvasDocumentProperty, value); }
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

        private void ContainerOnLeftButtonDown(object sender, InputDeviceEventHandlerArgs mouseButtonEventArgs)
        {
            var item = ItemContainerGenerator.ItemFromContainer((DependencyObject)sender);
             OnItemSelected(item);
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

        public IList SelectedItems { get; private set; }
        public event EventHandler<object> ItemSpecified;

        private void OnItemSelected(object e)
        {
            this.LastSelectedItem = e;
         
            var handler = ItemSpecified;
            if (handler != null) handler(this, e);
        }

        public object LastSelectedItem { get; private set; }

        public event EventHandler SelectionCleared;
        public void UnselectAll()
        {
            throw new NotImplementedException();
        }

        private void RaiseNoneSpecified()
        {
            this.LastSelectedItem = null;

            var handler = SelectionCleared;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        //protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        //{
        //    base.OnPreviewMouseLeftButtonDown(e);
        //    Focus();
        //}

        //protected override void OnPreviewKeyDown(KeyEventArgs e)
        //{
        //    OnKeyDown(e);
        //    if (e.Key == Key.LeftCtrl)
        //    {
        //        SelectionHandler.SelectionMode = SelectionMode.Add;
        //    }
        //}

        //protected override void OnPreviewKeyUp(KeyEventArgs e)
        //{
        //    base.OnKeyUp(e);
        //    if (e.Key == Key.LeftCtrl)
        //    {
        //        SelectionHandler.SelectionMode = SelectionMode.Direct;
        //    }
        //}


        public ICommand GroupCommand { get; private set; }

        [UsedImplicitly]
        public DesignSurfaceCommandHandler DesignSurfaceCommandHandler
        {
            get { return designSurfaceCommandHandler; }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public double GetCoordinate(CoordinatePart part)
        {
            throw new NotImplementedException();
        }

        public void SetCoordinate(CoordinatePart part, double value)
        {
            throw new NotImplementedException();
        }

        public double Left { get; set; }
        public double Top { get; set; }
        public CanvasItemCollection Children { get; private set; }
        public double Right { get; private set; }
        public double Bottom { get; private set; }
        public ICanvasItemContainer Parent { get; private set; }
        public event PointingManipulationEventHandler PointingManipulationStart;
        public event PointingManipulationEventHandler PointerMove;
        public event PointingManipulationEventHandler PointerManipulsationEnd;
        public void AddAdorner(IAdorner adorner)
        {
            throw new NotImplementedException();
        }

        public bool IsVisible { get; set; }

        public event FingerManipulationEventHandler FingerDown;
        public event FingerManipulationEventHandler FingerMove;
        public event FingerManipulationEventHandler FingerUp;
    }
}
