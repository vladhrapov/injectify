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
    internal sealed class BootstrapHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TApplication"></typeparam>
        /// <typeparam name="TServiceCollection"></typeparam>
        /// <typeparam name="TServiceProvider"></typeparam>
        /// <param name="application"></param>
        /// <param name="services"></param>
        /// <param name="providerBuilder"></param>
        public static void BootstrapStartup<TApplication, TServiceCollection, TServiceProvider>(TApplication application,
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

        public static void BootstrapServiceProvider<TApplication, TServiceProvider>(TApplication application, TServiceProvider provider)
            where TApplication : class
            where TServiceProvider : class
        {
            var servicesProp = IntrospectionHelper.GetServiceProviderProperty<TApplication, TServiceProvider>();

            servicesProp.SetValue(application, provider);
        }

        public static void BootstrapProps<TPage, TServiceProvider>(InjectionContext<TPage, TServiceProvider> context)
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
