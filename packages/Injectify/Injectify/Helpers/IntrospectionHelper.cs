using Injectify.Abstractions;
using Injectify.Annotations;
using Injectify.Exceptions;
using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Injectify.Microsoft.DependencyInjection")]

namespace Injectify.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class IntrospectionHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TServiceCollection"></typeparam>
        /// <typeparam name="TServiceProvider"></typeparam>
        /// <returns></returns>
        [Pure]
        public static Type GetAppType<TServiceCollection, TServiceProvider>()
            where TServiceCollection : class
            where TServiceProvider : class =>
                Assembly.GetEntryAssembly().GetTypes()
                    .Where(type => type.IsClass && DoesImplementUwpApplicationInterface<TServiceProvider>(type))
                    .FirstOrDefault();

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
        public static TServiceProvider GetServiceProviderFromApplication<TServiceProvider>(object applicationInstance)
            where TServiceProvider : class
        {
            var applicationClass = Assembly.GetEntryAssembly().GetTypes()
                .Where(type => type.IsClass && DoesImplementUwpApplicationInterface<TServiceProvider>(type))
                .FirstOrDefault();

            var servicesProperty = applicationClass.GetProperties()
                .Where(pi => pi.PropertyType == typeof(TServiceProvider))
                .FirstOrDefault();

            return servicesProperty.GetValue(applicationInstance) as TServiceProvider;
        }

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
        public static bool DoesImplementUwpApplicationInterface<TServiceProvider>(Type type)
            where TServiceProvider : class =>
                DoesImplementInterface<TServiceProvider>(type, typeof(IUwpApplication<>));

        private static bool DoesImplementInterface<TGenericArgument>(Type type, Type interfaceType)
            where TGenericArgument : class =>
                type.GetInterfaces()
                .Where(i => i.IsGenericType
                    && i.GetGenericTypeDefinition() == interfaceType
                    && i.GenericTypeArguments?.FirstOrDefault() == typeof(TGenericArgument))
                .Any();
    }
}
