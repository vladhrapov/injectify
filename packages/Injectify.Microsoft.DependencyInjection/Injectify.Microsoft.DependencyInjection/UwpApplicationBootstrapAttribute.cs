using Injectify.Abstractions;
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
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class UwpApplicationBootstrapAttribute : Attribute
    {
        public UwpApplicationBootstrapAttribute()
        {
        }

        public void Bootstrap(Application application, IStartup<ServiceCollection, ServiceProvider> startup)
        {
            //Debug.WriteLine($"DI UWPAPPBOOTSTRAP: {Assembly.GetEntryAssembly().GetTypes().Where(t => t.IsClass && typeof(t).C).FirstOrDefault().FullName}");
            var a = Assembly.GetEntryAssembly()
                .GetTypes()
                .Where(t => t.IsClass && t.IsAssignableFrom(typeof(IUwpApplication<>)))
                .FirstOrDefault();

            var servicesProp = a.GetProperties()
                .Where(p => p.PropertyType == typeof(ServiceProvider))
                .FirstOrDefault();
                
            //var filteredProps = application.GetType()
            //    .GetProperties()
            //    .Where(p => p.PropertyType == typeof(ServiceProvider));
            //var servicesProp = filteredProps?.FirstOrDefault();

            servicesProp.SetValue(application, startup.Services);
        }
    }
}
