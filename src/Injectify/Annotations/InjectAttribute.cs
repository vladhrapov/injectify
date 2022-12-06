using Injectify.Abstractions;
using System;
using System.Reflection;

namespace Injectify.Annotations
{
    /// <inheritdoc cref="Injectify.Abstractions.IInject"/>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class InjectAttribute : Attribute, IInject
    {
        /// <inheritdoc cref="Injectify.Abstractions.IInject
        ///     .Bootstrap{TPage, TServiceProvider}(InjectionContext{TPage, TServiceProvider}, PropertyInfo)"/>
        public void Bootstrap<TPage, TServiceProvider>(
            InjectionContext<TPage, TServiceProvider> context,
            PropertyInfo propInfo)
                where TPage : class
        {
            var propertyInstance = context.GetByPropertyInfo(context.ServiceProvider, propInfo);
            propInfo.SetValue(context.Page, propertyInstance);
        }
    }
}
