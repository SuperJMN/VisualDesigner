using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls.Primitives;
using Design.Interfaces;
using Glass.Basics.Extensions;
using Glass.Design.Converters;

namespace Glass.Design
{
    public class ResizeThumb : Thumb, IResizeThumb
    {
        private readonly IThumbCursorConverter cursorConverter;

        public ResizeThumb()
        {
            Loaded += (sender, args) => SetCursor();
            DragDelta += OnDragDelta;

            cursorConverter = ThumbCursorConverterFactory.Create(this);
        }

        private void SetCursor()
        {
            var myBounds = this.GetRectRelativeToParent();
            
            Cursor = cursorConverter.GetCursor(myBounds, CanvasItem.ToRect());
        }


        private void OnDragDelta(object sender, DragDeltaEventArgs dragDeltaEventArgs)
        {
            DeltaMove(dragDeltaEventArgs.HorizontalChange, dragDeltaEventArgs.VerticalChange);
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

        public event EventHandler<LocationChangedEventArgs> LeftChanged;
        public event EventHandler<LocationChangedEventArgs> TopChanged;

        public void SetTopCoercionMethod(CoercionHandler handler)
        {
            
        }

        public void SetLeftCoercionMethod(CoercionHandler handler)
        {
            
        }

        #region CanvasItem
        public static readonly DependencyProperty CanvasItemProperty =
          DependencyProperty.Register("CanvasItem", typeof(ICanvasItem), typeof(ResizeThumb),
            new FrameworkPropertyMetadata((ICanvasItem)null));

        public ICanvasItem CanvasItem
        {
            get { return (ICanvasItem)GetValue(CanvasItemProperty); }
            set { SetValue(CanvasItemProperty, value); }
        }

        #endregion



        public void DeltaMove(double horizontalChange, double verticalChange)
        {
            var proportionalResizer = new ProportionalResizer(CanvasItem);
            proportionalResizer.HookPoint = GetHookPointFromMyPosition();

            if (!AllowHorizontalResize)
            {
                horizontalChange = 0;
            }
            if (!AllowVerticalResize)
            {
                verticalChange = 0;
            }

            proportionalResizer.DeltaResize(new Vector(horizontalChange, verticalChange));
        }

        #region AllowVerticalResize
        public static readonly DependencyProperty AllowVerticalResizeProperty =
          DependencyProperty.Register("AllowVerticalResize", typeof(bool), typeof(ResizeThumb),
            new FrameworkPropertyMetadata(true));

        public bool AllowVerticalResize
        {
            get { return (bool)GetValue(AllowVerticalResizeProperty); }
            set { SetValue(AllowVerticalResizeProperty, value); }
        }

        #endregion

        #region AllowHorizontalResize
        public static readonly DependencyProperty AllowHorizontalResizeProperty =
          DependencyProperty.Register("AllowHorizontalResize", typeof(bool), typeof(ResizeThumb),
            new FrameworkPropertyMetadata(true));        

        public bool AllowHorizontalResize
        {
            get { return (bool)GetValue(AllowHorizontalResizeProperty); }
            set { SetValue(AllowHorizontalResizeProperty, value); }
        }

        #endregion


        private Vector GetHookPointFromMyPosition()
        {
            var horzCenterOfThumb = Left + (Width / 2);
            var vertCenterOfThumb = Top + (Height / 2);

            var horzRound = Math.Round(horzCenterOfThumb / CanvasItem.Width);
            var vertRound = Math.Round(vertCenterOfThumb / CanvasItem.Height);

            var leftProportion = 1 - horzRound;
            var topProportion = 1 - vertRound;

            return new Vector(leftProportion, topProportion);
        }

        public new event EventHandler SizeChanged;
        public event EventHandler<SizeChangeEventArgs> HeightChanged;
        public event EventHandler<SizeChangeEventArgs> WidthChanged;
        public ObservableCollection<ICanvasItem> Children { get; private set; }
    }
    
}