using System.Windows;
using System.Windows.Controls;
using PopulationGenetics.Library.Interfaces;
using Fluent;

namespace PopulationGenetics.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : RibbonWindow, IMainWindow
    {
        private IWorld _world;
        public IWorld World => _world;

        public MainWindow(IWorld world)
        {
            _world = world;
            InitializeComponent();
            CreateRibbon();
            world.CreateWorldControls(geneGrid);
            world.GeneBank.UpdateVisibleControls(geneGrid);

        }

        private void CreateRibbon()
        {
            processTurn.Click += processTurn_Click;
            resetWorld.Click += cleanWorld_Click;
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
