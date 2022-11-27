using Autofac;
using System.Linq;
using System.Reflection;

namespace Injectify.Autofac.Extensions
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
        public static object GetByPropertyInfo(this IContainer provider, PropertyInfo propInfo) =>
            provider.Resolve(propInfo.PropertyType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="parameterInfo"></param>
        /// <returns></returns>
        public static object GetByParameterInfo(this IContainer provider, ParameterInfo parameterInfo) =>
            provider.Resolve(parameterInfo.ParameterType);
    }
}
