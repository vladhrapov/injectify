namespace Injectify.Abstractions
{
    /// <summary>
    /// UWP application interface for the app bootstrap.
    /// </summary>
    /// <typeparam name="TServiceProvider">Service provider type from DI library.</typeparam>
    public interface IUwpApplication<TServiceProvider>
    {
        /// <summary>
        /// Service provider with registered services.
        /// </summary>
        TServiceProvider Services { get; set; }
    }
}
