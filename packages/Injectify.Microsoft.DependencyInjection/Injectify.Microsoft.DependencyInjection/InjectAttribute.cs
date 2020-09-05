using Injectify.Abstractions;
using System;

namespace Injectify.Microsoft.DependencyInjection
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class InjectAttribute : Attribute, IInject
    {
        public InjectAttribute()
        {
        }
    }
}
