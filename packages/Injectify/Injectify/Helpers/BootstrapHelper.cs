using Injectify.Abstractions;
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
        public static void BootstrapProps<TPage, TServiceProvider>(InjectionContext<TPage, TServiceProvider> context,
            Func<TServiceProvider, PropertyInfo, object> serviceSelector)
                where TPage : class
        {
            var injectPropsInfo = context.Page
                .GetType()
                .GetProperties()
                .Where(pi => pi.GetCustomAttribute<InjectAttribute>() != null)
                .ToArray();

            if (!injectPropsInfo.Any())
                return;

            foreach (var propInfo in injectPropsInfo)
            {
                var inject = propInfo.GetCustomAttribute<InjectAttribute>();
                inject.Bootstrap(context, propInfo, serviceSelector);
            }
        }

        public static void BootstrapInitParams<TPage, TServiceProvider>(InjectionContext<TPage, TServiceProvider> context,
            Func<TServiceProvider, ParameterInfo,  object> serviceSelector)
                where TPage : class
        {
            var onInitMethodInfo = IntrospectionHelper.GetOnInitMethod(context.Page);
            var onInitMethod = onInitMethodInfo?.GetCustomAttribute<OnInitAttribute>();

            if (onInitMethod is null)
            {
                return;
            }

            onInitMethod.Bootstrap(context, onInitMethodInfo, serviceSelector);
        }
    }
}
