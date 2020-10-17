using Injectify.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using SampleAppV0.Services;
using SampleAppV1.Services;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SampleAppV0
{
    // Implement IStartup interface as a Startup for the project
    public sealed class Startup : IStartup<ServiceCollection>
    {
        public void ConfigureServices(ServiceCollection services)
        {
            // Register dependencies
            services.AddScoped<ISampleService, SampleService>();
            services.AddSingleton<IProvider, StorageProvider>();
            services.AddSingleton<IProvider, XmlProvider>();
            services.AddScoped<IFactory<string>, RandomStringFactory>();
        }
    }

    public static class HelperClass
    {
        public static void Navigate<TPage>(this Frame frm)
        {
            var frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(TPage));
            var state = frame.GetNavigationState();
            frame.Content = new SomePage(new RandomStringFactory());
            Window.Current.Activate();
        }

        public static string GetUpdatedState(string state)
        {
            var stateSegments = state.Split(",");
            var one = int.Parse(stateSegments[1]);
            var two = int.Parse(stateSegments[2]);

            if (one > 0 && two > 0)
            {
                stateSegments[1] = $"{one + 1}";
                stateSegments[2] = $"{two + 1}";
            }

            return string.Join(",", stateSegments);
        }
    }
}
