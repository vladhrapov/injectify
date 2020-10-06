using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace Injectify.Microsoft.DependencyInjection.Extensions
{
    /// <summary>
    /// 
    /// </summary>
    internal static class ServiceProviderExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="propInfo"></param>
        /// <returns></returns>
        public static object GetByPropertyInfo(this ServiceProvider provider, PropertyInfo propInfo)
        {
            if (propInfo.PropertyType?.GenericTypeArguments?.Any() ?? false)
            {
                return provider.GetServices(propInfo.PropertyType?.GenericTypeArguments?.FirstOrDefault());
            }

            return provider.GetService(propInfo.PropertyType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="parameterInfo"></param>
        /// <returns></returns>
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
