using Injectify.Abstractions;
using Injectify.Annotations;
using Injectify.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Injectify.Microsoft.DependencyInjection.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class Bootstraper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="page"></param>
        [Obsolete("Call of this method starting from v0.3.0 is redundant and outdated. It will be removed starting from the major version 1.0.0.")]
        public static void Bootstrap<TPage>(this TPage page)
            where TPage : Page
        {
            //BootstrapApp();
            var classInjectable = page.GetType().GetCustomAttribute<InjectableAttribute>();

            var serviceProvider = IntrospectionHelper.GetServiceProviderFromApplication<ServiceProvider>(Application.Current);
            var context = new InjectionContext<TPage, ServiceProvider>(page,
                serviceProvider,
                ServiceProviderExtensions.GetByPropertyInfo,
                ServiceProviderExtensions.GetByParameterInfo);

            classInjectable.Bootstrap(context);
        }
    }
}
