using Injectify.Abstractions;
using Injectify.Annotations;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Injectify.Autofac")]
[assembly: InternalsVisibleTo("Injectify.Microsoft.DependencyInjection")]

namespace Injectify.Helpers
{
    /// <summary>
    /// Bootstrapper for a class implementing
    /// <see cref="Injectify.Abstractions.IStartup{TServiceCollection}"/>,
    /// DI service provider and various annotations marked with
    /// <see cref="Injectify.Annotations.InjectAttribute"/>,
    /// <see cref="Injectify.Annotations.OnInitAttribute"/>.
    /// </summary>
    internal sealed class BootstrapHelper
    {
        /// <summary>
        /// Bootstraps a class implementing
        /// <see cref="Injectify.Abstractions.IStartup{TServiceCollection}"/>
        /// </summary>
        /// <typeparam name="TApplication">Application type to introspect.</typeparam>
        /// <typeparam name="TServiceCollection">
        /// Service collection type contining registered instances (DI specific).
        /// </typeparam>
        /// <typeparam name="TServiceProvider">Service provider type (DI specific).</typeparam>
        /// <param name="application">Application type instance.</param>
        /// <param name="services">Service collection instance (DI specific).</param>
        /// <param name="providerBuilder">Function returning service provider instance (DI specific).</param>
        public static void BootstrapStartup<TApplication, TServiceCollection, TServiceProvider>(
            TApplication application,
            TServiceCollection services,
            Func<TServiceCollection, TServiceProvider> providerBuilder)
                where TApplication : class
                where TServiceCollection : class
                where TServiceProvider : class
        {
            // get startup implementation
            var startupClass = IntrospectionHelper.GetStartupType<TServiceCollection>();

            // create instance of the startup
            var startupInstance = Activator.CreateInstance(startupClass) as IStartup<TServiceCollection>;

            startupInstance.ConfigureServices(services);
            var provider = providerBuilder(services);// services.BuildServiceProvider();

            BootstrapHelper.BootstrapServiceProvider(application, provider);
        }

        /// <summary>
        /// Bootstraps service provider and sets provider instance
        /// to application class property.
        /// </summary>
        /// <typeparam name="TApplication">Application type to introspect.</typeparam>
        /// <typeparam name="TServiceProvider">Service provider type (DI specific).</typeparam>
        /// <param name="application">Application type instance.</param>
        /// <param name="provider">Service provider instance (DI specific).</param>
        public static void BootstrapServiceProvider<TApplication, TServiceProvider>(
            TApplication application,
            TServiceProvider provider)
                where TApplication : class
                where TServiceProvider : class
        {
            var servicesProp = IntrospectionHelper.GetServiceProviderProperty<TApplication, TServiceProvider>();

            servicesProp.SetValue(application, provider);
        }

        /// <summary>
        /// Bootstraps properties marked with <see cref="Injectify.Annotations.InjectAttribute"/>
        /// inside class marked with <see cref="Injectify.Annotations.InjectableAttribute"/>.
        /// </summary>
        /// <typeparam name="TPage">Page type (Framework specific).</typeparam>
        /// <typeparam name="TServiceProvider">Service provider type (DI specific).</typeparam>
        /// <param name="context">
        /// Injection context for a page, including service provider, registered services,
        /// service selectors and other required info.
        /// </param>
        public static void BootstrapProps<TPage, TServiceProvider>(
            InjectionContext<TPage, TServiceProvider> context)
                where TPage : class
        {
            var injectPropsInfo = context.Page
                .GetType()
                .GetProperties()
                .Where(pi => pi.GetCustomAttribute<InjectAttribute>() != null)
                .ToArray();

            if (!injectPropsInfo.Any())
                return;

            foreach (var propInfo in injectPropsInfo)
            {
                var inject = propInfo.GetCustomAttribute<InjectAttribute>();
                inject.Bootstrap(context, propInfo);
            }
        }

        /// <summary>
        /// Bootstraps method marked with <see cref="Injectify.Annotations.OnInitAttribute"/>
        /// attribute and calls method with found parameters.
        /// </summary>
        /// <typeparam name="TPage">Page type (Framework specific).</typeparam>
        /// <typeparam name="TServiceProvider">Service provider type (DI specific).</typeparam>
        /// <param name="context">
        /// Injection context for a page, including service provider, registered services,
        /// service selectors and other required info.
        /// </param>
        public static void BootstrapInitParams<TPage, TServiceProvider>(InjectionContext<TPage, TServiceProvider> context)
            where TPage : class
        {
            var onInitMethodInfo = IntrospectionHelper.GetOnInitMethod(context.Page);
            var onInitMethod = onInitMethodInfo?.GetCustomAttribute<OnInitAttribute>();

            if (onInitMethod is null)
            {
                return;
            }

            onInitMethod.Bootstrap(context, onInitMethodInfo);
        }
    }
}
