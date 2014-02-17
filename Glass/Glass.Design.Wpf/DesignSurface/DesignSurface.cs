using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using AutoMapper;
using Glass.Design.Pcl.Canvas;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface;
using Glass.Design.Pcl.DesignSurface.VisualAids.Selection;
using Glass.Design.Pcl.PlatformAbstraction;
using Glass.Design.Wpf.Annotations;
using PostSharp.Patterns.Model;
using Point = Glass.Design.Pcl.Core.Point;
using DesignSurfaceSelectionMode = Glass.Design.Pcl.DesignSurface.VisualAids.Selection.SelectionMode;
using SelectionMode = System.Windows.Controls.SelectionMode;

namespace Glass.Design.Wpf.DesignSurface
{
    [NotifyPropertyChanged]
    public sealed class DesignSurface : ListBox, IDesignSurface
    {
        public static readonly DependencyProperty CanvasDocumentProperty = DependencyProperty.Register("CanvasDocument",
            typeof (ICanvasItemContainer), typeof (DesignSurface),
            new FrameworkPropertyMetadata(null, OnCanvasDocumentChanged));

        static DesignSurface()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (DesignSurface),
                new FrameworkPropertyMetadata(typeof (DesignSurface)));
        }

        public DesignSurface()
        {
            this.SelectionMode = SelectionMode.Multiple;
            MouseLeftButtonDown += OnMouseLeftButtonDown;
            SelectionChanged += OnSelectionChanged;
            DesignAidsProvider = new DesignAidsProvider(this);
            SelectionHandler = new SelectionHandler(this);
            CommandHandler = new DesignSurfaceCommandHandler(this, this);
        }

        private DesignSurfaceCommandHandler CommandHandler { get; set; }


        private SelectionHandler SelectionHandler { get; set; }

        private DesignAidsProvider DesignAidsProvider { get; set; }

        [IgnoreAutoChangeNotification]
        public ICanvasItemContainer CanvasDocument
        {
            get { return (ICanvasItemContainer) GetValue(CanvasDocumentProperty); }
            set { SetValue(CanvasDocumentProperty, value); }
        }


        IList<object> IMultiSelector.SelectedItems
        {
            get { return SelectedItems.Cast<object>().ToList(); }            
        }

        public event EventHandler<object> ItemSpecified;
        
        public event EventHandler SelectionCleared;


        public ICommand GroupCommand { get; private set; }


        public event FingerManipulationEventHandler FingerDown;

        public event FingerManipulationEventHandler FingerMove;

        public event FingerManipulationEventHandler FingerUp;

        public void CaptureInput()
        {
            CaptureMouse();
        }

        public void ReleaseInput()
        {
            ReleaseMouseCapture();
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

        CanvasItemCollection ICanvasItemContainer.Children
        {
            get { return new CanvasItemCollection(Items.Cast<ICanvasItem>()); }
            
        }

        public double Right { get; private set; }
        public double Bottom { get; private set; }
        public ICanvasItemContainer Parent { get; private set; }

        public void AddAdorner(IAdorner adorner)
        {
            var coreInstance = (Visual) GetCoreInstance();
            var adornerLayer = AdornerLayer.GetAdornerLayer(coreInstance);
            adornerLayer.Add((Adorner) adorner);
        }

        public void RemoveAdorner(IAdorner adorner)
        {
            var coreInstance = (Visual) GetCoreInstance();
            var adornerLayer = AdornerLayer.GetAdornerLayer(coreInstance);
            adornerLayer.Remove((Adorner) adorner);
        }

        public bool IsVisible { get; set; }

        public object GetCoreInstance()
        {
            return this;
        }

        private static void OnCanvasDocumentChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var designSurface = ((DesignSurface) d);
            if (e.NewValue != null)
            {
                designSurface.ItemsSource = ((ICanvasItemContainer) e.NewValue).Children;
            }
            else
            {
                designSurface.ItemsSource = null;
            }
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            //RaiseNoneSpecified();
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

        private void ContainerOnLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var item = ItemContainerGenerator.ItemFromContainer((DependencyObject) sender);
            RaiseItemSpecified(item);
            mouseButtonEventArgs.Handled = true;
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            base.PrepareContainerForItemOverride(element, item);

            var designerItem = (DesignSurfaceItem) element;
            designerItem.PreviewMouseLeftButtonDown += ContainerOnLeftButtonDown;            
        }

        protected override void ClearContainerForItemOverride(DependencyObject element, object item)
        {
            base.ClearContainerForItemOverride(element, item);

            var designerItem = (DesignSurfaceItem) element;
            designerItem.PreviewMouseLeftButtonDown -= ContainerOnLeftButtonDown;
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DesignSurfaceItem;
        }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DesignSurfaceItem();
        }

        private void RaiseItemSpecified(object e)
        {
            var handler = ItemSpecified;
            if (handler != null) handler(this, e);
        }

        private void RaiseNoneSpecified()
        {
            var handler = SelectionCleared;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        protected override void OnPreviewMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonDown(e);
            Focus();

            OnFingerDown(new FingerManipulationEventArgs());
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            OnFingerMove(new FingerManipulationEventArgs());
        }

        protected override void OnPreviewKeyDown(KeyEventArgs e)
        {
            OnKeyDown(e);
            if (e.Key == Key.LeftCtrl)
            {
                SelectionHandler.SelectionMode = DesignSurfaceSelectionMode.Add;
            }
        }

        protected override void OnPreviewKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            if (e.Key == Key.LeftCtrl)
            {
                SelectionHandler.SelectionMode = DesignSurfaceSelectionMode.Direct;
            }
        }

        private void OnFingerDown(FingerManipulationEventArgs args)
        {
            var handler = FingerDown;
            if (handler != null) handler(this, args);
        }

        private void OnFingerMove(FingerManipulationEventArgs args)
        {
            var handler = FingerMove;
            if (handler != null) handler(this, args);
        }

        private void OnFingerUp(FingerManipulationEventArgs args)
        {
            var handler = FingerUp;
            if (handler != null) handler(this, args);
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


        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);

            var point = e.GetPosition(null);
            var pclPoint = Mapper.Map<Point>(point);
            OnFingerUp(new FingerManipulationEventArgs());
        }

        #region PlaneOperationMode

        public static readonly DependencyProperty PlaneOperationModeProperty =
            DependencyProperty.Register("PlanePlaneOperationMode", typeof (PlaneOperation), typeof (DesignSurface),
                new FrameworkPropertyMetadata(PlaneOperation.Resize,
                    OnOperationModeChanged));

        private readonly DesignSurfaceCommandHandler designSurfaceCommandHandler;
        private ICanvasItem _rootCanvasItem;
        private IList<object> selectedItems;
        private CanvasItemCollection children;

        [IgnoreAutoChangeNotification]
        public PlaneOperation PlaneOperationMode
        {
            get { return (PlaneOperation) GetValue(PlaneOperationModeProperty); }
            set { SetValue(PlaneOperationModeProperty, value); }
        }

        private static void OnOperationModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (DesignSurface) d;
            var oldOperationMode = (PlaneOperation) e.OldValue;
            var newOperationMode = target.PlaneOperationMode;
            target.OnOperationModeChanged(oldOperationMode, newOperationMode);
        }

        private void OnOperationModeChanged(PlaneOperation oldOperationMode, PlaneOperation newOperationMode)
        {
            DesignAidsProvider.PlaneOperation = newOperationMode;
        }

        #endregion

        [UsedImplicitly]
        public DesignSurfaceCommandHandler DesignSurfaceCommandHandler
        {
            get { return designSurfaceCommandHandler; }
        }
    }
}