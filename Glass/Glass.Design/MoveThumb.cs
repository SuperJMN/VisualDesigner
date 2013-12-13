using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using Design.Interfaces;
using Glass.Basics.Extensions;

namespace Glass.Design
{
    public sealed class MoveThumb : Thumb, IMoveThumb
    {
        public MoveThumb()
        {
            Cursor = Cursors.SizeAll;
            DragDelta += OnDragDelta;
        }

        private void OnDragDelta(object sender, DragDeltaEventArgs dragDeltaEventArgs)
        {
            var deltaEventArgs = new DeltaEventArgs(dragDeltaEventArgs.HorizontalChange, dragDeltaEventArgs.VerticalChange);
            OnMoveDelta(deltaEventArgs);
            CanvasItem.Left += deltaEventArgs.HorizontalChange;
            CanvasItem.Top += deltaEventArgs.VerticalChange;
        }

        public double Left
        {
            get
            {
                var rect = this.GetRectRelativeToParent();
                return rect.Left;
            }
            set
            {
                throw new InvalidOperationException();
            }
        }

        public double Top
        {
            get
            {
                var rect = this.GetRectRelativeToParent();
                return rect.Top;
            }
            set
            {
                throw new InvalidOperationException();
            }
        }

        #region CanvasItem
        public static readonly DependencyProperty CanvasItemProperty =
            DependencyProperty.Register("CanvasItem", typeof(ICanvasItem), typeof(MoveThumb),
                new FrameworkPropertyMetadata((ICanvasItem)null));

        public ICanvasItem CanvasItem
        {
            get { return (ICanvasItem)GetValue(CanvasItemProperty); }
            set { SetValue(CanvasItemProperty, value); }
        }

        public event DeltaMoveEventHandler MoveDelta;

        private void OnMoveDelta(DeltaEventArgs args)
        {
            var handler = MoveDelta;
            if (handler != null) handler(this, args);
        }

        #endregion
    }
}