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
            where TPage : class
        {
            var serviceProvider = IntrospectionHelper.GetServiceProviderFromApplication<ServiceProvider>(Application.Current);

            if (serviceProvider is null)
            {
                throw new InjectifyException($"'{nameof(serviceProvider)}' should not be null.");
            }

            BootstrapProps(pageInstance, serviceProvider);
            BootstrapConstructorParams(pageInstance, serviceProvider);
        }

        private void BootstrapProps<TPage>(TPage page, ServiceProvider serviceProvider)
        {
            var injectPropsInfo = page.GetType()
                .GetProperties()
                .Where(pi => pi.GetCustomAttribute<InjectAttribute>() != null)
                .ToArray();

            if (!injectPropsInfo.Any())
                return;

            foreach (var propInfo in injectPropsInfo)
            {
                var inject = propInfo.GetCustomAttribute<InjectAttribute>();
                inject.Bootstrap(page, serviceProvider, propInfo);
            }
        }

        private void BootstrapConstructorParams<TPage>(TPage page, ServiceProvider serviceProvider)
            where TPage : class
        {
            Func<ServiceProvider, ParameterInfo, object> serviceSelector = (provider, parameterInfo) =>
            {
                if (parameterInfo.ParameterType?.GenericTypeArguments?.Any() ?? false)
                {
                    return provider.GetServices(parameterInfo.ParameterType?.GenericTypeArguments?.FirstOrDefault());
                }

                return provider.GetService(parameterInfo.ParameterType);
            };

            // UWP does not support instantiation of the page using constructor with any parameters.
            // This is a current limitation of the framework and it's internal implementation
            // of the page factory that creates page instances internally suring frame navigation
            // by using default constructor with no parameters.
            //
            // OnInit is a workaround for injecting dependencies as parameters similar to constructor.
            //
            // Proposal to use navigation with extended frame navigation:
            // https://github.com/microsoft/microsoft-ui-xaml/issues/693
            //
            BootstrapHelper.BootstrapInitParams(page, serviceProvider, serviceSelector);
        }
    }
}
