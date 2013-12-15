using System;

namespace Glass.Design.Pcl.Annotations
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class AspMvcPartialViewLocationFormatAttribute : Attribute
    {
        public AspMvcPartialViewLocationFormatAttribute(string format) { }
    }
}