using System;
using System.Reflection;

namespace Injectify.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TPage"></typeparam>
    /// <typeparam name="TServiceProvider"></typeparam>
    public class InjectionContext<TPage, TServiceProvider>
        where TPage : class
    {
        /// <summary>
        /// 
        /// </summary>
        public TPage Page { get; }

        /// <summary>
        /// 
        /// </summary>
        public TServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 
        /// </summary>
        public Func<TServiceProvider, PropertyInfo, object> GetByPropertyInfo { get; }

        /// <summary>
        /// 
        /// </summary>
        public Func<TServiceProvider, ParameterInfo, object> GetByParameterInfo { get; }

        public InjectionContext(TPage page,
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
