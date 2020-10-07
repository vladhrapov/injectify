using Injectify.Microsoft.DependencyInjection;
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
        public MainPage()
        {
            this.InitializeComponent();
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
