using System;

namespace Glass.Design.Pcl.Annotations
{
    /// <summary>
    /// Razor attribute. Indicates that a parameter or a method is a Razor section.
    /// Use this attribute for custom wrappers similar to 
    /// <c>System.Web.WebPages.WebPageBase.RenderSection(String)</c>
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method, Inherited = true)]
    public sealed class RazorSectionAttribute : Attribute { }
}