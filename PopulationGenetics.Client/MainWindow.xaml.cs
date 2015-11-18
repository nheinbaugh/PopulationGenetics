using PopulationGenetics.Common;
using PopulationGenetics.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IMainWindow
    {
        private IWorld _world;
        public IWorld World => _world;

        public MainWindow(IWorld world)
        {
            _world = world;
            InitializeComponent();
            CreateToolbar();
            world.CreateWorldControls(geneGrid);
            world.GeneBank.UpdateVisibleControls(geneGrid);

        }

        private void CreateToolbar()
        {
            
            var tbt = new ToolBarTray();
            var tb = new ToolBar();
            tbt.ToolBars.Add(tb);
            tbt.Height = 30;
            var processTurn = new Button();
            processTurn.Content = "Process Turn";
            processTurn.Click += processTurn_Click;
            var cleanWorld = new Button();
            cleanWorld.Content = "Reset World";
            cleanWorld.Click += cleanWorld_Click;

            tb.Items.Add(processTurn);
            tb.Items.Add(cleanWorld);
            toolbarGrid.Children.Add(tbt);
        }

        private void cleanWorld_Click(object sender, RoutedEventArgs e)
        {
            _world.CleanWorld(false);
            geneGrid.Children.Clear();
            _world.CreateWorldControls(geneGrid);
            _world.SeedWorld(10000);
            _world.CreateWorldControls(geneGrid);
            _world.GeneBank.UpdateVisibleControls(geneGrid);
        }

        private void processTurn_Click(object sender, RoutedEventArgs e)
        {
            _world.ProcessTurn();
        }
    }
}
