using System;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Injectify")]
[assembly: InternalsVisibleTo("Injectify.Microsoft.DependencyInjection")]

namespace Injectify.Abstractions
{
    internal interface IOnInit
    {
        void Bootstrap<TPage, TServiceProvider>(TPage page,
            MethodInfo initMethodInfo,
            TServiceProvider serviceProvider,
            Func<TServiceProvider, ParameterInfo, object> serviceSelector)
                where TPage : class;
    }
}
