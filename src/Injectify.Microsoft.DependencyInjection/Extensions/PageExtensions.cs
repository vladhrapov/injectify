using Injectify.Abstractions;
using Injectify.Annotations;
using Injectify.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Injectify.Microsoft.DependencyInjection.Extensions
{
    /// <summary>
    /// Extensions for UWP page type.
    /// </summary>
    public static class PageExtensions
    {
        /// <summary>
        /// Executes bootstrap of all properties marked <see cref="Injectify.Annotations.InjectAttribute"/>
        /// and <see cref="Injectify.Annotations.OnInitAttribute"/> attributes.
        /// </summary>
        /// <typeparam name="TPage">Page type (Framework specific).</typeparam>
        /// <param name="page">Page instance of a framework specific type.</param>
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
