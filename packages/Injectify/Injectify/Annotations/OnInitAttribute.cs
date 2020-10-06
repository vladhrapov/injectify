using Injectify.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Injectify.Annotations
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class OnInitAttribute : Attribute, IOnInit
    {
        public void Bootstrap<TPage, TServiceProvider>(InjectionContext<TPage, TServiceProvider> context,
            MethodInfo onInitMethodInfo,
            Func<TServiceProvider, ParameterInfo, object> serviceSelector)
                where TPage : class
        {
            var parameters = onInitMethodInfo.GetParameters();

            if (!parameters.Any())
                return;

            var paramsInstances = new List<object>(parameters.Length);

            foreach (var parameter in parameters)
            {
                var argumentInstance = serviceSelector(context.ServiceProvider, parameter);
                paramsInstances.Add(argumentInstance);
            }

            onInitMethodInfo.Invoke(context.Page, paramsInstances.ToArray());
        }
    }
}
