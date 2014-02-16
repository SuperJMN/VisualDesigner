using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using Glass.Basics.Wpf.Core;

namespace Glass.Basics.Wpf.Behaviors.RubberBand
{
    public abstract class RubberBandSelectionBehavior<T> : Behavior<T> where T : ItemsControl
    {
        private RubberBandBehavior behavior;
        private HashSet<object> selectedItemsBackup;

        protected override void OnAttached()
        {
            behavior = new RubberBandBehavior();
            SelectedItemsBackup = new HashSet<object>();

            behavior.DragFinished += BehaviorOnDragFinished;
            behavior.DragStarted += BehaviorOnDragStarted;
            behavior.DragMove += BehaviorOnDragMove;

            behavior.Attach(AssociatedObject);
            base.OnAttached();
        }

        private void BehaviorOnDragStarted(object sender, EventArgs eventArgs)
        {

            containers = GetContainers();

            if (AutomaticSelectionMode)
                SelectionMode = GetAutomaticSelectionMode();


            if (SelectionMode == SelectionMode.Invert || SelectionMode == SelectionMode.Add)
            {
                SaveSelectionState();
            }
        }

        private void BehaviorOnDragMove(object sender, EventArgs<Rect> e)
        {
            containersIntoRubberBand = GetContainersWithinRubberBand(e.Data);

            if (SelectionMode == SelectionMode.Add || SelectionMode == SelectionMode.Direct)
            {
                if (SelectionMode == SelectionMode.Direct)
                {
                    Unselect(containers);
                }
                else
                {
                    RestoreOriginalStates();
                }

                foreach (var container in containersIntoRubberBand)
                {
                    if (!GetSelectedState(container))
                    {
                        SetSelectedState(container, true);
                    }
                }
            }
            else
            {
                RestoreOriginalStates();             
            }
        }

        private void BehaviorOnDragFinished(object sender, EventArgs<Rect> rect)
        {
            var contained = GetContainersWithinRubberBand(rect.Data);
            if (!contained.Any())
            {
                Unselect(containers);
            }              
        }

        protected abstract void SetSelectedState(DependencyObject container, bool value);
        protected abstract bool GetSelectedState(DependencyObject container);

        protected abstract void SaveSelectionState();

        private void RestoreOriginalStates()
        {
            //foreach (var originalState in selectedItemsBackup)
            //{
            //    var item = originalState.Key;
            //    item.IsSelected = originalState.Value;
            //}
        }

        protected abstract void Unselect(IEnumerable<DependencyObject> containers);

        private IEnumerable<DependencyObject> GetContainers()
        {
            var items = AssociatedObject.Items;
            var itemContainerGenerator = AssociatedObject.ItemContainerGenerator;

            var containers = new List<DependencyObject>();
            foreach (var item in items)
            {
                var dpo = itemContainerGenerator.ContainerFromItem(item);
                containers.Add(dpo);
            }

            return containers;
        }

        private IEnumerable<DependencyObject> GetContainersWithinRubberBand(Rect rect)
        {
            var list = new List<DependencyObject>();
            var containers = GetContainers();
            foreach (var container in containers)
            {
                var itemBounds = VisualTreeHelper.GetDescendantBounds((Visual)container);
                var containerOffset = VisualTreeHelper.GetOffset((Visual)container);

                itemBounds.Offset(containerOffset);

                if (rect.Contains(itemBounds))
                    list.Add(container);
            }
            return list;
        }

        protected override void OnDetaching()
        {
            behavior.Detach();
            base.OnDetaching();
        }

        #region SelectionMode

        public static readonly DependencyProperty SelectionModeProperty = DependencyProperty.Register("SelectionMode", typeof(SelectionMode), typeof(RubberBandSelectionBehavior<>), new FrameworkPropertyMetadata(SelectionMode.Direct));

        private IEnumerable<DependencyObject> containersIntoRubberBand;

        private IEnumerable<DependencyObject> containers;

        public SelectionMode SelectionMode
        {
            // ReSharper disable MemberCanBePrivate.Global
            get { return (SelectionMode)GetValue(SelectionModeProperty); }
            // ReSharper restore MemberCanBePrivate.Global
            set { SetValue(SelectionModeProperty, value); }
        }

        #endregion

        #region AutomaticSelectionMode


        public static readonly DependencyProperty AutomaticSelectionModeProperty =
            DependencyProperty.Register("AutomaticSelectionMode", typeof(bool), typeof(RubberBandSelectionBehavior<T>),
                new FrameworkPropertyMetadata(true));

        public bool AutomaticSelectionMode
        {
            get { return (bool)GetValue(AutomaticSelectionModeProperty); }
            set { SetValue(AutomaticSelectionModeProperty, value); }
        }

        #endregion

        #region IsEnabled

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register("IsEnabled", typeof(bool), typeof(RubberBandSelectionBehavior<T>),
                new FrameworkPropertyMetadata(true,
                    OnIsEnabledChanged));

        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        protected HashSet<object> SelectedItemsBackup
        {
            get { return selectedItemsBackup; }
            set { selectedItemsBackup = value; }
        }

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (RubberBandSelectionBehavior<T>)d;
            var oldIsEnabled = (bool)e.OldValue;
            var newIsEnabled = target.IsEnabled;
            target.OnIsEnabledChanged(oldIsEnabled, newIsEnabled);
        }

        protected virtual void OnIsEnabledChanged(bool oldIsEnabled, bool newIsEnabled)
        {
            behavior.IsEnabled = newIsEnabled;
        }

        #endregion





        private static SelectionMode GetAutomaticSelectionMode()
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                return SelectionMode.Invert;
            }
            if (Keyboard.IsKeyDown(Key.LeftShift))
            {
                return SelectionMode.Add;
            }
            return SelectionMode.Direct;
        }
    }

    public class ListBoxSelectionBehavior : RubberBandSelectionBehavior<ListBox>
    {
        protected override void SetSelectedState(DependencyObject container, bool value)
        {
            var item = (ListBoxItem) container;
            item.IsSelected = value;
        }

        protected override bool GetSelectedState(DependencyObject container)
        {
            var item = (ListBoxItem)container;
            return item.IsSelected;
        }

        protected override void SaveSelectionState()
        {
            var selection = new List<object>(AssociatedObject.SelectedItems.Cast<object>());
            SelectedItemsBackup = new HashSet<object>(selection);
        }

        protected override void Unselect(IEnumerable<DependencyObject> containers)
        {
            AssociatedObject.SelectedItems.Clear();
        }
    }
}