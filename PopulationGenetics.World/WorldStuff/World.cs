using PopulationGenetics.Library.Factories;
using PopulationGenetics.Library.SeedMaterial;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using PopulationGenetics.Library.Interfaces;
using PopulationGenetics.Library.Managers;
using PopulationGenetics.WpfBindings;

namespace PopulationGenetics.Library
{
    public class World : IWorld, INotifyPropertyChanged
    {
        private readonly IPopulation _population;
        private readonly ILocusBank _registeredGenes;
        private readonly IPersonFactory _personFactory;
        private int _age;
        private readonly IControlManager _controlManager;
        private IMortalityCurve _mortalityCurve;
        private IRandomGenerator _random;

        public event PropertyChangedEventHandler PropertyChanged;

        public IPersonFactory PersonFactory => _personFactory;
        public IPopulation Population => _population;
        public ILocusBank RegisteredGenes => _registeredGenes;
        public int Age => _age;
        public IMortalityCurve MortalityCurve => _mortalityCurve;


        public World(IPopulation pop, IPersonFactory pf, IControlManager controlManager, IMortalityCurve mortalityCurve, IRandomGenerator random)
        {
            if (pop?.Populus?.Count > 0) pop.Populus.Clear();
            _random = random;
            _population = pop;
            _registeredGenes = new LocusBank(controlManager, this);
            _personFactory = pf;
            _mortalityCurve = mortalityCurve;
            _controlManager = controlManager;
            WorldSeeds.BaseGenes(_registeredGenes, controlManager, this);
            SeedWorld(1000);
            _population?.UpdatePopulus();
        }


        public World(int seedSize)
        {
            SeedWorld(seedSize);
        }

        /// <summary>
        /// Seed a world with a populatin
        /// </summary>
        /// <param name="seedSize">Number of individuals to create for the new world</param>
        public void SeedWorld(int seedSize)
        {
            _population.CreatePopulation(seedSize, _registeredGenes);
        }

        public void ProcessTurn()
        {
            _age++;
            var culledPopulation = new ConcurrentBag<IPerson>();
            var children = new ConcurrentBag<IPerson>();
            Parallel.For(0, _population.PopulationSize, i =>
            {
                var person = _population.Populus[i];
                var survives = person.AgePerson(_mortalityCurve);
                if (!survives) culledPopulation.Add(person);
            });
            foreach (var culled in culledPopulation)
            {
                _population.Populus.Remove(culled);
            }
            var maleList = _population.Populus.Where(a => !a.IsFemale && a.EligibleForBreeding).ToList();
            var femaleList = _population.Populus.Where(a => a.IsFemale && a.EligibleForBreeding).ToList();
            Parallel.For(0, _population.PopulationSize, i =>
            {
                var person = _population.Populus[i];
                var child = person.IsFemale ? _personFactory.MakeBaby(person, maleList, _registeredGenes) : 
                    _personFactory.MakeBaby(person, femaleList, _registeredGenes);
                if (child != null) children.Add(child);
            });

            _population.Populus.AddRange(children);
            _population.UpdatePopulus();
            NotifyPropertyChanged("Age");
            foreach (var locus in _registeredGenes.Loci)
            {
                if(locus.isVisibleLocus)
                    locus.AlleleManager.UpdateControls();
            }
        }

        /// <summary>
        /// Clear the entire population and possibly clear the registered genes
        /// </summary>
        /// <param name="clearGenes">Whether to clear the gene bank or leave it populated with existing loci</param>
        public void CleanWorld(bool clearGenes)
        {
            _population.DestroyPopulation();
            _age = 0;
            if (clearGenes)
            {
                _registeredGenes.Loci.Clear();
            }
        }

        public List<StackPanel> CreateWorldControls(Grid targetGrid)
        {
            var rowCount = targetGrid.RowDefinitions.Count;
            var spList = new List<StackPanel>
            {
                _controlManager.CreateLocusSelector(_registeredGenes, this),
                _controlManager.CreateDataPair("pop", "Populus", "Population.PopulationSize", this),
                _controlManager.CreateDataPair("male", "Eligible Males", "Population.Males", this),
                _controlManager.CreateDataPair("female", "Eligible Females", "Population.Females", this),
                _controlManager.CreateDataPair("age", "World Age", "Age", this)
            };
            for (int i = 0; i < spList.Count; i++)
            {
                var current = spList[i];
                if (i >= rowCount)
                {
                    targetGrid.RowDefinitions.Add(new RowDefinition());
                }
                Grid.SetColumn(current, 1);
                Grid.SetRow(current, i);
                targetGrid.Children.Add(current);
            }

            targetGrid.ColumnDefinitions.Add(new ColumnDefinition {Width = GridLength.Auto});
            return spList;
        }

        protected void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}