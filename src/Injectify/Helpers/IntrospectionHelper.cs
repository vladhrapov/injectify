using Injectify.Abstractions;
using Injectify.Annotations;
using Injectify.Exceptions;
using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Injectify.Autofac")]
[assembly: InternalsVisibleTo("Injectify.Microsoft.DependencyInjection")]

namespace Injectify.Helpers
{
    /// <summary>
    /// Set of types introspection helpers.
    /// </summary>
    internal sealed class IntrospectionHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TServiceCollection"></typeparam>
        /// <typeparam name="TServiceProvider"></typeparam>
        /// <returns></returns>
        //[Pure]
        //public static Type GetAppType<TServiceCollection, TServiceProvider>()
        //    where TServiceCollection : class
        //    where TServiceProvider : class =>
        //        Assembly.GetEntryAssembly().GetTypes()
        //            .Where(type => type.IsClass && DoesImplementUwpApplicationInterface<TServiceProvider>(type))
        //            .FirstOrDefault();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TServiceCollection"></typeparam>
        /// <returns></returns>
        [Pure]
        public static Type GetStartupType<TServiceCollection>()
            where TServiceCollection : class =>
                Assembly.GetEntryAssembly().GetTypes()
                    .Where(type => type.IsClass && DoesImplementInterface<TServiceCollection>(type, typeof(IStartup<>)))
                    .FirstOrDefault();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TServiceProvider"></typeparam>
        /// <param name="applicationInstance"></param>
        /// <returns></returns>
        public static TServiceProvider GetServiceProviderFromApplication<TApplication, TServiceProvider>(object applicationInstance)
            where TApplication : class
            where TServiceProvider : class
        {
            var serviceProviderPropInfo = GetServiceProviderProperty<TApplication, TServiceProvider>();

            if (serviceProviderPropInfo is null)
            {
                throw new InjectifyException(
                    $"App type does not implement {typeof(IUwpApplication<>)}. Service provider property is absent.");
            }

            return serviceProviderPropInfo.GetValue(applicationInstance) as TServiceProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TApplication"></typeparam>
        /// <typeparam name="TServiceProvider"></typeparam>
        /// <returns></returns>
        public static PropertyInfo GetServiceProviderProperty<TApplication, TServiceProvider>()
            where TApplication : class
            where TServiceProvider : class
        {
            var uwpAppClass = Assembly.GetEntryAssembly()
                .GetTypes()
                .Where(t => t.IsClass &&
                    (t.BaseType == typeof(TApplication)) &&
                    DoesImplementUwpApplicationInterface<TServiceProvider>(t)) // ToDo: Maybe better to check whether implements IUwpApplication
                .FirstOrDefault();

            if (uwpAppClass is null)
            {
                throw new InjectifyException(
                    $"There is no class that inherits base '{typeof(TApplication).Name}' class or does not implement '{typeof(IUwpApplication<>).Name}'!");
            }

            var serviceProviderPropInfo = uwpAppClass.GetProperties()
                .Where(p => p.PropertyType == typeof(TServiceProvider))
                .FirstOrDefault();

            return serviceProviderPropInfo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <param name="page"></param>
        /// <returns></returns>
        public static MethodInfo GetOnInitMethod<TPage>(TPage page)
            where TPage : class
        {
            var onInitMethods = page.GetType()
                .GetMethods(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                .Where(mi => mi.GetCustomAttribute<OnInitAttribute>() != null)
                .ToArray();

            if (onInitMethods.Length > 1)
            {
                throw new InjectifyException("Only one method with OnInit annotation is allowed.");
            }

            return onInitMethods.FirstOrDefault();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TServiceProvider"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        [Pure]
        public static bool DoesImplementUwpApplicationInterface<TServiceProvider>(Type type)
            where TServiceProvider : class =>
                DoesImplementInterface<TServiceProvider>(type, typeof(IUwpApplication<>));

        [Pure]
        private static bool DoesImplementInterface<TGenericArgument>(Type type, Type interfaceType)
            where TGenericArgument : class =>
                type.GetInterfaces()
                .Where(i => i.IsGenericType
                    && i.GetGenericTypeDefinition() == interfaceType
                    && i.GenericTypeArguments?.FirstOrDefault() == typeof(TGenericArgument))
                .Any();
    }
}
