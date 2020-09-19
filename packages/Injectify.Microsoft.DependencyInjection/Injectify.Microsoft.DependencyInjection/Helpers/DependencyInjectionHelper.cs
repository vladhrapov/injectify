using Injectify.Abstractions;
using System;
using System.Linq;
using System.Reflection;

namespace Injectify.Microsoft.DependencyInjection.Helpers
{
    internal sealed class DependencyInjectionHelper
    {
        public static Type GetAppType<TServiceCollection, TServiceProvider>()
        {
            var classes = Assembly.GetEntryAssembly()
                .GetTypes()
                .Where(t => t.IsClass && HasInterfaces2<TServiceProvider>(t));

            return classes?.FirstOrDefault();
        }

        public static Type GetStartupType<TServiceCollection>()
        {
            var classes = Assembly.GetEntryAssembly()
                .GetTypes()
                .Where(t => t.IsClass && HasInterfaces<TServiceCollection>(t));

            return classes?.FirstOrDefault();
        }

        private static bool HasInterfaces<TServiceCollection>(Type type)
        {
            var interfaces = type.GetInterfaces();

            var filtered = interfaces.Where(i => i.IsGenericType);
            filtered = filtered.Where(i => i.GetGenericTypeDefinition() == typeof(IStartup<>));
            filtered = filtered.Where(i => i.GenericTypeArguments?.FirstOrDefault() == typeof(TServiceCollection));

            return filtered.Any();
        }

        private static bool HasInterfaces2<TServiceProvider>(Type type)
        {
            var interfaces = type.GetInterfaces();

            var filtered = interfaces.Where(i => i.IsGenericType);
            filtered = filtered.Where(i => i.GetGenericTypeDefinition() == typeof(IUwpApplication<>));
            filtered = filtered.Where(i => i.GenericTypeArguments?.FirstOrDefault() == typeof(TServiceProvider)); // Check out if need ??

            return filtered.Any();
        }
    }
}
