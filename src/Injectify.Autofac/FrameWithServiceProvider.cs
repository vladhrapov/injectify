using Autofac;
using Injectify.Abstractions;
using Injectify.Annotations;
using Injectify.Autofac.Extensions;
using System;
using System.Reflection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Injectify.Autofac
{
    /// <summary>
    /// Frame class with service provider extends basic Frame control.
    /// </summary>
    internal class FrameWithServiceProvider : Frame
    {
        private readonly IContainer _serviceProvider;

        /// <summary>
        /// Constructor with service provider parameter.
        /// </summary>
        /// <param name="serviceProvider">Service provider instance.</param>
        /// <exception cref="ArgumentNullException">Exception thrown for null parameter.</exception>
        public FrameWithServiceProvider(IContainer serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
        }

        protected virtual void OnFrameNavigated(object sender, NavigationEventArgs e)
        {
            var classInjectable = e.SourcePageType.GetCustomAttribute<InjectableAttribute>();

            // No need to throw when class was not marked as Injectable.
            // Just do not need to bootstrap dependencies.
            if (classInjectable != null)
            {
                if (!(e.Content is Page))
                    throw new InvalidCastException($"'{e.Content.GetType().Name}' is not assignable to '{typeof(Page).Name}'");

                var context = new InjectionContext<Page, IContainer>(e.Content as Page,
                    _serviceProvider,
                    ServiceProviderExtensions.GetByPropertyInfo,
                    ServiceProviderExtensions.GetByParameterInfo);

                classInjectable.Bootstrap(context);
            }
        }
    }
}
