using Injectify.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Injectify.Microsoft.DependencyInjection.Extensions
{
    /// <summary>
    /// Set of extensions for the Application class.
    /// </summary>
    public static class ApplicationExtensions
    {
        /// <summary>
        /// Creates a root frame with a bootstrapped Startup with services.
        /// </summary>
        /// <param name="application">Application instance.</param>
        /// <returns>Frame instance with a registered services.</returns>
        public static Frame GetRootFrame(this Application application)
        {
            BootstrapHelper.BootstrapStartup(application, new ServiceCollection(), services => services.BuildServiceProvider());

            var serviceProvider = IntrospectionHelper.GetServiceProviderFromApplication<Application, ServiceProvider>(application);
            var frame = new FrameWithServiceProvider(serviceProvider) as Frame;

            return frame;
        }
    }
}
