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
        /// <param name="page"></param>
        /// <param name="propInfo"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="serviceSelector"></param>
        public void Bootstrap<TPage, TServiceProvider>(TPage page,
            PropertyInfo propInfo,
            TServiceProvider serviceProvider,
            Func<TServiceProvider, PropertyInfo, object> serviceSelector)
        {
            var propertyInstance = serviceSelector(serviceProvider, propInfo);
            propInfo.SetValue(page, propertyInstance);
        }
    }
}
