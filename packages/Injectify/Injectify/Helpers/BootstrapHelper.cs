using Injectify.Annotations;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Injectify.Microsoft.DependencyInjection")]

namespace Injectify.Helpers
{
    internal sealed class BootstrapHelper
    {
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
