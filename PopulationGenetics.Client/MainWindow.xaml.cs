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

namespace PopulationGenetics.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IWorld _world;
        public IWorld World { get { return _world; } set { _world = value; } }

        public MainWindow(IWorld world)
        {
            this._world = world;
            InitializeComponent();
            var bob = world.CreateControls();
            geneGrid.Children.Add(bob);
            PopulateTextBoxes();

        }

        private void PopulateTextBoxes()
        {
            aPopBox.Text = _world.Population.AsQueryable()
                    .Where(a => a.Genes[0].Representation == "A").ToList().Count.ToString();
            bPopBox.Text = _world.Population.AsQueryable()
                    .Where(a => a.Genes[0].Representation == "B").ToList().Count.ToString();
            oPopBox.Text = _world.Population.AsQueryable()
                    .Where(a => a.Genes[0].Representation == "O").ToList().Count.ToString();
            abPopBox.Text = _world.Population.AsQueryable()
                    .Where(a => a.Genes[0].Representation.Length == 2).ToList().Count.ToString();
            ageBox.Text = _world.Age.ToString();
        }

        private void cleanWorld_Click(object sender, RoutedEventArgs e)
        {
            _world.CleanWorld(false);
            PopulateTextBoxes();
        }

        private void populateWorld_Click(object sender, RoutedEventArgs e)
        {
            if(_world.PopulationSize > 0)
            {
                MessageBox.Show("Please clear the world before attempting to repopulate.");
                return;
            }
            _world.SeedWorld(1000);
            PopulateTextBoxes();
        }

        private void processTurn_Click(object sender, RoutedEventArgs e)
        {
            _world.ProcessTurn();
            PopulateTextBoxes();
            geneGrid.RowDefinitions.Add(new RowDefinition());
        }
    }
}
