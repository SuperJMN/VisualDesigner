using System.Windows;
using System.Windows.Interactivity;

namespace Glass.Basics.Presentation.Rubberband
{
    public class RubberbandBehavior : Behavior<FrameworkElement>
    {
        private RubberBandAttacher attacher;

        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject.IsLoaded)
            {
                AttachToAssociatedObject();
            }
            else
            {
                AssociatedObject.Loaded += AssociatedObjectOnLoaded;
            }
        }

        private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            AttachToAssociatedObject();
        }

        private void AttachToAssociatedObject()
        {
            attacher = new RubberBandAttacher(AssociatedObject);
            attacher.DragCompleted += AttacherOnDragCompleted;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            attacher.DragCompleted -= AttacherOnDragCompleted;
        }

        private void AttacherOnDragCompleted(object sender, Rect rect)
        {
            SelectionRectangle = rect;
            OnDragCompleted(rect);
        }

        public event RectEventHandler DragCompleted;

        protected virtual void OnDragCompleted(Rect rect)
        {
            var handler = DragCompleted;
            if (handler != null)
            {
                handler(this, rect);
            }
        }

        #region SelectionRectangle
        public static readonly DependencyProperty SelectionRectangleProperty =
          DependencyProperty.Register("SelectionRectangle", typeof(Rect), typeof(RubberbandBehavior),
            new FrameworkPropertyMetadata(Rect.Empty));

        public Rect SelectionRectangle
        {
            get { return (Rect)GetValue(SelectionRectangleProperty); }
            set { SetValue(SelectionRectangleProperty, value); }
        }

        #endregion


    }
}