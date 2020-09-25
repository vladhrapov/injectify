using Injectify.Microsoft.DependencyInjection;
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
        public SomePage()
        {
            this.InitializeComponent();
        }

        [Inject]
        public ISampleService SampleService { get; set; }
    }
}
