﻿using Autofac;
using Injectify.Abstractions;
using Injectify.Exceptions;
using Injectify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Injectify.Autofac.Extensions
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
                .Where(p => p.PropertyType == typeof(IContainer))
                .FirstOrDefault();

            if (serviceProviderPropInfo is null)
            {
                throw new InjectifyException($"App type does not implement {typeof(IUwpApplication<>)}");
            }

            var serviceProvider = serviceProviderPropInfo.GetValue(application) as IContainer;
            var frame = new FrameWithServiceProvider(serviceProvider) as Frame;

            return frame;
        }

        /// <summary>
        /// Bootstrap startup for the UWP app.
        /// </summary>
        /// <param name="application"></param>
        public static void BootstrapStartup(this Application application)
        {
            // get startup implementation
            var startupClass = IntrospectionHelper.GetStartupType<ContainerBuilder>();

            // create instance of the startup
            var startupInstance = Activator.CreateInstance(startupClass) as IStartup<ContainerBuilder>;

            // set up configured service provider
            application.BootstrapApp(startupInstance);
        }

        private static void BootstrapApp(this Application application, IStartup<ContainerBuilder> startup)
        {
            var appClass = IntrospectionHelper.GetAppType<ContainerBuilder, IContainer>();

            var bootAttribute = appClass?.GetCustomAttribute<UwpApplicationBootstrapAttribute>();

            // bootstrap startup services into application
            bootAttribute.Bootstrap(application, startup);
        }
    }
}