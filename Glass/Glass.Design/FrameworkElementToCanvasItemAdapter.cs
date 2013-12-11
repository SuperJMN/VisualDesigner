using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Design.Interfaces;

namespace Glass.Design
{
    [DefaultProperty("Content")]
    public class FrameworkElementToCanvasItemAdapter : ContentControl, ICanvasItem
    {
        public FrameworkElementToCanvasItemAdapter()
        {
            base.SizeChanged += OnSizeChanged;
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            if (sizeChangedEventArgs.HeightChanged)
            {
                OnHeightChanged();
            }
            if (sizeChangedEventArgs.WidthChanged)
            {
                OnWidthChanged();
            }
        }

        #region Left

        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register("Left", typeof(double), typeof(FrameworkElementToCanvasItemAdapter),
                new FrameworkPropertyMetadata(double.NaN, OnLeftChanged));

        public double Left
        {
            get { return (double)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        private static void OnLeftChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (FrameworkElementToCanvasItemAdapter)d;
            var oldLeft = (double)e.OldValue;
            var newLeft = target.Left;
            target.OnLeftChanged(oldLeft, newLeft);
        }

        protected virtual void OnLeftChanged(double oldLeft, double newLeft)
        {
            Canvas.SetLeft(this, newLeft);
            OnLocationChanged();
        }

        #endregion

        #region Top

        public static readonly DependencyProperty TopProperty =
            DependencyProperty.Register("Top", typeof(double), typeof(FrameworkElementToCanvasItemAdapter),
                new FrameworkPropertyMetadata(double.NaN, OnTopChanged));

        public double Top
        {
            get { return (double)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        public event EventHandler LocationChanged;

        private static void OnTopChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (FrameworkElementToCanvasItemAdapter)d;
            var oldTop = (double)e.OldValue;
            var newTop = target.Top;
            target.OnTopChanged(oldTop, newTop);
        }

        protected virtual void OnTopChanged(double oldTop, double newTop)
        {
            Canvas.SetTop(this, newTop);
            OnLocationChanged();
        }

        #endregion

        protected virtual void OnLocationChanged()
        {
            var handler = LocationChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler HeightChanged;

        protected virtual void OnHeightChanged()
        {
            var handler = HeightChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        public event EventHandler WidthChanged;

        protected virtual void OnWidthChanged()
        {
            var handler = WidthChanged;
            if (handler != null) handler(this, EventArgs.Empty);
        }
    }
}