using Autofac;
using System.Linq;
using System.Reflection;

namespace Injectify.Autofac.Extensions
{
    /// <summary>
    /// Service provider extensions for a DI Framework.
    /// </summary>
    internal static class ServiceProviderExtensions
    {
        /// <summary>
        /// Get instance for property type from registered types in provider.
        /// </summary>
        /// <param name="provider">Service provider instance.</param>
        /// <param name="propInfo">Property type metadata to search for in service provider.</param>
        /// <returns>Searched type instance.</returns>
        public static object GetByPropertyInfo(this IContainer provider, PropertyInfo propInfo) =>
            provider.Resolve(propInfo.PropertyType);

        /// <summary>
        /// Get instance for parameter type from registered types in provider.
        /// </summary>
        /// <param name="provider">Service provider instance.</param>
        /// <param name="parameterInfo">Parameter type metadata to search for in service provider.</param>
        /// <returns>Searched type instance.</returns>
        public static object GetByParameterInfo(this IContainer provider, ParameterInfo parameterInfo) =>
            provider.Resolve(parameterInfo.ParameterType);
    }
}
