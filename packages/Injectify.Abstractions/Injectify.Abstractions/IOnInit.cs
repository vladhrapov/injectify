using System;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Injectify")]
[assembly: InternalsVisibleTo("Injectify.Microsoft.DependencyInjection")]

namespace Injectify.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IOnInit
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <typeparam name="TServiceProvider"></typeparam>
        /// <param name="context"></param>
        /// <param name="initMethodInfo"></param>
        /// <param name="serviceSelector"></param>
        void Bootstrap<TPage, TServiceProvider>(InjectionContext<TPage, TServiceProvider> context,
            MethodInfo initMethodInfo,
            Func<TServiceProvider, ParameterInfo, object> serviceSelector)
                where TPage : class;
    }
}
