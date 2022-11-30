using Injectify.Abstractions;
using Injectify.Exceptions;
using Injectify.Helpers;
using System;

namespace Injectify.Annotations
{
    /// <summary>
    /// 
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public class InjectableAttribute : Attribute, IInjectable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TPage"></typeparam>
        /// <typeparam name="TServiceProvider"></typeparam>
        /// <param name="context"></param>
        public void Bootstrap<TPage, TServiceProvider>(InjectionContext<TPage, TServiceProvider> context)
            where TPage : class
        {
            if (context.ServiceProvider == null)
            {
                throw new InjectifyException($"'{nameof(context.ServiceProvider)}' should not be null.");
            }

            BootstrapHelper.BootstrapProps(context);

            // UWP does not support instantiation of the page using constructor with any parameters.
            // This is a current limitation of the framework and it's internal implementation
            // of the page factory that creates page instances internally suring frame navigation
            // by using default constructor with no parameters.
            //
            // OnInit is a workaround for injecting dependencies as parameters similar to constructor.
            //
            // Proposal to use navigation with extended frame navigation:
            // https://github.com/microsoft/microsoft-ui-xaml/issues/693
            //
            BootstrapHelper.BootstrapInitParams(context);
        }
    }
}
