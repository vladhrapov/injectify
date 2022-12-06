using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Injectify")]

namespace Injectify.Abstractions
{
    /// <summary>
    /// Attribute to mark a class that contains members marked with [Inject] attribute.
    /// </summary>
    internal interface IInjectable
    {
        /// <summary>
        /// Bootstrap all class properties and methods marked with [Inject] and [OnInit] attributes.
        /// </summary>
        /// <typeparam name="TPage">Page type (Framework specific).</typeparam>
        /// <typeparam name="TServiceProvider">Service provider type (DI specific).</typeparam>
        /// <param name="context">
        /// Injection context for a page, including service provider, registered services,
        /// service selectors and other required info.
        /// </param>
        void Bootstrap<TPage, TServiceProvider>(
            InjectionContext<TPage, TServiceProvider> context)
                where TPage : class;
    }
}
