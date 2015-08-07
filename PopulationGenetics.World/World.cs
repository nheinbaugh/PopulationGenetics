using PopulationGenetics.Library.Factories;
using PopulationGenetics.Library.SeedMaterial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library
{
    public class World : IWorld, INotifyPropertyChanged
    {
        private readonly List<IPerson> _population;
        private readonly ILocusBank _registeredGenes;
        private readonly IPersonFactory _personFactory;
        private int _age;
        private readonly IControlManager _controlManager;

        public event PropertyChangedEventHandler PropertyChanged;

        public IPersonFactory PersonFactory { get { return _personFactory; } }
        public List<IPerson> Population { get { return _population; } }
        public ILocusBank RegisteredGenes { get { return _registeredGenes; } }
        public int PopulationSize { get { return _population.Count; } }
        public int Age { get { return _age; } }


        public World(List<IPerson> pop, ILocusBank genes, IPersonFactory personFactory, IControlManager controlManager)
        {
            if (pop.Count > 0) pop.Clear();
            _population = pop;
            _registeredGenes = genes;
            _personFactory = personFactory;
            _controlManager = controlManager;
            WorldSeeds.BaseGenes(_registeredGenes);
            SeedWorld(1000);
        }

        public World(int seedSize)
        {
            SeedWorld(seedSize);
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SeedWorld(int seedSize)
        {
            for (int i = 0; i < seedSize; i++)
            {
                _population.Add(_personFactory.CreateNewPerson());
            }
            NotifyPropertyChanged("PopulationSize");
        }

        public void ProcessTurn()
        {
            _age++;
            foreach (var person in _population)
            {

            }
            NotifyPropertyChanged("PopulationSize");
            NotifyPropertyChanged("Age");
        }

        public void CleanWorld(bool clearGenes)
        {
            _population.Clear();
            if (clearGenes)
            {
                _registeredGenes.Genes.Clear();
            }
            NotifyPropertyChanged("PopulationSize");
        }

        public List<StackPanel> CreateWorldControls(Grid targetGrid)
        {
            var spList = new List<StackPanel>();
            spList.Add(_controlManager.CreateDataPair("pop", "Population", "PopulationSize", this));
            spList.Add(_controlManager.CreateDataPair("age", "World Age", "Age", this));

            foreach (var sp in spList)
            {
                sp.Margin = new Thickness(5, 5, 5, 5);

                sp.HorizontalAlignment = HorizontalAlignment.Right;
                Grid.SetColumn(sp, 1);
                var rd = new RowDefinition { Height = GridLength.Auto };
                targetGrid.RowDefinitions.Add(rd);
                var rows = targetGrid.RowDefinitions.Count;
                Grid.SetRow(sp, rows - 1);
                targetGrid.Children.Add(sp);
            }
            return spList;
        }
    }
}

