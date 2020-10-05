using System;
using System.Reflection;
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
        [Obsolete("Call of this method starting from v0.3.0 is redundant and outdated. It will be removed starting from the major version 1.0.0.")]
        public static void Bootstrap<TPage>(this TPage page)
            where TPage : Page
        {
            //BootstrapApp();
            var classInjectable = page.GetType().GetCustomAttribute<InjectableAttribute>();

            classInjectable.Bootstrap(page);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="page"></param>
        //public static void Bootstrap<T>(T type)
        //{
        //    //BootstrapApp();
        //    var classInjectable = type.GetType().GetCustomAttribute<InjectableAttribute>();

        //    classInjectable.Bootstrap(page);
        //}
    }
}
