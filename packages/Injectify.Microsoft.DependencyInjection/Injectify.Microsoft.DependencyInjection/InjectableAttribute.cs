using Injectify.Abstractions;
using Injectify.Exceptions;
using Injectify.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml;

namespace Injectify.Microsoft.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class InjectableAttribute : Attribute, IInjectable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="pageInstance"></param>
        public void Bootstrap<TPage>(TPage pageInstance)
        {
            BootstrapProps(pageInstance);
            BootstrapConstructorParams(pageInstance);
        }

        private void BootstrapProps<TPage>(TPage page)
        {
            var injectPropsInfo = page.GetType()
                .GetProperties()
                .Where(pi => pi.GetCustomAttribute<InjectAttribute>() != null)
                .ToArray();

            if (!injectPropsInfo.Any())
                return;

            var serviceProvider = IntrospectionHelper.GetServiceProviderFromApplication<ServiceProvider>(Application.Current);

            if (serviceProvider is null)
            {
                throw new InjectifyException($"'{nameof(serviceProvider)}' should not be null.");
            }

            foreach (var propInfo in injectPropsInfo)
            {
                var inject = propInfo.GetCustomAttribute<InjectAttribute>();
                inject.Bootstrap(page, serviceProvider, propInfo);
            }
        }

        private void BootstrapConstructorParams<TPage>(TPage page)
        {
            // ToDo: Implement
        }
    }
}
