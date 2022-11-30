using System.Reflection;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Injectify")]

namespace Injectify.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    internal interface IOnInit
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <typeparam name="TServiceProvider"></typeparam>
        /// <param name="context"></param>
        /// <param name="methodInfo"></param>
        void Bootstrap<TPage, TServiceProvider>(InjectionContext<TPage, TServiceProvider> context, MethodInfo methodInfo)
            where TPage : class;
    }
}
