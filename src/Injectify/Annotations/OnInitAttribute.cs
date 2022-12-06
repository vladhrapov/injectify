using Injectify.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Injectify.Annotations
{
    /// <inheritdoc cref="Injectify.Abstractions.IOnInit"/>
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public sealed class OnInitAttribute : Attribute, IOnInit
    {
        /// <inheritdoc cref="Injectify.Abstractions.IOnInit
        ///     .Bootstrap{TPage, TServiceProvider}(InjectionContext{TPage, TServiceProvider}, MethodInfo)"/>
        public void Bootstrap<TPage, TServiceProvider>(
            InjectionContext<TPage, TServiceProvider> context,
            MethodInfo methodInfo)
                where TPage : class
        {
            // OnInit method info
            var parameters = methodInfo.GetParameters();

            if (!parameters.Any())
                return;

            var paramsInstances = new List<object>(parameters.Length);

            foreach (var parameter in parameters)
            {
                var argumentInstance = context.GetByParameterInfo(context.ServiceProvider, parameter);
                paramsInstances.Add(argumentInstance);
            }

            methodInfo.Invoke(context.Page, paramsInstances.ToArray());
        }
    }
}
