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

        private void ComputeBounds()
        {
            this.BeginUpdate();

            Left = Children.GetLeft();
            Top = Children.GetTop();
            Width = Children.GetWidth();
            Height = Children.GetHeight();

            this.EndUpdate(false);
        }

        private IList<IRect> ChildrenRects
        {
            get { return Children.Select(item => item.Rect()).ToList(); }
        }

        private bool AreChildrenUpdatedByMyself
        {
            get { return ChildrenUpdateLevel > 0; }
        }

        private int ChildrenUpdateLevel { get; set; }

        private void AttachToChildrenLayoutEvents()
        {

            foreach (var child in Children)
            {
                child.PropertyChanged += ChildOnPropertyChanged;
            }
        }

        private void ChildOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            switch (propertyChangedEventArgs.PropertyName)
            {
                case "Left":
                case "Top":
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

        


        private IEnumerable<ICanvasItem> ChildrenExcept(ICanvasItem changed)
        {

            return Children.Except(new List<ICanvasItem> { changed });
        }

      
        private IEnumerable<IRect> ChildrenRectExcept(IRect changed)
        {
            return ChildrenRects.Except(new List<IRect> { changed });
        }

     



        public ChildrenExpandableCanvasItem()
            : this(new List<ICanvasItem>())
        {
        }



        public void Dispose()
        {
            DettachFromChildrenLayoutEvents();
        }
    }
}