using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using Design.Interfaces;

namespace Glass.Design
{
    [TemplatePart(Name = "PART_Thumb", Type = typeof(Thumb))]
    public class MovingControl : Control
    {
        public Thumb Thumb { get; private set; }


        static MovingControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MovingControl), new FrameworkPropertyMetadata(typeof(MovingControl)));
        }

        #region CanvasItem
        public static readonly DependencyProperty CanvasItemProperty =
            DependencyProperty.Register("CanvasItem", typeof(ICanvasItem), typeof(MovingControl),
                new FrameworkPropertyMetadata((ICanvasItem)null));

        public ICanvasItem CanvasItem
        {
            get { return (ICanvasItem)GetValue(CanvasItemProperty); }
            set { SetValue(CanvasItemProperty, value); }
        }

        #endregion

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            Thumb = (Thumb)Template.FindName("PART_Thumb", this);
        }
    }
}