using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Injectify.Microsoft.DependencyInjection.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    public static class Bootstraper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="page"></param>
        public static void Bootstrap<TPage>(this TPage page)
            where TPage : Page
        {
            //BootstrapApp();
            var classInjectable = page.GetType().GetCustomAttribute<InjectableAttribute>();

            classInjectable.Bootstrap(page);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="page"></param>
        public static void Bootstrap<T>(T page)
        {
            //BootstrapApp();
            var classInjectable = page.GetType().GetCustomAttribute<InjectableAttribute>();

            classInjectable.Bootstrap(page);
        }
    }
}
