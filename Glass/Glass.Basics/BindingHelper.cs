using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Glass.Basics.Wpf
{
    public static class BindingHelper
    {
        public static void UpdateSourcesOfAllBindings(this DependencyObject o)
        {
            //Immediate Properties
            var propertiesAll = new List<FieldInfo>();
            var currentLevel = o.GetType();
            while (currentLevel != typeof(object))
            {
                propertiesAll.AddRange(currentLevel.GetFields());
                currentLevel = currentLevel.BaseType;
            }
            var propertiesDp = propertiesAll.Where(x => x.FieldType == typeof(DependencyProperty));
            foreach (var property in propertiesDp)
            {
                var dependencyProperty = property.GetValue(o) as DependencyProperty;
                var ex = BindingOperations.GetBindingExpression(o, dependencyProperty);
                if (ex != null)
                {
                    ex.UpdateSource();
                }
            }

            //Children
            var childrenCount = VisualTreeHelper.GetChildrenCount(o);
            for (var i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(o, i);
                child.UpdateSourcesOfAllBindings();
            }
        } 
    }
}