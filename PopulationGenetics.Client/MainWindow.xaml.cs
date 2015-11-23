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
            var ribbon = new Ribbon();
            ribbon.MinHeight = 100;
            var backstage = new Backstage();
            ribbon.ToolBarItems.Add(backstage);
           //mainGrid.Children.Add(ribbon);


            //var group = new RibbonGroupBox();
            //group.Header = "This is a group box";

            //var processTurn = new Fluent.Button();
            //processTurn.Content = "Process Turn";
            //processTurn.Click += processTurn_Click;
            //var cleanWorld = new Fluent.Button();
            //cleanWorld.Content = "Reset World";
            //cleanWorld.Click += cleanWorld_Click;
            //group.Items.Add(processTurn);
            //group.Items.Add(cleanWorld);
            //mainTab.Groups.Add(group);



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
