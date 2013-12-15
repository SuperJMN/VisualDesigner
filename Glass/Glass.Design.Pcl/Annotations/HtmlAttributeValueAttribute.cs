using System;

namespace Glass.Design.Pcl.Annotations
{
    [AttributeUsage(
        AttributeTargets.Parameter | AttributeTargets.Field |
        AttributeTargets.Property, Inherited = true)]
    public sealed class HtmlAttributeValueAttribute : Attribute
    {
        public HtmlAttributeValueAttribute([NotNull] string name)
        {
            Name = name;
        }

        [NotNull]
        public string Name { get; private set; }
    }
}