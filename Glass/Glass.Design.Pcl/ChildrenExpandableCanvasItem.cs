using System;
using System.Collections.Generic;
using System.ComponentModel;
using Glass.Design.Pcl.Canvas;

namespace Glass.Design.Pcl
{
    public class ChildrenExpandableCanvasItem : CanvasVisualItem, IDisposable
    {
        protected ChildrenExpandableCanvasItem(IEnumerable<ICanvasItem> children)
        {
            foreach (var canvasItem in children)
            {
                Children.Add(canvasItem);
            }

            ComputeBounds();

            AttachToChildrenLayoutEvents();
        }

        protected ChildrenExpandableCanvasItem()
            : this(new List<ICanvasItem>())
        {
        }


        private void ComputeBounds()
        {
            this.BeginUpdate();

            Left = Children.GetLeft();
            Top = Children.GetTop();
            Width = Children.GetWidth();
            Height = Children.GetHeight();

            this.EndUpdate(false);
        }

    
        private void AttachToChildrenLayoutEvents()
        {
            foreach (var child in Children)
            {
                child.PropertyChanged += ChildOnPropertyChanged;
            }
        }

        private void ChildOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (this.IsUpdating)
                return;

            switch (propertyChangedEventArgs.PropertyName)
            {
                case "Left":
                case "Top":
                case "Width":
                case "Height":
                    this.ComputeBounds();
                    break;

            }
        }

        private void DettachFromChildrenLayoutEvents()
        {
            foreach (var child in Children)
            {
                child.PropertyChanged -= ChildOnPropertyChanged;
            }
        }

     



        public void Dispose()
        {
            DettachFromChildrenLayoutEvents();
        }
    }
}