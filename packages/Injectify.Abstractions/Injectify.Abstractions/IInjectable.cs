using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Injectify")]
[assembly: InternalsVisibleTo("Injectify.Microsoft.DependencyInjection")]

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
        /// <param name="page">UWP page instance.</param>
        void Bootstrap<TPage>(TPage page);
    }
}
