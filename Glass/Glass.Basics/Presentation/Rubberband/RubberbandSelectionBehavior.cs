using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using Glass.Basics.Extensions;

namespace Glass.Basics.Presentation.Rubberband
{
    public class RubberbandSelectionBehavior : RubberbandBehavior
    {
        private Panel panel;
        private ItemsControl itemsControl;

        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject is Panel)
            {
                panel = AssociatedObject as Panel;
            }
            else if (AssociatedObject is ItemsControl)
            {
                AttachToItemsPanel();
            }
            else
            {
                throw new ArgumentException();                
            }
        }

        private void AttachToItemsPanel()
        {
            itemsControl = (ItemsControl)AssociatedObject;
            itemsControl.ItemContainerGenerator.StatusChanged += ItemContainerGeneratorOnStatusChanged;
        }

        private void ItemContainerGeneratorOnStatusChanged(object sender, EventArgs e)
        {
            if (itemsControl.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated && panel == null)
            {
                panel = itemsControl.FindItemsPanel();
            }
        }

        protected override void OnDragCompleted(Rect rect)
        {
            base.OnDragCompleted(rect);

            if (!IsEnabled)
                return;

            SelectedItems = panel.GetChildrenWithBounds(rect);

            if (Command != null)
            {
                if (Command.CanExecute(CommandParamenter))
                {
                    Command.Execute(CommandParamenter);
                }
            }
        }

        #region SelectedItems
        public static readonly DependencyProperty SelectedItemsProperty =
            DependencyProperty.Register("SelectedItems", typeof(IEnumerable), typeof(RubberbandSelectionBehavior),
                new FrameworkPropertyMetadata(new List<Visual>()));

        public IEnumerable SelectedItems
        {
            get { return (IEnumerable)GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }

        #endregion

        #region Command
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(RubberbandSelectionBehavior),
                new FrameworkPropertyMetadata((ICommand)null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        #endregion

        #region CommandParamenter
        public static readonly DependencyProperty CommandParamenterProperty =
            DependencyProperty.Register("CommandParamenter", typeof(object), typeof(RubberbandSelectionBehavior),
                new FrameworkPropertyMetadata((object)null));

        public object CommandParamenter
        {
            get { return GetValue(CommandParamenterProperty); }
            set { SetValue(CommandParamenterProperty, value); }
        }

        #endregion

        #region IsEnabled
        public static readonly DependencyProperty IsEnabledProperty =
          DependencyProperty.Register("IsEnabled", typeof(bool), typeof(RubberbandSelectionBehavior),
            new FrameworkPropertyMetadata(true));

        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        #endregion
    }
}