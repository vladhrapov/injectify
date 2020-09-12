using Injectify.Microsoft.DependencyInjection;
using Injectify.Microsoft.DependencyInjection.Extensions;
using SampleAppV0.Services;
using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace SampleAppV0
{
    // Mark class as Injectable
    [Injectable]
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            Bootstraper.Bootstrap(this); // Run bootstrap for current page
            this.InitializeComponent();
        }

        // Mark property with Inject
        [Inject]
        public ISampleService SampleService { get; set; }

        // Mark property with Inject
        [Inject]
        public IEnumerable<IProvider> Providers { get; set; }
    }
}
