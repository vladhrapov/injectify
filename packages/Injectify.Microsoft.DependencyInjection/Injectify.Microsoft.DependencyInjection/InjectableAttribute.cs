using Injectify.Abstractions;
using Injectify.Microsoft.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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
        /// <param name="page"></param>
        public void Bootstrap<TPage>(TPage page)
        {
            BootstrapProps(page);
        }

        private void BootstrapProps<T>(T page)
        {
            var propsInfo = page.GetType()
                .GetProperties()
                .Where(pi => pi.GetCustomAttribute<InjectAttribute>() != null)
                .ToArray();

            foreach (var propInfo in propsInfo)
            {
                if (propInfo.PropertyType?.GenericTypeArguments?.Any() ?? false)
                {
                    var serviceProvider = GetServiceProviderFromApplication();
                    var instDemo = serviceProvider.GetServices(propInfo.PropertyType?.GenericTypeArguments?.FirstOrDefault());
                    //var instDemo = app.Services.GetServices(propInfo.PropertyType?.GenericTypeArguments?.FirstOrDefault());

                    //var instDemo = (App.Current as App).Services.GetServices(propInfo.PropertyType?.GenericTypeArguments?.FirstOrDefault());
                    propInfo.SetValue(page, instDemo);
                }
                else
                {
                    var serviceProvider = GetServiceProviderFromApplication();
                    var instDemo = serviceProvider.GetService(propInfo.PropertyType);
                    //var instDemo = app.Services.GetService(propInfo.PropertyType);

                    //var instDemo = (App.Current as App).Services.GetService(propInfo.PropertyType);
                    propInfo.SetValue(page, instDemo);
                }
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
