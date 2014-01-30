using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using Glass.Design.Pcl.CanvasItem;
using Glass.Design.Pcl.Core;
using Glass.Design.Pcl.DesignSurface;

namespace Glass.Design.Pcl
{
    public class ChildrenExpandableCanvasItem : CanvasItem.CanvasItem, IDisposable
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