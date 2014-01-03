#region

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.DesignSurface;

#endregion

namespace Glass.Design.Wpf.DesignSurface
{
    [DefaultProperty("Content")]
    public sealed class DesignerItem : ContentControl, ICanvasItem
    {
        static DesignerItem()
        {

            DefaultStyleKeyProperty.OverrideMetadata(typeof(DesignerItem), new FrameworkPropertyMetadata(typeof(DesignerItem)));
        }

        public DesignerItem()
        {
            SizeChanged += OnSizeChanged;
        }

        public double Top
        {
            get { return (double)GetValue(TopProperty); }
            set { SetValue(TopProperty, value); }
        }

        public event EventHandler<LocationChangedEventArgs> LeftChanged;

        public event EventHandler<LocationChangedEventArgs> TopChanged;

        public double Right { get { return Left + Width; }  }
        public double Bottom { get { return Top + Height; } }
        public ObservableCollection<ICanvasItem> Children { get; private set; }
        public event EventHandler<SizeChangeEventArgs> HeightChanged;

        public event EventHandler<SizeChangeEventArgs> WidthChanged;

        private void OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            var sizeChangeEventArgs = new SizeChangeEventArgs(sizeChangedEventArgs.PreviousSize.Height, 
                sizeChangedEventArgs.NewSize.Height);

            if (sizeChangedEventArgs.HeightChanged)
            {
                OnHeightChanged(sizeChangeEventArgs);
            }
            if (sizeChangedEventArgs.WidthChanged)
            {
                OnWidthChanged(sizeChangeEventArgs);
            }
        }

        private void OnLeftChanged(LocationChangedEventArgs e)
        {
            var handler = LeftChanged;
            if (handler != null) handler(this, e);
        }


        public static readonly DependencyProperty TopProperty =
           DependencyProperty.Register("Top", typeof(double), typeof(DesignerItem),
               new FrameworkPropertyMetadata(double.NaN, OnTopChanged));

        private void OnTopChanged(LocationChangedEventArgs e)
        {
            var handler = TopChanged;
            if (handler != null) handler(this, e);
        }

        private static void OnTopChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (DesignerItem)d;
            var oldTop = (double)e.OldValue;
            var newTop = target.Top;
            target.OnTopChanged(oldTop, newTop);
        }

        private void OnTopChanged(double oldTop, double newTop)
        {
            Canvas.SetTop(this, newTop);
            OnTopChanged(new LocationChangedEventArgs(oldTop, newTop));
        }

        private void OnHeightChanged(SizeChangeEventArgs e)
        {
            var handler = HeightChanged;
            if (handler != null) handler(this, e);
        }

        private void OnWidthChanged(SizeChangeEventArgs e)
        {
            var handler = WidthChanged;
            if (handler != null) handler(this, e);
        }

        #region Left

        public static readonly DependencyProperty LeftProperty =
            DependencyProperty.Register("Left", typeof(double), typeof(DesignerItem),
                new FrameworkPropertyMetadata(double.NaN, OnLeftChanged));

        public double Left
        {
            get { return (double)GetValue(LeftProperty); }
            set { SetValue(LeftProperty, value); }
        }

        private static void OnLeftChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (DesignerItem)d;
            var oldLeft = (double)e.OldValue;
            var newLeft = target.Left;
            target.OnLeftChanged(oldLeft, newLeft);
        }

        private void OnLeftChanged(double oldLeft, double newLeft)
        {
            Canvas.SetLeft(this, newLeft);
            OnLeftChanged(new LocationChangedEventArgs(oldLeft, newLeft));
        }

        #endregion
    }
}