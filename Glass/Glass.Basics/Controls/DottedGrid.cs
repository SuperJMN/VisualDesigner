using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Glass.Basics.Wpf.Controls
{
    public class DottedGrid : Border
    {
        private Brush brush;

        public DottedGrid()
        {
            brush = new SolidColorBrush(DotColor);
        }

        #region GridWidth
        public static readonly DependencyProperty GridWidthProperty =
          DependencyProperty.Register("GridWidth", typeof(double), typeof(DottedGrid),
            new FrameworkPropertyMetadata((double)32D, FrameworkPropertyMetadataOptions.AffectsRender));

        public double GridWidth
        {
            get { return (double)GetValue(GridWidthProperty); }
            set { SetValue(GridWidthProperty, value); }
        }

        #endregion

        #region GridHeight
        public static readonly DependencyProperty GridHeightProperty =
          DependencyProperty.Register("GridHeight", typeof(double), typeof(DottedGrid),
            new FrameworkPropertyMetadata(32D, FrameworkPropertyMetadataOptions.AffectsRender));

        public double GridHeight
        {
            get { return (double)GetValue(GridHeightProperty); }
            set { SetValue(GridHeightProperty, value); }
        }

        #endregion


        protected override void OnRender(DrawingContext dc)
        {
            base.OnRender(dc);

            for (double y = 0; y < this.ActualHeight; y += GridHeight)
            {
                for (double x = 0; x < this.ActualWidth; x += GridWidth)
                {
                    dc.DrawRectangle(brush, null, new Rect(x, y, 1, 1));
                }
            }
        }


        #region DotColor

        public static readonly DependencyProperty DotColorProperty =
            DependencyProperty.Register("DotColor", typeof(Color), typeof(DottedGrid),
                new FrameworkPropertyMetadata(Colors.Black, FrameworkPropertyMetadataOptions.AffectsRender,
                    OnDotColorChanged));

        public Color DotColor
        {
            get { return (Color)GetValue(DotColorProperty); }
            set { SetValue(DotColorProperty, value); }
        }

        private static void OnDotColorChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (DottedGrid)d;
            var oldDotColor = (Color)e.OldValue;
            var newDotColor = target.DotColor;
            target.OnDotColorChanged(oldDotColor, newDotColor);
        }

        protected virtual void OnDotColorChanged(Color oldDotColor, Color newDotColor)
        {
            brush = new SolidColorBrush(newDotColor);
        }

        #endregion





    }
}