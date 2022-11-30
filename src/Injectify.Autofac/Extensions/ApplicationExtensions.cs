using Autofac;
using Injectify.Helpers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Injectify.Autofac.Extensions
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
            BootstrapHelper.BootstrapStartup(application, new ContainerBuilder(), services => services.Build());

            var serviceProvider = IntrospectionHelper.GetServiceProviderFromApplication<Application, IContainer>(application);
            var frame = new FrameWithServiceProvider(serviceProvider) as Frame;

            return frame;
        }
    }
}
