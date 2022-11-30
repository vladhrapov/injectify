namespace Injectify.Abstractions
{
    /// <summary>
    /// Register dependencies for the application startup.
    /// </summary>
    /// <typeparam name="TServiceCollection">Service collection type.</typeparam>
    public interface IStartup<TServiceCollection>
    {
        /// <summary>
        /// Configure services.
        /// </summary>
        /// <param name="services">Collection of the registered services.</param>
        void ConfigureServices(TServiceCollection services);
    }
}
