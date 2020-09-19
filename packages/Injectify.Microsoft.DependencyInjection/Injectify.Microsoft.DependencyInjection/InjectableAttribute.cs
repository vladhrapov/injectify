using Injectify.Abstractions;
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
        public InjectableAttribute()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="pageInstance"></param>
        public void Bootstrap<TPage>(TPage pageInstance)
        {
            BootstrapProps(pageInstance);
        }

        private void BootstrapProps<TPage>(TPage page)
        {
            var injectPropsInfo = page.GetType()
                .GetProperties()
                .Where(pi => pi.GetCustomAttribute<InjectAttribute>() != null)
                .ToArray();

            if (!injectPropsInfo.Any())
                return;

            var serviceProvider = GetServiceProviderFromApplication();

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

        private ServiceProvider GetServiceProviderFromApplication()
        {
            var classes = Assembly.GetEntryAssembly()
                .GetTypes();
            var classesAll = classes
                .Where(type => type.IsClass == true && HasInterfaces(type));

            var applicationClass = classesAll
                .FirstOrDefault();

            var servicesProperties = applicationClass.GetProperties().Where(pi => pi.PropertyType == typeof(ServiceProvider));
            var servicesProperty = servicesProperties.FirstOrDefault();

            //servicesProperty.SetValue(applicationClass, new Startup().Services);

            var val = servicesProperty.GetValue(Application.Current);
            return val as ServiceProvider;
        }

        private bool HasInterfaces(Type type)
        {
            var interfaces = type.GetInterfaces();

            var filtered = interfaces.Where(i => i.IsGenericType);
            filtered = filtered.Where(i => i.GetGenericTypeDefinition() == typeof(IUwpApplication<>));
            filtered = filtered.Where(i => i.GenericTypeArguments?.FirstOrDefault() == typeof(ServiceProvider));

            return filtered.Any();
        }
    }
}
