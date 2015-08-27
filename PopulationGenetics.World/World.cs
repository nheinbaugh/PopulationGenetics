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

        public event PropertyChangedEventHandler PropertyChanged;

        public IPersonFactory PersonFactory => _personFactory;
        public IPopulation Population => _population;
        public ILocusBank RegisteredGenes => _registeredGenes;
        public int Age => _age;
        public IMortalityCurve MortalityCurve => _mortalityCurve;


        public World(IPopulation pop, IPersonFactory pf, IControlManager controlManager, IMortalityCurve mortalityCurve)
        {
            if (pop?.Populus?.Count > 0) pop.Populus.Clear();
            _population = pop;
            _registeredGenes = new LocusBank(controlManager, this);
            _personFactory = pf;
            _mortalityCurve = mortalityCurve;
            _controlManager = controlManager;
            WorldSeeds.BaseGenes(_registeredGenes, controlManager, this);
            SeedWorld(1000);
            _population.UpdatePopulus();
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
                person.AgePerson();
                var survives = CheckSurvival(person.Age);
                if (!survives) culledPopulation.Add(person);
                var partner = ProcreateCheck(person, 100);
                if (partner != null)
                {
                    children.Add(_personFactory.CreateChild(person, partner, _registeredGenes));
                    if (person.IsFemale) person.HaveBaby();
                }
            });
            foreach (var culled in culledPopulation)
            {
                _population.Populus.Remove(culled);
            }
            _population.Populus.AddRange(children);
            _population.UpdatePopulus();
            NotifyPropertyChanged("Age");
            foreach (var locus in _registeredGenes.Loci)
            {
                locus.AlleleManager.UpdateControls();
            }
        }

        /// <summary>
        /// Check if the selected person object will create an offspring
        /// </summary>
        /// <param name="initializer">The person being checked</param>
        /// <param name="procreateChance">The odds that a person will procreate (based on age?)</param>
        /// <returns></returns>
        private IPerson ProcreateCheck(IPerson initializer, int procreateChance)
        {
            var rand = new Random();
            if (TrulyRandomGenerator.BooleanGenerator(1000, procreateChance))
            {
                var seed = 0;
                if (initializer.IsFemale && !initializer.IsPregnant)
                {
                    if (_population.Males == 0) return null;
                    var rob = _population.Populus.Where(a => !a.IsFemale && a.EligibleForBreeding).ToList();
                    seed = rand.Next(rob.Count);
                    return rob[seed];
                }
                if (_population.Females == 0) return null;
                var bob = _population.Populus.Where(a => a.IsFemale && a.EligibleForBreeding).ToList();
                seed = rand.Next(bob.Count);
                return bob[seed].IsPregnant ? null : bob[seed];
            }
            return null;
        }


        /// <summary>
        /// Check if a person object is going to survive the current generation
        /// </summary>
        /// <param name="age">Age from the person object</param>
        /// <returns></returns>
        private bool CheckSurvival(int age)
        {
            var survivalRate = _mortalityCurve.GetMortalityByAge(age);
            return TrulyRandomGenerator.BooleanGenerator(1000, survivalRate);
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
            var spList = new List<StackPanel>
            {
                _controlManager.CreateDataPair("pop", "Populus", "Population.PopulationSize", this),
                _controlManager.CreateDataPair("male", "Eligible Males", "Population.Males", this),
                _controlManager.CreateDataPair("female", "Eligible Females", "Population.Females", this),
                _controlManager.CreateDataPair("age", "World Age", "Age", this)
            };
            for (int i = 0; i < spList.Count; i++)
            {
                var current = spList[i];
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