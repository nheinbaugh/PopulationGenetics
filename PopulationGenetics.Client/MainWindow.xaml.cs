﻿using PopulationGenetics.Common;
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
        public IWorld World { get { return _world; } set { _world = value; } }

        public MainWindow(IWorld world)
        {
            _world = world;
            InitializeComponent();
            world.CreateWorldControls(geneGrid);
            world.GeneBank.UpdateVisibleControls(geneGrid);

        }

        private void cleanWorld_Click(object sender, RoutedEventArgs e)
        {
            _world.CleanWorld(false);
            geneGrid.Children.Clear();
            _world.CreateWorldControls(geneGrid);
            populateWorld.IsEnabled = true;
            processTurn.IsEnabled = false;
            cleanWorld.IsEnabled = false;

        }

        private void populateWorld_Click(object sender, RoutedEventArgs e)
        {
            if(_world.Population.Populus.Count> 0)
            {
                MessageBox.Show("Please clear the world before attempting to repopulate.");
                return;
            }
            _world.SeedWorld(10000);
            _world.CreateWorldControls(geneGrid);
            _world.GeneBank.UpdateVisibleControls(geneGrid);
            processTurn.IsEnabled = true;
            populateWorld.IsEnabled = false;
            cleanWorld.IsEnabled = true;
        }

        private void processTurn_Click(object sender, RoutedEventArgs e)
        {
            _world.ProcessTurn();
        }
    }
}
