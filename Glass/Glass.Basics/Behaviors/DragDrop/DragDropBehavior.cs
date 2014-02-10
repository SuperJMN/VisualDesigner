using System;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;
using Glass.Basics.Wpf.Extensions;

namespace Glass.Basics.Wpf.Behaviors.DragDrop
{
    public class DragDropBehavior : Behavior<FrameworkElement>
    {
        private Panel hostPanel;
        private Point startingPoint;
        private bool isDragging;
        private UIElement childInDrag;
        private ItemsControl itemsControl;

        protected override void OnAttached()
        {
            SubscribeAssociatedObjectToEvents();
        }

        private void SetAllowDrop(DropModes dropMode)
        {
            var isAssociatedObjectDropTarget = (dropMode == DropModes.DropOntoSelf);

            AssociatedObject.AllowDrop = isAssociatedObjectDropTarget;

            if (hostPanel != null)
            {
                foreach (UIElement uiElement in hostPanel.Children)
                {
                    uiElement.AllowDrop = !isAssociatedObjectDropTarget;
                }
            }
        }

        private void SubscribeAssociatedObjectToEvents()
        {
            AssociatedObject.Loaded += AssociatedObjectOnLoaded;
            AssociatedObject.PreviewMouseMove += AssociatedObjectOnPreviewMouseMove;
            AssociatedObject.PreviewMouseLeftButtonDown += AssociatedObjectOnPreviewMouseLeftButtonDown;
            AssociatedObject.PreviewDragOver += AssociatedObjectOnPreviewDragOver;
            AssociatedObject.Drop += AssociatedObjectOnDrop;
        }

        private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            if (AssociatedObject is ItemsControl)
            {
                itemsControl = (ItemsControl)AssociatedObject;
                hostPanel = AssociatedObject.FindItemsPanel();
                SetAllowDrop(DropMode);
                itemsControl.ItemContainerGenerator.StatusChanged += ItemContainerGeneratorOnStatusChanged;
            }
            else if (AssociatedObject is Panel)
            {
                hostPanel = (Panel)AssociatedObject;
                SetAllowDrop(DropMode);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

        private void ItemContainerGeneratorOnStatusChanged(object sender, EventArgs eventArgs)
        {
            if (itemsControl.ItemContainerGenerator.Status != GeneratorStatus.NotStarted)
            {
                if (hostPanel == null)
                {
                    hostPanel = AssociatedObject.FindItemsPanel();
                    SetAllowDrop(DropMode);
                }
            }
        }

        private void AssociatedObjectOnPreviewDragOver(object sender, DragEventArgs dragEventArgs)
        {
            if (!IsDropTarget)
            {
                dragEventArgs.Effects = DragDropEffects.None;
                dragEventArgs.Handled = true;
                return;
            }

            //var potentialDropTarget = GetDropTarget(dragEventArgs.GetPosition(AssociatedObject));
            if (!IsValidDropTarget(dragEventArgs.Data))
            {
                dragEventArgs.Effects = DragDropEffects.None;
                dragEventArgs.Handled = true;
            }
        }

        private bool IsValidDropTarget(IDataObject dataObject)
        {
            if (DropCommand == null)
            {
                return false;
            }

            return DropCommand.CanExecute(GetObjectFromData(dataObject));
        }

        private void AssociatedObjectOnDrop(object sender, DragEventArgs dragEventArgs)
        {
            Debug.WriteLine(dragEventArgs.Data);
            dragEventArgs.Handled = true;
            DropTarget = (UIElement)dragEventArgs.Source;
            DropPoint = dragEventArgs.GetPosition(AssociatedObject);
            ExecuteDropCommandIfAvailable(dragEventArgs.Data);
        }

        private void AssociatedObjectOnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            startingPoint = mouseButtonEventArgs.GetPosition(AssociatedObject);
            childInDrag = ChildrenRetriever.GetChildAt(hostPanel.Children, startingPoint);
            Debug.WriteLine(childInDrag);
        }

        private void AssociatedObjectOnPreviewMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            if (mouseEventArgs.LeftButton == MouseButtonState.Pressed && !isDragging && IsDragSource)
            {
                var position = mouseEventArgs.GetPosition(AssociatedObject);

                if (Math.Abs(position.X - startingPoint.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(position.Y - startingPoint.Y) > SystemParameters.MinimumVerticalDragDistance)
                {
                    if (childInDrag != null)
                        StartDrag();
                }
            }
        }

        private void StartDrag()
        {
            var data = new DataObject(GetItemToExport(childInDrag));
            System.Windows.DragDrop.DoDragDrop(AssociatedObject, data, DragDropEffects.Move);

            isDragging = false;
        }

        private object GetItemToExport(object childBeingDragged)
        {

            if (AssociatedObject is ItemsControl && ExportItems)
            {
                var itemsControl = (ItemsControl)AssociatedObject;
                childBeingDragged = itemsControl.ItemContainerGenerator.ItemFromContainer(childInDrag);
            }
            return childBeingDragged;
        }

