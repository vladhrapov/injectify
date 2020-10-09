using Injectify.Abstractions;
using Injectify.Annotations;
using Injectify.Exceptions;
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
    public static class Bootstrapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="page"></param>
        public static TPage Bootstrap<TPage>(this TPage page)
            where TPage : Page
        {
            var classInjectable = page.GetType().GetCustomAttribute<InjectableAttribute>();

            var serviceProvider = IntrospectionHelper.GetServiceProviderFromApplication<ServiceProvider>(Application.Current);
            var context = new InjectionContext<TPage, ServiceProvider>(page,
                serviceProvider,
                ServiceProviderExtensions.GetByPropertyInfo,
                ServiceProviderExtensions.GetByParameterInfo);

            if (classInjectable is null)
            {
                throw new InjectifyException($"'{page.GetType().Name}' was not marked using '{nameof(InjectableAttribute)}'");
            }

            classInjectable.Bootstrap(context);

            return page;
        }
    }
}
