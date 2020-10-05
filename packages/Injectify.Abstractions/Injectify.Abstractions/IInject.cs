using System;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Injectify")]
[assembly: InternalsVisibleTo("Injectify.Microsoft.DependencyInjection")]

namespace Injectify.Abstractions
{
    /// <summary>
    /// Annotates member for injection of the specified dependency type.
    /// </summary>
    internal interface IInject
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <typeparam name="TServiceProvider"></typeparam>
        /// <param name="page"></param>
        /// <param name="propInfo"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="serviceSelector"></param>
        void Bootstrap<TPage, TServiceProvider>(TPage page,
            PropertyInfo propInfo,
            TServiceProvider serviceProvider,
            Func<TServiceProvider, PropertyInfo, object> serviceSelector);
    }
}
