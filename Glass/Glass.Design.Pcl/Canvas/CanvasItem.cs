﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Glass.Design.Pcl.Core;
using PostSharp.Patterns.Contracts;
using PostSharp.Patterns.Model;
using PostSharp.Patterns.Recording;

// ReSharper disable CompareOfFloatsByEqualityOperator

namespace Glass.Design.Pcl.Canvas
{
    [NotifyPropertyChanged(true)]
    public abstract class CanvasItem : ICanvasItem, IRecordableCallback
    {
        private bool undoing;
        private double previousWidth, previousHeight, previousTop, previousLeft;

        protected bool IsUpdating { get; private set; }


        protected CanvasItem()
        {
            this.previousWidth = 1;
            this.previousHeight = 1;

        }

        internal ChildrenPositioning ChildrenPositioning { get; set; }


        
     
        public abstract CanvasItemCollection Children { get; set; }

        public abstract double Left { get; set; }

        public abstract double Top { get; set; }

        [StrictlyGreaterThan(0)]
        public abstract double Width { get; set; }

        [StrictlyGreaterThan(0)]
        public abstract double Height { get; set; }

        public double Right
        {
            get { return this.Left + this.Width; }
        }

        public double Bottom
        {
            get { return this.Top + this.Height; }
        }

        public abstract ICanvasItemContainer Parent { get; }

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

            if (widthFactor == 1 && heightFactor == 1) return;

            foreach (ICanvasItem child in this.Children)
            {
                if (!double.IsNaN(widthFactor) && widthFactor != 1)
                {
                    double origin = this.ChildrenPositioning == ChildrenPositioning.Absolute ? this.Left : 0;

                    child.Width = child.Width * widthFactor;
                    child.Left = origin + (child.Left - origin) * widthFactor;
                }

                if (!double.IsNaN(heightFactor) && heightFactor != 1)
                {
                    double origin = this.ChildrenPositioning == ChildrenPositioning.Absolute ? this.Top : 0;

                    child.Height = child.Height * heightFactor;
                    child.Top = origin + (child.Top - origin) * heightFactor;
                }
            }
        }

        protected virtual void OnMoved(double leftIncrement, double topIncrement)
        {
            if (this.ChildrenPositioning == ChildrenPositioning.Absolute)
            {
                if (leftIncrement == 0 && topIncrement == 0) return;

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
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (!this.IsUpdating && !this.undoing)
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
                            double factor = this.Width / this.previousWidth;
                            this.OnResized(factor, 1);
                            this.previousWidth = this.Width;
                            break;
                        }

                    case "Height":
                        {
                            double factor = this.Height / this.previousHeight;
                            this.OnResized(1, factor);
                            this.previousHeight = this.Height;

                            break;
                        }

                }
            }

            PropertyChangedEventHandler handler = this.PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }


        double ICoordinate.GetCoordinate(CoordinatePart part)
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

        void ICoordinate.SetCoordinate(CoordinatePart part, double value)
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

        void IRecordableCallback.OnReplaying(ReplayKind kind, ReplayContext context)
        {
            this.undoing = true;
        }

        void IRecordableCallback.OnReplayed(ReplayKind kind, ReplayContext context)
        {
            this.undoing = false;
        }

        public abstract string GetName();
    }
}