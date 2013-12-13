using System.Windows;

namespace Glass.Basics.Presentation.Rubberband
{
    public class RubberBandAdorner : ControlAdorner
    {
        public RubberBandAdorner(UIElement adornedElement, UIElement chrome) : base(adornedElement, chrome)
        {
            Chrome.IsHitTestVisible = false;
            Width = 0;
            Height = 0;
        }

        #region Left
        public static readonly DependencyProperty LeftProperty =
          DependencyProperty.Register("Left", typeof(double), typeof(RubberBandAdorner),
            new FrameworkPropertyMetadata(0D, FrameworkPropertyMetadataOptions.AffectsArrange));

        public double Left
        {
            get { return (double)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        #endregion

        #region Top
        public static readonly DependencyProperty TopProperty =
          DependencyProperty.Register("Top", typeof(double), typeof(RubberBandAdorner),
            new FrameworkPropertyMetadata(0D, FrameworkPropertyMetadataOptions.AffectsArrange));

        public double Top
        {
            get { return (double)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        #endregion

        protected override Size MeasureOverride(Size constraint)
        {
            Chrome.Measure(constraint);
            return Chrome.DesiredSize;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var arrangeSize = new Size(Width, Height);

            Chrome.Arrange(new Rect(new Point(Left, Top), arrangeSize));
            return Chrome.RenderSize;
        }
    }
}