using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Injectify")]

namespace Injectify.Abstractions
{
    /// <summary>
    /// Annotates class for injecting members.
    /// </summary>
    internal interface IInjectable
    {
        /// <summary>
        /// Bootstrap class members marked using Inject attribute.
        /// </summary>
        /// <typeparam name="TPage">Page type.</typeparam>
        /// <typeparam name="TServiceProvider">Service provider type.</typeparam>
        /// <param name="context">Injection context for the page, including service provider with registered services and service selectors.</param>
        void Bootstrap<TPage, TServiceProvider>(InjectionContext<TPage, TServiceProvider> context)
            where TPage : class;
    }
}
