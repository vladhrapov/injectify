using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Injectify.Microsoft.DependencyInjection.Extensions
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
        public static object GetByPropertyInfo(this ServiceProvider provider, PropertyInfo propInfo)
        {
            if (propInfo.PropertyType?.GenericTypeArguments?.Any() ?? false)
            {
                return provider.GetServices(propInfo.PropertyType?.GenericTypeArguments?.FirstOrDefault());
            }

            return provider.GetService(propInfo.PropertyType);
        }

        /// <summary>
        /// Get instance for parameter type from registered types in provider.
        /// </summary>
        /// <param name="provider">Service provider instance.</param>
        /// <param name="parameterInfo">Parameter type metadata to search for in service provider.</param>
        /// <returns>Searched type instance.</returns>
        public static object GetByParameterInfo(this ServiceProvider provider, ParameterInfo parameterInfo)
        {
            if (parameterInfo.ParameterType?.GenericTypeArguments?.Any() ?? false)
            {
                return provider.GetServices(parameterInfo.ParameterType?.GenericTypeArguments?.FirstOrDefault());
            }

            return provider.GetService(parameterInfo.ParameterType);
        }
    }
}
