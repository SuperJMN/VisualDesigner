using System.ComponentModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Glass.Basics.Behaviors
{
    public class SelectionChangeSupervisingBehavior : Behavior<Selector>
    {
        protected override void OnAttached()
        {
            AssociatedObject.IsSynchronizedWithCurrentItem = true;

            if (AssociatedObject.IsLoaded)
            {
                AttachToItems();
            }
            else
            {
                AssociatedObject.Loaded += OnItemsOnCurrentChanging;
            }
        }

        private void AttachToItems()
        {
            AssociatedObject.Items.CurrentChanging += ItemsOnCurrentChanging;
        }

        private void ItemsOnCurrentChanging(object sender, CurrentChangingEventArgs currentChangingEventArgs)
        {
            if (AllowSelectionChange)
            {
                return;
            }

            if (currentChangingEventArgs.IsCancelable)
            {
                var item = ((ICollectionView)sender).CurrentItem;
                currentChangingEventArgs.Cancel = true;
                AssociatedObject.SelectedItem = item;

                ExecuteCommandIfSet();
            }
        }

        private void OnItemsOnCurrentChanging(object sender, RoutedEventArgs routedEventArgs)
        {
            AttachToItems();
        }

        private void ExecuteCommandIfSet()
        {

            if (ChangeAttemptCommand != null)
            {
                if (ChangeAttemptCommand.CanExecute(ChangeAttemptCommandParameter))
                {
                    ChangeAttemptCommand.Execute(ChangeAttemptCommandParameter);
                }
            }
        }

        #region ChangeAttemptCommand
        public static readonly DependencyProperty ChangeAttemptCommandProperty =
            DependencyProperty.Register("ChangeAttemptCommand", typeof(ICommand), typeof(SelectionChangeSupervisingBehavior),
                new FrameworkPropertyMetadata((ICommand)null));

        public ICommand ChangeAttemptCommand
        {
            get { return (ICommand)GetValue(ChangeAttemptCommandProperty); }
            set { SetValue(ChangeAttemptCommandProperty, value); }
        }

        #endregion

        #region ChangeAttemptCommandParameter
        public static readonly DependencyProperty ChangeAttemptCommandParameterProperty =
            DependencyProperty.Register("ChangeAttemptCommandParameter", typeof(object), typeof(SelectionChangeSupervisingBehavior),
                new FrameworkPropertyMetadata((object)null));

        public object ChangeAttemptCommandParameter
        {
            get { return GetValue(ChangeAttemptCommandParameterProperty); }
            set { SetValue(ChangeAttemptCommandParameterProperty, value); }
        }

        #endregion


        #region AllowSelectionChange
        public static readonly DependencyProperty AllowSelectionChangeProperty =
            DependencyProperty.Register("AllowSelectionChange", typeof(bool), typeof(SelectionChangeSupervisingBehavior),
                new FrameworkPropertyMetadata(true));

        public bool AllowSelectionChange
        {
            get { return (bool)GetValue(AllowSelectionChangeProperty); }
            set { SetValue(AllowSelectionChangeProperty, value); }
        }

        #endregion
    }
}
