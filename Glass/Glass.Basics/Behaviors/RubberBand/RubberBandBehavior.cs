using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interactivity;
using Glass.Basics.Wpf.Core;

namespace Glass.Basics.Wpf.Behaviors.RubberBand
{
    public class RubberBandBehavior : Behavior<FrameworkElement>
    {
        private RubberBandAdorner adorner;

        protected override void OnAttached()
        {
            AssociatedObject.Loaded += AssociatedObjectOnLoaded;
            base.OnAttached();
        }

        private void AssociatedObjectOnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            adorner = new RubberBandAdorner(AssociatedObject);

            adorner.DragStarted += (s, args) => OnDragStarted();
            adorner.DragMove += (o, args) => OnDragMove(args);
            adorner.DragFinished += (s, args) => OnDragFinished(args);

            var adornerLayer = AdornerLayer.GetAdornerLayer(AssociatedObject);
            if (adornerLayer != null)
            {
                adornerLayer.Add(adorner);
            }
        }

        protected override void OnDetaching()
        {
            AssociatedObject.Loaded -= AssociatedObjectOnLoaded;
            base.OnDetaching();
        }

        private void OnDragMove(EventArgs<Rect> e)
        {

            if (!IsEnabled)
                return;

            if (DragMove != null)
                DragMove(this, e);
        }

        private void OnDragStarted()
        {
            if (!IsEnabled)
                return;

            if (DragStarted != null)
                DragStarted(this, EventArgs.Empty);
        }

        private void OnDragFinished(EventArgs<Rect> e)
        {

            if (!IsEnabled)
                return;

            SelectionRectangle = e.Data;

            if (DragCompletedCommand != null)
            {
                DragCompletedCommand.Execute(DragCompletedCommandParameter);
            }

            if (DragFinished != null)
                DragFinished(this, e);
        }

        #region IsEnabled

        public static readonly DependencyProperty IsEnabledProperty =
            DependencyProperty.Register("IsEnabled", typeof(bool), typeof(RubberBandBehavior),
                new FrameworkPropertyMetadata(true,
                    OnIsEnabledChanged));

        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        private static void OnIsEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (RubberBandBehavior)d;
            var newIsEnabled = target.IsEnabled;
            target.OnIsEnabledChanged(newIsEnabled);
        }

        private void OnIsEnabledChanged(bool newIsEnabled)
        {
            if (adorner == null)
                return;

            adorner.IsEnabled = newIsEnabled;
        }

        #endregion

        #region DragCompletedCommand
        public static readonly DependencyProperty DragCompletedCommandProperty =
          DependencyProperty.Register("DragCompletedCommand", typeof(ICommand), typeof(RubberBandBehavior),
            new FrameworkPropertyMetadata(null));

        public ICommand DragCompletedCommand
        {
            get { return (ICommand)GetValue(DragCompletedCommandProperty); }
            set { SetValue(DragCompletedCommandProperty, value); }
        }

        #endregion

        #region DragCompletedCommandParameter
        public static readonly DependencyProperty DragCompletedCommandParameterProperty =
          DependencyProperty.Register("DragCompletedCommandParameter", typeof(object), typeof(RubberBandBehavior),
            new FrameworkPropertyMetadata(null));

        public object DragCompletedCommandParameter
        {
            get { return GetValue(DragCompletedCommandParameterProperty); }
            set { SetValue(DragCompletedCommandParameterProperty, value); }
        }

        #endregion

        #region SelectionRectangle
        public static readonly DependencyProperty SelectionRectangleProperty =
          DependencyProperty.Register("SelectionRectangle", typeof(Rect), typeof(RubberBandBehavior),
            new FrameworkPropertyMetadata(Rect.Empty));

        public Rect SelectionRectangle
        {
            get { return (Rect)GetValue(SelectionRectangleProperty); }
            set { SetValue(SelectionRectangleProperty, value); }
        }

        #endregion

        public event EventHandler<EventArgs<Rect>> DragFinished;
        public event EventHandler<EventArgs<Rect>> DragMove;
        public event EventHandler DragStarted;
    }
}