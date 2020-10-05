using Injectify.Abstractions;
using Injectify.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Injectify.Annotations
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class OnInitAttribute : Attribute, IOnInit
    {
        public void Bootstrap<TPage, TServiceProvider>(TPage page,
            MethodInfo onInitMethodInfo,
            TServiceProvider serviceProvider,
            Func<TServiceProvider, ParameterInfo, object> serviceSelector)
                where TPage : class
        {
            var parameters = onInitMethodInfo.GetParameters();

            if (!parameters.Any())
                return;

            var paramsInstances = new List<object>(parameters.Length);

            foreach (var parameter in parameters)
            {
                var argumentInstance = serviceSelector(serviceProvider, parameter);
                paramsInstances.Add(argumentInstance);
            }

            onInitMethodInfo.Invoke(page, paramsInstances.ToArray());
        }
    }
}
