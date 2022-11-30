using Injectify.Abstractions;
using System;
using System.Reflection;

namespace Injectify.Annotations
{
    /// <summary>
    /// Marks property of the class for injection.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class InjectAttribute : Attribute, IInject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <typeparam name="TServiceProvider"></typeparam>
        /// <param name="context"></param>
        /// <param name="propInfo"></param>
        public void Bootstrap<TPage, TServiceProvider>(InjectionContext<TPage, TServiceProvider> context, PropertyInfo propInfo)
            where TPage : class
        {
            var propertyInstance = context.GetByPropertyInfo(context.ServiceProvider, propInfo);
            propInfo.SetValue(context.Page, propertyInstance);
        }
    }
}
