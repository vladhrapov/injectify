using Injectify.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using SampleAppV0.Services;

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
        }
    }
}
