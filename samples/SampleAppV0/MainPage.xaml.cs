using Injectify.Annotations;
using Injectify.Microsoft.DependencyInjection.Extensions;
using SampleAppV0.Services;
using System.Collections.Generic;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace SampleAppV0
{
    // Mark class as Injectable
    [Injectable]
    public sealed partial class MainPage : Page
    {
        private ISampleService _sampleService;
        private IEnumerable<IProvider> _providers;

        public MainPage()
        {
            this.Bootstrap()
                .InitializeComponent();
        }

        // Mark method using on init and inject dependencies for initialize
        [OnInit]
        private void Init(ISampleService sampleService, IEnumerable<IProvider> providers)
        {
            _sampleService = sampleService;
            _providers = providers;
        }

        // Mark property with Inject
        [Inject]
        public ISampleService SampleService { get; set; }

        // Mark property with Inject
        [Inject]
        public IEnumerable<IProvider> Providers { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SomePage));
        }
    }
}
