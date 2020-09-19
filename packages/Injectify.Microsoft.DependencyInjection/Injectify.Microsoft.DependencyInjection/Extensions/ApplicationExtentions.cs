using Injectify.Abstractions;
using Injectify.Microsoft.DependencyInjection.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Injectify.Microsoft.DependencyInjection.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationExtentions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="application"></param>
        /// <returns></returns>
        public static Frame GetRootFrame(this Application application)
        {
            BootstrapStartup(application);

            var serviceProviderPropInfo = application.GetType()
                .GetProperties()
                .Where(p => p.PropertyType == typeof(ServiceProvider))
                .FirstOrDefault();

            var serviceProvider = serviceProviderPropInfo.GetValue(application) as ServiceProvider;
            var frame = new FrameWithServiceProvider<ServiceProvider>(serviceProvider) as Frame;

            return frame;
        }

        /// <summary>
        /// Bootstrap startup for the UWP app.
        /// </summary>
        /// <param name="application"></param>
        public static void BootstrapStartup(this Application application)
        {
            // get startup implementation
            var startupClass = DependencyInjectionHelper.GetStartupType<ServiceCollection>();

            // create instance of the startup
            var startupInstance = Activator.CreateInstance(startupClass) as IStartup<ServiceCollection>;

            // set up configured service provider
            //this.Services = ((object)st.Services) as ServiceProvider;

            application.BootstrapApp(startupInstance);
        }

        private static void BootstrapApp(this Application application, IStartup<ServiceCollection> startup)
        {
            var appClass = DependencyInjectionHelper.GetAppType<ServiceCollection, ServiceProvider>();

            var bootAttribute = appClass?.GetCustomAttribute<UwpApplicationBootstrapAttribute>();

            // bootstrap startup services into application
            bootAttribute.Bootstrap(application, startup);
        }
    }
}
