using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Glass.Basics.Wpf.Behaviors {
    public class KeepFocusOnErrorBehavior : Behavior<UIElement>
    {
        private IList<UIElement> elementsToObserveFocusLoss;

        protected override void OnAttached()
        {
            elementsToObserveFocusLoss = AssociatedObject.GetDescendants<UIElement>();
            elementsToObserveFocusLoss.Add(AssociatedObject);

            foreach (var uiElement in elementsToObserveFocusLoss)
            {
                uiElement.PreviewLostKeyboardFocus += AssociatedObjectOnPreviewLostKeyboardFocus;
                
            }
           
            base.OnAttached();
        }

        private void AssociatedObjectOnPreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs keyboardFocusChangedEventArgs)
        {
            var elementToLoseFocus = (UIElement) sender;

            var elementToReceiveFocus = elementToLoseFocus.PredictFocus(FocusNavigationDirection.Down);

            if (elementsToObserveFocusLoss.Contains((UIElement) elementToReceiveFocus))
                return;

            AssociatedObject.UpdateSourcesOfAllBindings();

            var value = Validation.GetHasError(AssociatedObject);
            if (value)
            {
                keyboardFocusChangedEventArgs.Handled = true;
                ExecuteErrorCommandIfCanExecute();
            }
        }

        private void ExecuteErrorCommandIfCanExecute()
        {
            if (ErrorCommand != null)
            {
                if (ErrorCommand.CanExecute(ErrorCommandParameter))
                    ErrorCommand.Execute(ErrorCommandParameter);
            }
        }

        #region ShowErrorMessageCommand
        public static readonly DependencyProperty ErrorCommandProperty =
          DependencyProperty.Register("ErrorCommand", typeof(ICommand), typeof(KeepFocusOnErrorBehavior),
            new FrameworkPropertyMetadata((ICommand)null));

        public ICommand ErrorCommand
        {
            get { return (ICommand)GetValue(ErrorCommandProperty); }
            set { SetValue(ErrorCommandProperty, value); }
        }

        #endregion

        #region ErrorCommandParameter
        public static readonly DependencyProperty ErrorCommandParameterProperty =
          DependencyProperty.Register("ErrorCommandParameter", typeof(object), typeof(KeepFocusOnErrorBehavior),
            new FrameworkPropertyMetadata((object)null));

        public object ErrorCommandParameter
        {
            get { return GetValue(ErrorCommandParameterProperty); }
            set { SetValue(ErrorCommandParameterProperty, value); }
        }

        #endregion

        protected override void OnDetaching()
        {
            base.OnDetaching();

            foreach (var uiElement in elementsToObserveFocusLoss)
            {
                uiElement.PreviewLostKeyboardFocus -= AssociatedObjectOnPreviewLostKeyboardFocus;
            }
        }
    }
}
