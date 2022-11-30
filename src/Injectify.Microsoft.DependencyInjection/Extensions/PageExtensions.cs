using Injectify.Abstractions;
using Injectify.Annotations;
using Injectify.Exceptions;
using Injectify.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Injectify.Microsoft.DependencyInjection.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class PageExtensions
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

            if (classInjectable != null)
            {
                var serviceProvider = IntrospectionHelper.GetServiceProviderFromApplication<Application, ServiceProvider>(Application.Current);
                var context = new InjectionContext<TPage, ServiceProvider>(page,
                    serviceProvider,
                    ServiceProviderExtensions.GetByPropertyInfo,
                    ServiceProviderExtensions.GetByParameterInfo);

                classInjectable.Bootstrap(context);
            }

            return page;
        }
    }
}
