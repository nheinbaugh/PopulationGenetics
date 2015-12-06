using PopulationGenetics.Win10.Client.ViewModels;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;

namespace PopulationGenetics.Win10.Client.Views
{
    public sealed partial class EditGenesPage : Page
    {
        public MainPageViewModel ViewModel => this.DataContext as MainPageViewModel;
        public EditGenesPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Disabled;
        }

    }
}