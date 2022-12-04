using Injectify.Annotations;
using Injectify.Autofac.Extensions;
using SampleAppV0.AutofacDI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SampleAppV0.AutofacDI
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
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

        [OnInit]
        public void OnInit(ISampleService sampleService, IEnumerable<IProvider> providers)
        {
            _sampleService = sampleService;
            _providers = providers;
        }

        [Inject]
        public ISampleService SampleService { get; set; }

        [Inject]
        public IEnumerable<IProvider> Providers { get; set; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(SamplePage));
        }
    }
}
