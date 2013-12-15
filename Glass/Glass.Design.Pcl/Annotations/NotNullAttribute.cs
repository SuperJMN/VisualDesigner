using System;

namespace Glass.Design.Pcl.Annotations
{
    /// <summary>
    /// Indicates that the value of the marked element could never be <c>null</c>
    /// </summary>
    /// <example><code>
    /// [NotNull] public object Foo() {
    ///   return null; // Warning: Possible 'null' assignment
    /// }
    /// </code></example>
    [AttributeUsage(
        AttributeTargets.Method | AttributeTargets.Parameter |
        AttributeTargets.Property | AttributeTargets.Delegate |
        AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public sealed class NotNullAttribute : Attribute { }
}