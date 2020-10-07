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
    public sealed class UwpApplicationBootstrapAttribute : Attribute
    {
        /// <summary>
        /// Bootstrap application marked by UwpApplicationBootstrap.
        /// </summary>
        /// <param name="application"></param>
        /// <param name="startup"></param>
        public void Bootstrap(Application application, IStartup<ServiceCollection> startup)
        {
            var uwpAppClass = Assembly.GetEntryAssembly()
                .GetTypes()
                .Where(t => t.IsClass && (t.BaseType == typeof(Application))) // ToDo: Maybe better to check whether implements IUwpApplication
                .FirstOrDefault();

            var servicesProp = uwpAppClass.GetProperties()
                .Where(p => p.PropertyType == typeof(ServiceProvider))
                .FirstOrDefault();

            var services = new ServiceCollection();
            startup.ConfigureServices(services);
            var provider = services.BuildServiceProvider();

            servicesProp.SetValue(application, provider);
        }
    }
}
