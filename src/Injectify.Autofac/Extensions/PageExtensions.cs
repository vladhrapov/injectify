using Autofac;
using Injectify.Abstractions;
using Injectify.Annotations;
using Injectify.Helpers;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Injectify.Autofac.Extensions
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
                var serviceProvider = IntrospectionHelper.GetServiceProviderFromApplication<Application, IContainer>(Application.Current);
                var context = new InjectionContext<TPage, IContainer>(page,
                    serviceProvider,
                    ServiceProviderExtensions.GetByPropertyInfo,
                    ServiceProviderExtensions.GetByParameterInfo);

                classInjectable.Bootstrap(context);
            }

            return page;
        }
    }
}
