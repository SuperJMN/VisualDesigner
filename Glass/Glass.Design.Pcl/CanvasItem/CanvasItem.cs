using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface;
using PostSharp.Patterns.Contracts;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Undo;

namespace Glass.Design.Pcl.CanvasItem
{
    [NotifyPropertyChanged]
    [Recordable]
    public class CanvasItem : ICanvasItem
    {
        [NotRecorded]
        private double previousWidth, previousHeight, previousTop, previousLeft;

        protected bool IsUpdating { get; private set; }
        private double _left;
        private CanvasItemCollection children;

        public CanvasItem()
        {
            this.Width = this.previousWidth = 1;
            this.Height = this.previousHeight = 1;
        }

        public double Right { get { return Left + Width; } }
        public double Bottom { get { return Top + Height; } }
        public ICanvasItemParent Parent { get; set; }

        public virtual CanvasItemCollection Children
        {
            get
            {
                // Lazy initialization ensures the collection is not created if the property is overridden.
                if ( this.children == null )
                    this.children = new CanvasItemCollection();
                return children;
            }
        }

        public double Left
        {
            get { return _left; }
            set { _left = value; }
        }

        public double Top { get; set; }

        [StrictlyGreaterThan(0)]
        public double Width { get; set; }
        [StrictlyGreaterThan(0)]
        public double Height { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void BeginUpdate()
        {
            this.IsUpdating = true;
        }

        protected void EndUpdate(bool applyToChildren)
        {
            this.IsUpdating = false;

            if (applyToChildren)
            {
                this.OnMoved(this.Left - this.previousLeft, this.Top - this.previousTop);
                this.OnResized(this.Width - this.previousWidth, this.Height - this.previousHeight);
            }

            this.previousLeft = this.Left;
            this.previousTop = this.Top;
            this.previousHeight = this.Height;
            this.previousWidth = this.Width;

        }

        protected virtual void OnResized(double widthFactor, double heightFactor)
        {
            foreach (ICanvasItem child in this.Children)
            {
                if (!double.IsNaN(widthFactor) && widthFactor != 1)
                {
                    child.Width = child.Width*widthFactor;
                    child.Left = this.Left + (child.Left - this.Left)*widthFactor;
                }

                if (!double.IsNaN(heightFactor) && heightFactor != 1)
                {
                    child.Height = child.Height*heightFactor;
                    child.Top = this.Top + (child.Top - this.Top)*heightFactor;
                }
            }
        }

        protected virtual void OnMoved(double leftIncrement, double topIncrement)
        {
            foreach (ICanvasItem child in this.Children)
            {
                if (!double.IsNaN(leftIncrement) && leftIncrement != 0)
                {
                    child.Left += leftIncrement;
                }

                if (!double.IsNaN(topIncrement) && topIncrement != 0)
                {
                    child.Top += topIncrement;
                }
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (!this.IsUpdating)
            {
                switch (propertyName)
                {
                    case "Top":
                    {
                        this.OnMoved(0, this.Top - this.previousTop);
                        this.previousTop = this.Top;
                        break;
                    }

                    case "Left":
                    {
                        this.OnMoved(this.Left - this.previousLeft, 0);
                        this.previousLeft = this.Left;
                        break;
                    }

                    case "Width":
                    {
                        double factor = this.Width/this.previousWidth;
                        this.OnResized(factor, 1);
                        this.previousWidth = this.Width;
                        break;
                    }

                    case "Height":
                    {
                        double factor = this.Height/this.previousHeight;
                        this.OnResized(1, factor);
                        this.previousHeight = this.Height;

                        break;
                    }

                }
            }

            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public double GetCoordinate(CoordinatePart part)
        {
            switch (part)
            {
                case CoordinatePart.None:
                    return double.NaN;
                case CoordinatePart.Left:
                    return this.Left;
                case CoordinatePart.Right:
                    return this.Right;
                case CoordinatePart.Top:
                    return this.Top;
                case CoordinatePart.Bottom:
                    return this.Bottom;
                case CoordinatePart.Width:
                    return this.Width;
                case CoordinatePart.Height:
                    return this.Height;
                default:
                    throw new ArgumentOutOfRangeException("part");
            }
        }

        public void SetCoordinate(CoordinatePart part, double value)
        {

            switch (part)
            {
                case CoordinatePart.None:
                    break;
                case CoordinatePart.Left:
                    this.Left = value;
                    break;
                case CoordinatePart.Top:
                    this.Top = value;
                    break;
                case CoordinatePart.Width:
                    this.Width = value;
                    break;
                case CoordinatePart.Height:
                    this.Height = value;
                    break;
                case CoordinatePart.Bottom:
                case CoordinatePart.Right:
                    throw new NotSupportedException();
                default:
                    throw new ArgumentOutOfRangeException("part");
            }
        }

        public override string ToString()
        {
            return string.Format("{0} Left={1}, Top={2}, Width={3}, Height={4}", this.GetType().Name, this.Left,
                this.Top,
                this.Width, this.Height);
        }
    }


}