using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Glass.Basics.Wpf.Extensions;

namespace Glass.Basics.Wpf.Presentation.Rubberband
{
    public class RubberbandAutoSelectionBehavior: RubberbandBehavior
    {
        private Panel panel;
        private MultiSelector multiSelector;

        protected override void OnAttached()
        {
            base.OnAttached();

            multiSelector = AssociatedObject as MultiSelector;

            if (multiSelector == null)
            {
                throw new InvalidOperationException();
            }

            multiSelector.ItemContainerGenerator.StatusChanged += ItemContainerGeneratorOnStatusChanged;
        }

        private void ItemContainerGeneratorOnStatusChanged(object sender, EventArgs eventArgs)
        {
            if (multiSelector.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated && panel == null)
            {                
                AttachToPanel();
            }
        }

        private void AttachToPanel()
        {
            panel = multiSelector.FindItemsPanel();
        }

        protected override void OnDragCompleted(Rect rect)
        {
            base.OnDragCompleted(rect);

            if (!IsEnabled)
                return;

            
            var selectedItems = panel.GetChildrenWithBounds(rect);
            

            foreach (var selectedContainer in selectedItems)
            {
                var item = multiSelector.ItemContainerGenerator.ItemFromContainer(selectedContainer);
                multiSelector.SelectedItems.Add(item);
            }           
        }

        #region IsEnabled
        public static readonly DependencyProperty IsEnabledProperty =
          DependencyProperty.Register("IsEnabled", typeof(bool), typeof(RubberbandAutoSelectionBehavior),
            new FrameworkPropertyMetadata(true));

        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        #endregion


    }
}