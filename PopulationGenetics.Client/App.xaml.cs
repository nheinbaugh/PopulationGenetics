using Ninject;
using PopulationGenetics.Common;
using PopulationGenetics.Library;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PopulationGenetics.Client
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
         IKernel container;
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            ConfigureContainer();
            ComposeObjects();
            Current.MainWindow.Show();
        }

        private void ConfigureContainer()
        {
            container = new StandardKernel(new GeneticsModule());
            container.Bind<MainWindow>().ToSelf();
        }

        private void ComposeObjects()
        {
            Current.MainWindow = container.Get<MainWindow>();
        }
    }
}
