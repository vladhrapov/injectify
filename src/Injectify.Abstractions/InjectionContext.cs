using System;
using System.Reflection;

namespace Injectify.Abstractions
{
    /// <summary>
    /// Injection context for a page, including service provider, registered services,
    /// service selectors and other required info.
    /// </summary>
    /// <typeparam name="TPage">Page type (Framework specific).</typeparam>
    /// <typeparam name="TServiceProvider">Service provider type (DI specific).</typeparam>
    public class InjectionContext<TPage, TServiceProvider>
        where TPage : class
    {
        /// <summary>
        /// Page instance of a framework specific type.
        /// </summary>
        public TPage Page { get; }

        /// <summary>
        /// Service provider type instance.
        /// </summary>
        public TServiceProvider ServiceProvider { get; }

        /// <summary>
        /// DI specific selector func for a property type.
        /// </summary>
        public Func<TServiceProvider, PropertyInfo, object> GetByPropertyInfo { get; }

        /// <summary>
        /// DI specific selector func for a parameter type.
        /// </summary>
        public Func<TServiceProvider, ParameterInfo, object> GetByParameterInfo { get; }

        /// <summary>
        /// Public constructor with required parameters.
        /// </summary>
        /// <param name="page">Page instance of a framework specific type.</param>
        /// <param name="serviceProvider">Service provider type instance.</param>
        /// <param name="getByPropertyInfo">DI specific selector func for a property type.</param>
        /// <param name="getByParameterInfo">DI specific selector func for a parameter type.</param>
        public InjectionContext(
            TPage page,
            TServiceProvider serviceProvider,
            Func<TServiceProvider, PropertyInfo, object> getByPropertyInfo,
            Func<TServiceProvider, ParameterInfo, object> getByParameterInfo)
        {
            Page = page;
            ServiceProvider = serviceProvider;
            GetByPropertyInfo = getByPropertyInfo;
            GetByParameterInfo = getByParameterInfo;
        }
    }
}
