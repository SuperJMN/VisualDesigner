using System;

namespace Glass.Design.Pcl.Annotations
{
    /// <summary>
    /// ASP.NET MVC attribute. If applied to a parameter, indicates that the parameter
    /// is an MVC view. If applied to a method, the MVC view name is calculated implicitly
    /// from the context. Use this attribute for custom wrappers similar to
    /// <c>System.Web.Mvc.Controller.View(ObjectDto)</c>
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Method)]
    public sealed class AspMvcViewAttribute : PathReferenceAttribute { }
}