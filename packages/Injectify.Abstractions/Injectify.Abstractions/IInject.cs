using System.Reflection;
using System.Runtime.CompilerServices;

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
        /// <param name="serviceProvider"></param>
        /// <param name="propInfo"></param>
        void Bootstrap<TPage, TServiceProvider>(TPage page, TServiceProvider serviceProvider, PropertyInfo propInfo);
    }
}
