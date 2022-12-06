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
    /// Introspection helpers for inspecting metadata.
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
        /// Introspects assembly for any types that implement
        /// <see cref="Injectify.Abstractions.IStartup{TServiceCollection}"/> type.
        /// </summary>
        /// <typeparam name="TServiceCollection">Service provider type (DI specific).</typeparam>
        /// <returns>First type that implements <see cref="Injectify.Abstractions.IStartup{TServiceCollection}"/></returns>
        [Pure]
        public static Type GetStartupType<TServiceCollection>()
            where TServiceCollection : class =>
                Assembly.GetEntryAssembly().GetTypes()
                    .Where(type => type.IsClass && DoesImplementInterface<TServiceCollection>(type, typeof(IStartup<>)))
                    .FirstOrDefault();

        /// <summary>
        /// Introspects application type for a service provider property.
        /// </summary>
        /// <typeparam name="TServiceProvider">Service provider type (DI specific).</typeparam>
        /// <param name="applicationInstance">Application type instance.</param>
        /// <returns>Service provider instance.</returns>
        [Pure]
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
        /// Introspects assembly for a service provider property inside app type.
        /// </summary>
        /// <typeparam name="TApplication">Application type to introspect.</typeparam>
        /// <typeparam name="TServiceProvider">Service provider type (DI specific).</typeparam>
        /// <returns>Service provider property metadata.</returns>
        [Pure]
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
                    $"There is no class that inherits base '{typeof(TApplication).Name}' class" +
                    $"or does not implement '{typeof(IUwpApplication<>).Name}'!");
            }

            var serviceProviderPropInfo = uwpAppClass.GetProperties()
                .Where(p => p.PropertyType == typeof(TServiceProvider))
                .FirstOrDefault();

            return serviceProviderPropInfo;
        }

        /// <summary>
        /// Introspects assembly for a method marked with <see cref="Injectify.Annotations.OnInitAttribute"/> inside the page.
        /// </summary>
        /// <typeparam name="TPage">Page type (Framework specific).</typeparam>
        /// <param name="page">Page instance.</param>
        /// <returns>Method metadata.</returns>
        [Pure]
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

        [Pure]
        private static bool DoesImplementUwpApplicationInterface<TServiceProvider>(Type type)
            where TServiceProvider : class =>
                DoesImplementInterface<TServiceProvider>(type, typeof(IUwpApplication<>));

        [Pure]
        private static bool DoesImplementInterface<TGenericArgument>(Type type, Type interfaceType)
            where TGenericArgument : class =>
                type.GetInterfaces()
                .Where(i => i.IsGenericType &&
                    i.GetGenericTypeDefinition() == interfaceType &&
                    i.GenericTypeArguments?.FirstOrDefault() == typeof(TGenericArgument))
                .Any();
    }
}
