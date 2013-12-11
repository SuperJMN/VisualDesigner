using System.Windows;
using System.Windows.Controls;
using Design.Interfaces;

namespace Glass.Design
{

    public class SizingControl : Control
    {

        static SizingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(SizingControl), new FrameworkPropertyMetadata(typeof(SizingControl)));
        }

        #region CanvasItem

        public static readonly DependencyProperty CanvasItemProperty =
            DependencyProperty.Register("CanvasItem", typeof(ICanvasItem), typeof(SizingControl),
                new FrameworkPropertyMetadata((ICanvasItem)null,
                    new PropertyChangedCallback(OnCanvasItemChanged)));

        public ICanvasItem CanvasItem
        {
            get { return (ICanvasItem)GetValue(CanvasItemProperty); }
            set { SetValue(CanvasItemProperty, value); }
        }

        private static void OnCanvasItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (SizingControl)d;
            var oldCanvasItem = (ICanvasItem)e.OldValue;
            var newCanvasItem = target.CanvasItem;
            target.OnCanvasItemChanged(oldCanvasItem, newCanvasItem);
        }

        protected virtual void OnCanvasItemChanged(ICanvasItem oldCanvasItem, ICanvasItem newCanvasItem)
        {
            //if (newCanvasItem != null)
            //{
            //    AspectRatioKeeper = new AspectRatioKeeper(newCanvasItem);
            //}
            //else
            //{
            //    AspectRatioKeeper.Dispose();
            //}
        }

        #endregion

        private AspectRatioKeeper AspectRatioKeeper { get; set; }
    }
}
