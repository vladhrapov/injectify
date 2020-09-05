namespace Injectify.Abstractions
{
    /// <summary>
    /// Register dependencies for the application startup.
    /// </summary>
    /// <typeparam name="TServiceCollection">Service collection type.</typeparam>
    /// <typeparam name="TServiceProvider">Service provider with registered services.</typeparam>
    public interface IStartup<TServiceCollection, TServiceProvider>
    {
        /// <summary>
        /// Service provider with a list of registered services.
        /// </summary>
        TServiceProvider Services { get; }

        /// <summary>
        /// Configure services.
        /// </summary>
        /// <param name="services">Collection of the registered services.</param>
        /// <returns>Service provider with registered services.</returns>
        TServiceProvider ConfigureServices(TServiceCollection services);
    }
}
