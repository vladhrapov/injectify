using Injectify.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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
        /// <param name="serviceSelector"></param>
        public void Bootstrap<TPage, TServiceProvider>(InjectionContext<TPage, TServiceProvider> context,
            PropertyInfo propInfo,
            Func<TServiceProvider, PropertyInfo, object> serviceSelector)
                where TPage : class
        {
            var propertyInstance = serviceSelector(context.ServiceProvider, propInfo);
            propInfo.SetValue(context.Page, propertyInstance);
        }
    }
}
