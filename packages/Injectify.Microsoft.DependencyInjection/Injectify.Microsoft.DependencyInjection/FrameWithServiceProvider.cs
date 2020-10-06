using Injectify.Abstractions;
using Injectify.Annotations;
using Injectify.Microsoft.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Injectify.Microsoft.DependencyInjection
{
    internal class FrameWithServiceProvider : Frame
    {
        private readonly ServiceProvider _serviceProvider;

        public FrameWithServiceProvider(ServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            this.Navigated += OnFrameNavigated;
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

                //var serviceProvider = IntrospectionHelper.GetServiceProviderFromApplication<ServiceProvider>(Application.Current);
                var context = new InjectionContext<Page, ServiceProvider>(e.Content as Page,
                    _serviceProvider,
                    ServiceProviderExtensions.GetByPropertyInfo,
                    ServiceProviderExtensions.GetByParameterInfo);

                classInjectable.Bootstrap(context);
            }
        }
    }
}
