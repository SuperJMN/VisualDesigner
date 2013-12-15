using System;

namespace Glass.Design.Pcl.Annotations
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class AspMvcAreaPartialViewLocationFormatAttribute : Attribute
    {
        public AspMvcAreaPartialViewLocationFormatAttribute(string format) { }
    }
}