using Injectify.Annotations;
using Injectify.Microsoft.DependencyInjection.Extensions;
using SampleAppV0;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SampleAppV1
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [Injectable]
    public sealed partial class ThirdPage : Page
    {
        private int _number;

        public ThirdPage()
        {
            this.Bootstrap().InitializeComponent();
        }

        public ThirdPage(int number)
        {
            this.Bootstrap().InitializeComponent();
            _number = number;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            var state = this.Frame.GetNavigationState();

            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
            }
        }

        private void GoToMain_Click(object sender, RoutedEventArgs e)
        {
            var state = this.Frame.GetNavigationState();
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
