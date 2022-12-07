using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Injectify")]

namespace Injectify.Abstractions
{
    /// <summary>
    /// Annotates property member of the class for injection of the specified dependency type.
    /// </summary>
    internal interface IInject
    {
        /// <summary>
        /// Bootstrap property marked with [Inject] attribute.
        /// </summary>
        /// <typeparam name="TPage">Page type (Framework specific).</typeparam>
        /// <typeparam name="TServiceProvider">Service provider type (DI specific).</typeparam>
        /// <param name="context">
        /// Injection context for a page, including service provider, registered services,
        /// service selectors and other required info.
        /// </param>
        /// <param name="propInfo">Marked property metadata info.</param>
        void Bootstrap<TPage, TServiceProvider>(
            InjectionContext<TPage, TServiceProvider> context,
            PropertyInfo propInfo)
                where TPage : class;
    }
}
