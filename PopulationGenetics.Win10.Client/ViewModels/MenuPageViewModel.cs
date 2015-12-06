using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PopulationGenetics.Win10.Client.ViewModels
{
    public class MenuPageViewModel : PopulationGenetics.Win10.Client.Mvvm.ViewModelBase
    {
        public MenuPageViewModel()
        {

        }

        public void StartSim_Click()
        {
            NavigationService.Navigate(typeof(Views.MainPage), "We Came from the start sim button");
        }
        public void EditGenes_Click()
        {
            NavigationService.Navigate(typeof(Views.EditGenesPage));
        }
    }
}
