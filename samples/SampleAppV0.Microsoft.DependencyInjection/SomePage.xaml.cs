using Injectify.Annotations;
using Injectify.Microsoft.DependencyInjection.Extensions;
using SampleAppV0.Services;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace SampleAppV0
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [Injectable]
    public sealed partial class SomePage : Page
    {
        private ISampleService _sampleService;

        public SomePage()
        {
            this.Bootstrap()
                .InitializeComponent();
        }

        // No OnInit annotation - nothing is going to be injected
        public void Init(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        [Inject]
        public ISampleService SampleService { get; set; }

        private void Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainPage));
        }
    }
}
