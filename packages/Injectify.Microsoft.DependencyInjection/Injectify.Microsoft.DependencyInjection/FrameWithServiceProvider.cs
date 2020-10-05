using System;
using System.Reflection;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Injectify.Microsoft.DependencyInjection
{
    internal class FrameWithServiceProvider<TServiceProvider> : Frame
        where TServiceProvider : class
    {
        private readonly TServiceProvider _serviceProvider;

        public FrameWithServiceProvider(TServiceProvider serviceProvider)
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

                classInjectable.Bootstrap(e.Content);
            }
        }
    }
}
