using Autofac;
using Injectify.Abstractions;
using System;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml;

namespace Injectify.Autofac
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
        public void Bootstrap(Application application, IStartup<ContainerBuilder> startup)
        {
            var uwpAppClass = Assembly.GetEntryAssembly()
                .GetTypes()
                .Where(t => t.IsClass && (t.BaseType == typeof(Application))) // ToDo: Maybe better to check whether implements IUwpApplication
                .FirstOrDefault();

            var servicesProp = uwpAppClass.GetProperties()
                .Where(p => p.PropertyType == typeof(IContainer))
                .FirstOrDefault();

            var services = new ContainerBuilder();
            startup.ConfigureServices(services);
            var provider = services.Build();

            servicesProp.SetValue(application, provider);
        }
    }
}
