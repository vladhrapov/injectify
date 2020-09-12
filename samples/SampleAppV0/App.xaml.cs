using Injectify.Abstractions;
using Injectify.Microsoft.DependencyInjection;
using Injectify.Microsoft.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace SampleAppV0
{
    // Mark application with bootstrap attribute
    [UwpApplicationBootstrap]
    sealed partial class App : Application, IUwpApplication<ServiceProvider> // Implement IUwpApplication 
    {
        public App()
        {
            this.InitializeComponent();
            this.Suspending += OnSuspending;
            this.BootstrapStartup(); // Run Startup Bootstrap
        }

        public ServiceProvider Services { get; set; }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
            Frame rootFrame = Window.Current.Content as Frame;

            if (rootFrame == null)
            {
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    rootFrame.Navigate(typeof(MainPage), e.Arguments);
                }
                Window.Current.Activate();
            }
        }

        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }

        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();

            deferral.Complete();
        }
    }
}
