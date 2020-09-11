using Injectify.Abstractions;
using System;

namespace Injectify.Microsoft.DependencyInjection
{
    /// <summary>
    /// Marks property of the class for injection.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class InjectAttribute : Attribute, IInject
    {
        /// <summary>
        /// Marks property of the class for injection.
        /// </summary>
        public InjectAttribute()
        {
        }
    }
}
