using PopulationGenetics.Win10.Client.ViewModels;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Controls;

namespace PopulationGenetics.Win10.Client.Views
{
    public sealed partial class MenuPage : Page
    {
        public MenuPageViewModel ViewModel => DataContext as MenuPageViewModel;
        public MenuPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Disabled;
        }

        private void editGenes_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {

        }
    }
}
