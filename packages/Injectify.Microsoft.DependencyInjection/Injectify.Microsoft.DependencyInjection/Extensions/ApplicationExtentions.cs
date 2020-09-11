using Injectify.Abstractions;
using Injectify.Microsoft.DependencyInjection.Helpers;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace Injectify.Microsoft.DependencyInjection.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class ApplicationExtentions
    {
        /// <summary>
        /// Bootstrap startup for the UWP app.
        /// </summary>
        /// <param name="application"></param>
        public static void BootstrapStartup(this Application application)
        {
            // get startup implementation
            var startupClass = DependencyInjectionHelper.GetStartupType<ServiceCollection, ServiceProvider>();

            // create instance of the startup
            var startupInstance = Activator.CreateInstance(startupClass) as IStartup<ServiceCollection, ServiceProvider>;

            // set up configured service provider
            //this.Services = ((object)st.Services) as ServiceProvider;

            application.BootstrapApp(startupInstance);
        }

        private static void BootstrapApp(this Application application, IStartup<ServiceCollection, ServiceProvider> startup)
        {
            var appClass = DependencyInjectionHelper.GetAppType<ServiceCollection, ServiceProvider>();

            var bootAttribute = appClass?.GetCustomAttribute<UwpApplicationBootstrapAttribute>();

            // bootstrap startup services into application
            bootAttribute.Bootstrap(application, startup);
        }
    }
}
