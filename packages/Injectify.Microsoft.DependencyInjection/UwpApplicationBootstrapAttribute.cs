using System;

namespace Injectify.Microsoft.DependencyInjection
{
    /// <summary>
    /// Annotation for Application bootstrap class.
    /// </summary>
    [Obsolete("This attribute is going to be deleted in v1.0.0")]
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class UwpApplicationBootstrapAttribute : Attribute
    {
    }
}
