using Injectify.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;

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

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <typeparam name="TServiceProvider"></typeparam>
        /// <param name="page"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="propInfo"></param>
        public void Bootstrap<TPage, TServiceProvider>(TPage page, TServiceProvider serviceProvider, PropertyInfo propInfo)
        {
            if (propInfo.PropertyType?.GenericTypeArguments?.Any() ?? false)
            {
                var serviceInstances = (serviceProvider as ServiceProvider).GetServices(propInfo.PropertyType?.GenericTypeArguments?.FirstOrDefault());
                propInfo.SetValue(page, serviceInstances);
            }
            else
            {
                var serviceInstance = (serviceProvider as ServiceProvider).GetService(propInfo.PropertyType);
                propInfo.SetValue(page, serviceInstance);
            }
        }
    }
}
