using Injectify.Annotations;
using Injectify.Microsoft.DependencyInjection.Extensions;
using SampleAppV0.Services;
using SampleAppV1;
using SampleAppV1.Services;
using Windows.UI.Xaml;
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
        private IFactory<string> _factory;
        private string _randomString;

        public SomePage()
        {
            this.Bootstrap()
                .InitializeComponent();
        }

        public SomePage(IFactory<string> factory)
        {
            this.Bootstrap().InitializeComponent();
            _factory = factory;
            _randomString = _factory.Get();


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
            //var state = this.Frame.GetNavigationState();
            //this.Frame.Navigate(typeof(ThirdPage));




            var frame = Window.Current.Content as Frame;
            frame.Navigate(typeof(ThirdPage));
            frame.Content = new ThirdPage(33333);
            var state = frame.GetNavigationState();
            //var updatedState = HelperClass.GetUpdatedState(state);
            //frame.SetNavigationState(updatedState);
            Window.Current.Activate();


        }
    }
}
