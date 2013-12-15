using System;

namespace Glass.Design.Pcl.Annotations
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class AspMvcAreaViewLocationFormatAttribute : Attribute
    {
        public AspMvcAreaViewLocationFormatAttribute(string format) { }
    }
}