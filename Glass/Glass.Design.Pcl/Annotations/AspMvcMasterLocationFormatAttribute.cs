using System;

namespace Glass.Design.Pcl.Annotations
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class AspMvcMasterLocationFormatAttribute : Attribute
    {
        public AspMvcMasterLocationFormatAttribute(string format) { }
    }
}