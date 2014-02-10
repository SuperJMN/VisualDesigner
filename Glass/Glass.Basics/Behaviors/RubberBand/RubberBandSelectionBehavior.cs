using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using System.Windows.Media;
using Glass.Basics.Wpf.Core;

namespace Glass.Basics.Wpf.Behaviors.RubberBand {
    public class RubberBandSelectionBehavior : Behavior<ItemsControl> {
        private RubberBandBehavior behavior;
        private Dictionary<ISelectable, bool> originalStates;

        protected override void OnAttached() {
            behavior = new RubberBandBehavior();
            originalStates = new Dictionary<ISelectable, bool>();

            behavior.DragFinished += BehaviorOnDragFinished;
            behavior.DragStarted += BehaviorOnDragStarted;
            behavior.DragMove += BehaviorOnDragMove;

            behavior.Attach(AssociatedObject);
            base.OnAttached();
        }

        private void BehaviorOnDragStarted(object sender, EventArgs eventArgs) {

            containers = GetSelectableContainers();

            if (AutomaticSelectionMode)
                SelectionMode = GetAutomaticSelectionMode();


            if (SelectionMode == SelectionMode.Invert || SelectionMode == SelectionMode.Add) {
                SaveSelectionState();
            }
        }

        private void BehaviorOnDragMove(object sender, EventArgs<Rect> e)
        {
            containersIntoRubberBand = GetContainersWithinRubberBand(e.Data);

            if (SelectionMode == SelectionMode.Add || SelectionMode == SelectionMode.Direct) {
                if (SelectionMode == SelectionMode.Direct) {
                    Unselect(containers);
                } else {
                    RestoreOriginalStates();
                }

                foreach (var container in containersIntoRubberBand) {
                    if (!container.IsSelected) {
                        container.IsSelected = true;
                    }
                }
            } else {
                RestoreOriginalStates();
                foreach (var container in containersIntoRubberBand) {
                    var selectionValue = false;

                    if (originalStates.ContainsKey(container)) {
                        selectionValue = !originalStates[container];
                    }

                    container.IsSelected = selectionValue;
                }
            }
        }

        private void BehaviorOnDragFinished(object sender, EventArgs<Rect> rect) {
            var contained = GetContainersWithinRubberBand(rect.Data);
            if (!contained.Any()) {
                Unselect(containers);
            }
        }

        private void SaveSelectionState() {
            originalStates.Clear();

            foreach (var selectable in containers) {
                originalStates.Add(selectable, selectable.IsSelected);
            }
        }

        private void RestoreOriginalStates() {
            foreach (var originalState in originalStates) {
                var item = originalState.Key;
                item.IsSelected = originalState.Value;
            }
        }

        private static void Unselect(IEnumerable<ISelectable> containers) {
            if (containers == null)
                return;

            foreach (var selectable in containers) {
                selectable.IsSelected = false;
            }
        }

        private IEnumerable<ISelectable> GetSelectableContainers()
        {
            var items = AssociatedObject.Items;
            var itemContainerGenerator = AssociatedObject.ItemContainerGenerator;

            var containers = new List<DependencyObject>();
            foreach (var item in items)
            {
                var dpo = itemContainerGenerator.ContainerFromItem(item);
                containers.Add(dpo);
            }

            return containers.OfType<ISelectable>();
        }

        private IEnumerable<ISelectable> GetContainersWithinRubberBand(Rect rect)
        {
            var list = new List<ISelectable>();
            var containers = GetSelectableContainers();
            foreach (DependencyObject container in containers)
            {                
                var itemBounds = VisualTreeHelper.GetDescendantBounds((Visual)container);
                var containerOffset = VisualTreeHelper.GetOffset((Visual)container);

                itemBounds.Offset(containerOffset);

                if (rect.Contains(itemBounds))
                    list.Add((ISelectable)container);
            }
            return list;
        }

        protected override void OnDetaching() {
            behavior.Detach();
            base.OnDetaching();
        }

        #region SelectionMode

        public static readonly DependencyProperty SelectionModeProperty = DependencyProperty.Register("SelectionMode", typeof(SelectionMode), typeof(RubberBandSelectionBehavior), new FrameworkPropertyMetadata(SelectionMode.Direct));

        private IEnumerable<ISelectable> containersIntoRubberBand;

        private IEnumerable<ISelectable> containers;

        public SelectionMode SelectionMode {
            // ReSharper disable MemberCanBePrivate.Global
            get { return (SelectionMode)GetValue(SelectionModeProperty); }
            // ReSharper restore MemberCanBePrivate.Global
            set { SetValue(SelectionModeProperty, value); }
        }

        #endregion

        #region AutomaticSelectionMode


        public static readonly DependencyProperty AutomaticSelectionModeProperty =
            DependencyProperty.Register("AutomaticSelectionMode", typeof(bool), typeof(RubberBandSelectionBehavior),
                new FrameworkPropertyMetadata(true));

        public bool AutomaticSelectionMode {
            get { return (bool)GetValue(AutomaticSelectionModeProperty); }
            set { SetValue(AutomaticSelectionModeProperty, value); }
        }

        #endregion

        #region IsEnabled

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register("IsEnabled", typeof(bool), typeof(RubberBandSelectionBehavior),
                new FrameworkPropertyMetadata(true,
                    OnIsEnabledChanged));

        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (RubberBandSelectionBehavior)d;
            var oldIsEnabled = (bool)e.OldValue;
            var newIsEnabled = target.IsEnabled;
            target.OnIsEnabledChanged(oldIsEnabled, newIsEnabled);
        }

        protected virtual void OnIsEnabledChanged(bool oldIsEnabled, bool newIsEnabled)
        {
            behavior.IsEnabled = newIsEnabled;
        }

        #endregion



        

        private static SelectionMode GetAutomaticSelectionMode() {
            if (Keyboard.IsKeyDown(Key.LeftCtrl)) {
                return SelectionMode.Invert;
            }
            if (Keyboard.IsKeyDown(Key.LeftShift)) {
                return SelectionMode.Add;
            }
            return SelectionMode.Direct;
        }
    }
}