        private void ExecuteDropCommandIfAvailable(IDataObject data)
        {
            if (DropCommand != null)
            {
                var sourceObject = GetObjectFromData(data);
                DropCommand.Execute(sourceObject);
            }
        }

        private static object GetObjectFromData(IDataObject data)
        {
            var dataFormats = data.GetFormats();
            var dataType = dataFormats.First();
            return data.GetData(dataType);
        }

        #region IsDragSource

        public static readonly DependencyProperty IsDragSourceProperty =
            DependencyProperty.Register("IsDragSource", typeof(bool), typeof(DragDropBehavior),
                                        new FrameworkPropertyMetadata(true,
                                                                      OnIsDropSourceChanged));

        public bool IsDragSource
        {
            get { return (bool)GetValue(IsDragSourceProperty); }
            set { SetValue(IsDragSourceProperty, value); }
        }

        private static void OnIsDropSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (DragDropBehavior)d;
            var oldIsDropSource = (bool)e.OldValue;
            var newIsDropSource = target.IsDragSource;
            target.OnIsDropSourceChanged(oldIsDropSource, newIsDropSource);
        }

        protected void OnIsDropSourceChanged(bool oldIsDropSource, bool newIsDropSource)
        {

        }

        #endregion

        #region IsDropTarget

        public static readonly DependencyProperty IsDropTargetProperty =
            DependencyProperty.Register("IsDropTarget", typeof(bool), typeof(DragDropBehavior),
                                        new FrameworkPropertyMetadata(true,
                                                                      OnIsDropTargetChanged));

        public bool IsDropTarget
        {
            get { return (bool)GetValue(IsDropTargetProperty); }
            set { SetValue(IsDropTargetProperty, value); }
        }

        private static void OnIsDropTargetChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (DragDropBehavior)d;
            var oldIsDropTarget = (bool)e.OldValue;
            var newIsDropTarget = target.IsDropTarget;
            target.OnIsDropTargetChanged(oldIsDropTarget, newIsDropTarget);
        }

        protected void OnIsDropTargetChanged(bool oldIsDropTarget, bool newIsDropTarget)
        {
            if (AssociatedObject != null)
            {
                AssociatedObject.AllowDrop = true;
            }
        }

        #endregion

        #region DropMode

        public static readonly DependencyProperty DropModeProperty =
            DependencyProperty.Register("DropMode", typeof(DropModes), typeof(DragDropBehavior),
                new FrameworkPropertyMetadata(DropModes.DropOntoSelf,
                    OnDropModeChanged));

        public DropModes DropMode
        {
            get { return (DropModes)GetValue(DropModeProperty); }
            set { SetValue(DropModeProperty, value); }
        }

        private static void OnDropModeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (DragDropBehavior)d;
            var oldDropMode = (DropModes)e.OldValue;
            var newDropMode = target.DropMode;
            target.OnDropModeChanged(oldDropMode, newDropMode);
        }

        protected void OnDropModeChanged(DropModes oldDropMode, DropModes newDropMode)
        {
            if (AssociatedObject != null)
            {
                SetAllowDrop(newDropMode);
            }
        }

        #endregion

        #region DropCommand
        public static readonly DependencyProperty DropCommandProperty =
          DependencyProperty.Register("DropCommand", typeof(ICommand), typeof(DragDropBehavior),
            new FrameworkPropertyMetadata((ICommand)null));

        public ICommand DropCommand
        {
            get { return (ICommand)GetValue(DropCommandProperty); }
            set { SetValue(DropCommandProperty, value); }
        }

        #endregion

        #region ExportItems
        public static readonly DependencyProperty ExportItemsProperty =
          DependencyProperty.Register("ExportItems", typeof(bool), typeof(DragDropBehavior),
            new FrameworkPropertyMetadata(false));

        public bool ExportItems
        {
            get { return (bool)GetValue(ExportItemsProperty); }
            set { SetValue(ExportItemsProperty, value); }
        }

        #endregion

        #region AllowedDropType
        public static readonly DependencyProperty AllowedDropTypeProperty =
          DependencyProperty.Register("AllowedDropType", typeof(Type), typeof(DragDropBehavior),
            new FrameworkPropertyMetadata((Type)null));

        public Type AllowedDropType
        {
            get { return (Type)GetValue(AllowedDropTypeProperty); }
            set { SetValue(AllowedDropTypeProperty, value); }
        }

        #endregion

        #region DropTarget
        public static readonly DependencyProperty DropTargetProperty =
          DependencyProperty.Register("DropTarget", typeof(UIElement), typeof(DragDropBehavior),
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public UIElement DropTarget
        {
            get { return (UIElement)GetValue(DropTargetProperty); }
            set { SetValue(DropTargetProperty, value); }
        }

        #endregion

        #region DropPoint
        public static readonly DependencyProperty DropPointProperty =
          DependencyProperty.Register("DropPoint", typeof(Point), typeof(DragDropBehavior),
            new FrameworkPropertyMetadata(new Point(), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public Point DropPoint
        {
            get { return (Point)GetValue(DropPointProperty); }
            set { SetValue(DropPointProperty, value); }
        }

        #endregion
    }
}