using Injectify.Abstractions;
using Injectify.Microsoft.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Injectify.Microsoft.DependencyInjection.Helpers
{
    internal sealed class DependencyInjectionHelper
    {
        public static Type GetAppType<TServiceCollection, TServiceProvider>()
        {
            Debug.WriteLine($"DI ASSEMBLY: {Assembly.GetCallingAssembly().FullName} \n");
            Debug.WriteLine($"DI ASSEMBLY: {Assembly.GetEntryAssembly().FullName}\n");
            var classes = Assembly.GetEntryAssembly()
                .GetTypes()
                .Where(t => t.IsClass && HasInterfaces2<TServiceCollection, TServiceProvider>(t));

            return classes?.FirstOrDefault();
        }

        public static Type GetStartupType<TServiceCollection, TServiceProvider>()
        {
            var classes = Assembly.GetEntryAssembly()
                .GetTypes()
                .Where(t => t.IsClass && HasInterfaces<TServiceCollection, TServiceProvider>(t));

            return classes?.FirstOrDefault();
        }

        private static bool HasInterfaces<TServiceCollection, TServiceProvider>(Type type)
        {
            var interfaces = type.GetInterfaces();

            var filtered = interfaces.Where(i => i.IsGenericType);
            filtered = filtered.Where(i => i.GetGenericTypeDefinition() == typeof(IStartup<,>));
            filtered = filtered.Where(i => i.GenericTypeArguments?.FirstOrDefault() == typeof(TServiceCollection)
                && i.GenericTypeArguments?.LastOrDefault() == typeof(TServiceProvider));

            return filtered.Any();
        }

        private static bool HasInterfaces2<TServiceCollection, TServiceProvider>(Type type)
        {
            var interfaces = type.GetInterfaces();
            Debug.WriteLine($"====BEFORE ==== HasInterfaces2 DI ASSEMBLY : {string.Join(',', interfaces.Select(i => i.FullName))}\n");

            var filtered = interfaces.Where(i => i.IsGenericType);
            filtered = filtered.Where(i => i.GetGenericTypeDefinition() == typeof(IUwpApplication<>));
            //filtered = filtered.Where(i => i.GenericTypeArguments?.FirstOrDefault() == typeof(TServiceProvider));

            Debug.WriteLine($"+++AFTER+++ HasInterfaces2 DI ASSEMBLY : {type.FullName} RESULT: {filtered.Any()}\n");
            return filtered.Any();
        }
    }
}
