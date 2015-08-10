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
        private readonly IPopulation _population;
        private readonly ILocusBank _registeredGenes;
        private readonly IPersonFactory _personFactory;
        private int _age;
        private readonly IControlManager _controlManager;

        public event PropertyChangedEventHandler PropertyChanged;

        public IPersonFactory PersonFactory => _personFactory;
        public IPopulation Population => _population;
        public ILocusBank RegisteredGenes => _registeredGenes;
        public int Age => _age;


        public World(IPopulation pop, ILocusBank genes, IPersonFactory personFactory, IControlManager controlManager)
        {
            if (pop?.Populus?.Count > 0) pop.Populus.Clear();
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void SeedWorld(int seedSize)
        {
            _population.CreatePopulation(seedSize);
        }

        public void ProcessTurn()
        {
            _age++;
            foreach (var person in _population.Populus)
            {


            }
            NotifyPropertyChanged("Age");
        }

        public void CleanWorld(bool clearGenes)
        {
            _population.DestroyPopulation();
            if (clearGenes)
            {
                _registeredGenes.Loci.Clear();
            }
        }

        public List<StackPanel> CreateWorldControls(Grid targetGrid)
        {
            var spList = new List<StackPanel>
            {
                _controlManager.CreateDataPair("pop", "Populus", "Population.PopulationSize", this),
                _controlManager.CreateDataPair("age", "World Age", "Age", this)
            };
            foreach (var locus in _registeredGenes.Loci)
            {
                foreach (var allele in locus.AlleleManager.Alleles)
                {
                    var func = new Func<IAllele, int>(AllelePopulation);
                    _controlManager.CreateDataPairLinq(allele.Representation, allele.Representation + " Populus",
                        func);
                }
            }
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

        private int AllelePopulation(IAllele allele)
        {
            var gene = this.RegisteredGenes.Loci.Single(l => l.AlleleManager.Alleles.Contains(allele));
            // return all people
            // who whave the allele

            //represented in this gene
            return 2;
        }
    }
}