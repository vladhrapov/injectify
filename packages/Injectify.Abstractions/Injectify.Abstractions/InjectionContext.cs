namespace Injectify.Abstractions
{
    public class InjectionContext<TPage, TServiceProvider>
        where TPage : class
    {
        public TPage Page { get; }
        public TServiceProvider ServiceProvider { get; }

        public InjectionContext(TPage page, TServiceProvider serviceProvider)
        {
            Page = page;
            ServiceProvider = serviceProvider;
        }
    }
}
