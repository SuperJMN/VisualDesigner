using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
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

            Children.CollectionChanged += ChildrenOnCollectionChanged;

            Left = Extensions.GetLeftFromChildren(ChildrenRects);
            Top = Extensions.GetTopFromChildren(ChildrenRects);
            Width = Extensions.GetWidthFromChildren(ChildrenRects, Left);
            Height = Extensions.GetHeightFromChildren(ChildrenRects, Top);

            AttachToChildrenLayoutEvents();
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
                child.LeftChanged += ChildOnLeftChanged;
                child.TopChanged += ChildOnTopChanged;
            }
        }

        private void DettachFromChildrenLayoutEvents()
        {
            foreach (var child in Children)
            {
                child.LeftChanged -= ChildOnLeftChanged;
                child.TopChanged -= ChildOnTopChanged;
            }
        }

        // TODO: Refactor
        private void ChildOnLeftChanged(object sender, LocationChangedEventArgs locationChangedEventArgs)
        {

            if (!AreChildrenUpdatedByMyself)
            {
                ChildrenUpdateLevel++;

                var changed = (ICanvasItem)sender;

                var newBounds = Extensions.GetBoundingRect(ChildrenRects);
                var oldRects = ChildrenRectExcept(changed.Rect()).ToList();

                var changedOldRect = changed.Rect();
                changedOldRect.X = locationChangedEventArgs.OldValue;

                oldRects.Add(changedOldRect);

                var oldBounds = Extensions.GetBoundingRect(oldRects);

                var leftExcess = newBounds.Left - oldBounds.Left;
                var rightExcess = newBounds.Right - oldBounds.Right;
                         
                Width += rightExcess - leftExcess;
                Left += leftExcess;

                ChildrenUpdateLevel--;
            }
        }

        private IEnumerable<ICanvasItem> ChildrenExcept(ICanvasItem changed)
        {

            return Children.Except(new List<ICanvasItem> { changed });
        }

        // TODO: Refactor
        private void ChildOnTopChanged(object sender, LocationChangedEventArgs locationChangedEventArgs)
        {

            if (!AreChildrenUpdatedByMyself)
            {
                ChildrenUpdateLevel++;

                var changed = (ICanvasItem)sender;

                var newBounds = Extensions.GetBoundingRect(ChildrenRects);
                var oldRects = ChildrenRectExcept(changed.Rect()).ToList();

                var changedOldRect = changed.Rect();
                changedOldRect.Y = locationChangedEventArgs.OldValue;

                oldRects.Add(changedOldRect);

                var oldBounds = Extensions.GetBoundingRect(oldRects);

                var topExcess = newBounds.Top - oldBounds.Top;
                var bottomExcess = newBounds.Bottom - oldBounds.Bottom;

                Height += bottomExcess - topExcess;
                Top += topExcess;

                ChildrenUpdateLevel--;
            }
        }

        private IEnumerable<IRect> ChildrenRectExcept(IRect changed)
        {
            return ChildrenRects.Except(new List<IRect> { changed });
        }

        protected override void OnLeftChanged(LocationChangedEventArgs e)
        {
            if (!AreChildrenUpdatedByMyself)
            {
                ChildrenUpdateLevel++;
                ApplyDeltaToChildrensLeft(e.OldValue, e.NewValue);
                ChildrenUpdateLevel--;
            }
        }



        protected override void OnTopChanged(LocationChangedEventArgs e)
        {
            if (!AreChildrenUpdatedByMyself)
            {
                ChildrenUpdateLevel++;

                Children.SwapCoordinates();
                ApplyDeltaToChildrensLeft(e.OldValue, e.NewValue);
                Children.SwapCoordinates();

                ChildrenUpdateLevel--;
            }
        }

        protected override void OnWidthChanged(SizeChangeEventArgs e)
        {
            if (!AreChildrenUpdatedByMyself)
            {
                ChildrenUpdateLevel++;

                ApplyDeltaToChildrensWidth(e, Left);

                ChildrenUpdateLevel--;
            }
        }

        protected override void OnHeightChanged(SizeChangeEventArgs e)
        {
            if (!AreChildrenUpdatedByMyself)
            {
                ChildrenUpdateLevel++;

                Children.SwapCoordinates();
                ApplyDeltaToChildrensWidth(e, Top);
                Children.SwapCoordinates();

                ChildrenUpdateLevel--;
            }
        }

        public ChildrenExpandableCanvasItem()
            : this(new List<ICanvasItem>())
        {
        }


        private void ChildrenOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {

        }

        private void ApplyDeltaToChildrensLeft(double oldLeft, double newLeft)
        {
            var delta = newLeft - oldLeft;

            foreach (var canvasItem in Children)
            {
                canvasItem.Left += delta;
            }
        }

        private void ApplyDeltaToChildrensWidth(SizeChangeEventArgs sizeChangeEventArgs, double currentParentLeft)
        {
            if (sizeChangeEventArgs.OldValue <= 0)
            {
                return;
            }

            foreach (var canvasItem in Children)
            {
                Resize(canvasItem, sizeChangeEventArgs, currentParentLeft);
            }
        }

        private static void Resize(ICanvasItem canvasItem, SizeChangeEventArgs sizeChange, double currentParentLeft)
        {
            var currentLeft = canvasItem.Left - currentParentLeft;
            var currentWidth = canvasItem.Width;

            var leftProportion = currentLeft / sizeChange.OldValue;
            var widthProportion = currentWidth / sizeChange.OldValue;

            canvasItem.Left = currentParentLeft + sizeChange.NewValue * leftProportion;
            canvasItem.Width = sizeChange.NewValue * widthProportion;
        }

        public void Dispose()
        {
            DettachFromChildrenLayoutEvents();
        }
    }
}