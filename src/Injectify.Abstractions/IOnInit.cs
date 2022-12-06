using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Injectify")]

namespace Injectify.Abstractions
{
    /// <summary>
    /// Annotates method of the class for parameters injection of the specified dependency types.
    /// </summary>
    internal interface IOnInit
    {
        /// <summary>
        /// Bootstrap method marked with [OnInit] attribute.
        /// </summary>
        /// <typeparam name="TPage">Page type (Framework specific).</typeparam>
        /// <typeparam name="TServiceProvider">Service provider type (DI specific).</typeparam>
        /// <param name="context">
        /// Injection context for a page, including service provider, registered services,
        /// service selectors and other required info.
        /// </param>
        /// <param name="methodInfo">Marked method metadata info.</param>
        void Bootstrap<TPage, TServiceProvider>(
            InjectionContext<TPage, TServiceProvider> context,
            MethodInfo methodInfo)
                where TPage : class;
    }
}
