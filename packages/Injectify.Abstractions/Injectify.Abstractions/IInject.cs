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
        /// <param name="context"></param>
        /// <param name="propInfo"></param>
        void Bootstrap<TPage, TServiceProvider>(InjectionContext<TPage, TServiceProvider> context, PropertyInfo propInfo)
            where TPage : class;
    }
}
