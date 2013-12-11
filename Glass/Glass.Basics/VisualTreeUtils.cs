using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Glass.Basics
{
    public static class VisualTreeUtils
    {
        /// <summary>
        /// Finds a parent of a given item on the visual tree.
        /// </summary>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="child">A direct or indirect child of the
        /// queried item.</param>
        /// <returns>The first parent item that matches the submitted
        /// type parameter. If not matching item can be found, a null
        /// reference is being returned.</returns>
        public static T TryFindParent<T>(this DependencyObject child) where T : class
        {
            //get parent item
            var parentObject = GetParentObject(child);

            //we've reached the end of the tree
            if (parentObject == null) return null;

            //check if the parent matches the type we're looking for
            var parent = parentObject as T;
            if (parent != null)
            {
                return parent;
            }

            //use recursion to proceed with next level
            return TryFindParent<T>(parentObject);
        }

        /// <summary>
        /// This method is an alternative to WPF's
        /// <see cref="VisualTreeHelper.GetParent"/> method, which also
        /// supports content elements. Keep in mind that for content element,
        /// this method falls back to the logical tree of the element!
        /// </summary>
        /// <param name="child">The item to be processed.</param>
        /// <returns>The submitted item's parent, if available. Otherwise
        /// null.</returns>
        // ReSharper disable MemberCanBePrivate.Global
        public static DependencyObject GetParentObject(this DependencyObject child)
        // ReSharper restore MemberCanBePrivate.Global
        {
            if (child == null) return null;

            //handle content elements separately
            var contentElement = child as ContentElement;
            if (contentElement != null)
            {
                var parent = ContentOperations.GetParent(contentElement);
                if (parent != null) return parent;

                var fce = contentElement as FrameworkContentElement;
                return fce != null ? fce.Parent : null;
            }

            //also try searching for parent in framework elements (such as DockPanel, etc)
            var frameworkElement = child as FrameworkElement;
            if (frameworkElement != null)
            {
                var parent = frameworkElement.Parent;
                if (parent != null) return parent;
            }

            //if it's not a ContentElement/FrameworkElement, rely on VisualTreeHelper
            return VisualTreeHelper.GetParent(child);
        }

        public static T GetVisualChild<T>(this DependencyObject parent) where T : Visual
        {
            var child = default(T);

            var numVisuals = VisualTreeHelper.GetChildrenCount(parent);
            for (var i = 0; i < numVisuals; i++)
            {
                var v = (Visual)VisualTreeHelper.GetChild(parent, i);
                child = v as T ?? GetVisualChild<T>(v);
                if (child != null)
                {
                    break;
                }
            }
            return child;
        }

        // ReSharper disable UnusedMember.Global
        public static DependencyObject FindChild<T>(this DependencyObject control)
        // ReSharper restore UnusedMember.Global
        {
            var childNumber = VisualTreeHelper.GetChildrenCount(control);
            for (var i = 0; i < childNumber; i++)
            {
                var child = VisualTreeHelper.GetChild(control, i);

                if (child is T)
                    return child;

                var result = FindChild<T>(child);
                if (result != null)
                    return result;
            }
            return null;
        }


        /// <summary>
        /// Gets children, children's children, etc. from 
        /// the visual tree that match the specified type
        /// </summary>
        // ReSharper disable UnusedMember.Global
        public static List<T> GetDescendants<T>(this DependencyObject parent)
            // ReSharper restore UnusedMember.Global
            where T : UIElement
        {
            var children = new List<T>();

            var count = VisualTreeHelper.GetChildrenCount(parent);

            if (count > 0)
            {
                for (var i = 0; i < count; i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i) as UIElement;

                    if (child == null)
                        break;

                    if (child is T)
                    {
                        children.Add((T)child);
                    }

                    children.AddRange(child.GetDescendants<T>());
                }
                return children;
            }

            return new List<T>();
        }


        public static Rect GetBoundsWithOffset(this Visual visual)
        {
            var position = VisualTreeHelper.GetOffset(visual);
            var contentBounds = VisualTreeHelper.GetContentBounds(visual);

            return !contentBounds.Equals(Rect.Empty) ? Rect.Offset(contentBounds, position) : Rect.Empty;
        }
    }
}