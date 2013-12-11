using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Glass.Basics.Controls.EditableTextBlock
{
    public class EditableTextBlock : Control
    {
        private bool previouslyClicked;
        private string textOldValue;
        private bool generatedContainerFocusableOldValue;

        static EditableTextBlock()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(EditableTextBlock), new FrameworkPropertyMetadata(typeof(EditableTextBlock)));
        }

        public EditableTextBlock()
        {
            PreviewMouseLeftButtonDown += OnPreviewMouseLeftButtonDown;
            KeyDown += OnKeyDown;
        }

        private void TextBoxOnLostFocus(object sender, RoutedEventArgs routedEventArgs)
        {
            TurnOffEditMode();
            previouslyClicked = false;
        }

        private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (keyEventArgs.Key == Key.Enter || keyEventArgs.Key == Key.Escape)
            {
                if (keyEventArgs.Key == Key.Escape)
                {
                    Text = textOldValue;                    
                }
                TryToExitEditMode();
            }
        }

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if (previouslyClicked || IsEditing)
            {
                TurnOnEditMode();
            }
            else
            {
                previouslyClicked = true;
            }
        }

        private void TurnOnEditMode()
        {
            textOldValue = this.Text;
            IsEditing = true;
            generatedContainerFocusableOldValue = IsGeneratedContainerFocusable;
            IsGeneratedContainerFocusable = false;
            textBox.Focus();
        }

        private bool IsGeneratedContainerFocusable
        {
            get
            {
                if (generatedContainer != null)
                {
                    return generatedContainer.Focusable;
                }
                return false;
            }
            set
            {
                if (generatedContainer != null)
                {
                    generatedContainer.Focusable = value;
                }
            }
        }


        protected override void OnVisualParentChanged(DependencyObject oldParent)
        {
            base.OnVisualParentChanged(oldParent);

            parentWindow = this.TryFindParent<Window>();

            if (IsChildFromItemsControl())
            {
                var parentItemsControl = GetParentItemsControl();
                generatedContainer = (UIElement)GetGeneratedParentElement(parentItemsControl);
            }

            parentWindow.PreviewMouseDown += ParentWindowOnPreviewMouseDown;
        }

        private DependencyObject GetGeneratedParentElement(ItemsControl itemsControl)
        {
            return itemsControl.ContainerFromElement(this);
        }

        private ItemsControl GetParentItemsControl()
        {
            return this.TryFindParent<ItemsControl>();
        }

        private bool IsChildFromItemsControl()
        {
            return this.TryFindParent<ItemsControl>() != null;
        }

        private void ParentWindowOnPreviewMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            var position = mouseButtonEventArgs.GetPosition(this);

            // Si se mea fuera del tiesto (es decir, pulsa con el ratón fuera del control
            if (position.X < 0 || position.Y < 0 || position.X > ActualWidth || position.Y > ActualHeight)
            {
                TryToExitEditMode();
            }
        }

        private void TryToExitEditMode()
        {
            var validationsPassed = AreValidationsPassed();
            if (validationsPassed)
            {
                TurnOffEditMode();
                previouslyClicked = true;
            }
        }

        private bool AreValidationsPassed()
        {
            this.UpdateSourcesOfAllBindings();

            var bindingsValidate = !Validation.GetHasError(this);
            return bindingsValidate;
        }

        private void TurnOffEditMode()
        {
            IsEditing = false;
            IsGeneratedContainerFocusable = this.generatedContainerFocusableOldValue;
        }

        #region IsEditing
        public static readonly DependencyProperty IsEditingProperty =
          DependencyProperty.Register("IsEditing", typeof(bool), typeof(EditableTextBlock),
            new FrameworkPropertyMetadata(false));

        public bool IsEditing
        {
            get { return (bool)GetValue(IsEditingProperty); }
            set { SetValue(IsEditingProperty, value); }
        }

        #endregion

        #region Text
        public static readonly DependencyProperty TextProperty =
          DependencyProperty.Register("Text", typeof(string), typeof(EditableTextBlock),
            new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        private Window parentWindow;
        private TextBox textBox;
        private UIElement generatedContainer;

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            textBox = (TextBox)Template.FindName("PART_TextBox", this);
            textBox.LostFocus += TextBoxOnLostFocus;
            textBox.PreviewLostKeyboardFocus += TextBoxOnPreviewLostKeyboardFocus;
        }

        private void TextBoxOnPreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs keyboardFocusChangedEventArgs)
        {
            var expr = BindingOperations.GetBindingExpression(textBox, TextBox.TextProperty);
            expr.UpdateSource();

        }
    }

}
