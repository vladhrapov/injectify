using Injectify.Annotations;
using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Injectify.Microsoft.DependencyInjection")]

namespace Injectify.Helpers
{
    internal sealed class BootstrapHelper
    {
        public static void BootstrapProps<TPage, TServiceProvider>(TPage page,
            TServiceProvider serviceProvider,
            Func<TServiceProvider, PropertyInfo, object> serviceSelector)
        {
            var injectPropsInfo = page.GetType()
                .GetProperties()
                .Where(pi => pi.GetCustomAttribute<InjectAttribute>() != null)
                .ToArray();

            if (!injectPropsInfo.Any())
                return;

            foreach (var propInfo in injectPropsInfo)
            {
                var inject = propInfo.GetCustomAttribute<InjectAttribute>();
                inject.Bootstrap(page, propInfo, serviceProvider, serviceSelector);
            }
        }

        public static void BootstrapInitParams<TPage, TServiceProvider>(TPage page,
            TServiceProvider serviceProvider,
            Func<TServiceProvider, ParameterInfo,  object> serviceSelector)
                where TPage : class
        {
            var onInitMethodInfo = IntrospectionHelper.GetOnInitMethod(page);
            var onInitMethod = onInitMethodInfo?.GetCustomAttribute<OnInitAttribute>();

            if (onInitMethod is null)
            {
                return;
            }

            onInitMethod.Bootstrap(page, onInitMethodInfo, serviceProvider, serviceSelector);
        }
    }
}
