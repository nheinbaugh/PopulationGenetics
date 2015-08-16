using PopulationGenetics.Library.Factories;
using PopulationGenetics.Library.SeedMaterial;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
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

        public event PropertyChangedEventHandler PropertyChanged;

        public IPersonFactory PersonFactory => _personFactory;
        public IPopulation Population => _population;
        public ILocusBank RegisteredGenes => _registeredGenes;
        public int Age => _age;


        public World(IPopulation pop, IPersonFactory pf, IControlManager controlManager)
        {
            if (pop?.Populus?.Count > 0) pop.Populus.Clear();
            _population = pop;
            _registeredGenes = new LocusBank(controlManager, this);
            _personFactory = pf;
            _controlManager = controlManager;
            WorldSeeds.BaseGenes(_registeredGenes, controlManager, this);
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
            _population.CreatePopulation(seedSize, _registeredGenes);
        }

        public void ProcessTurn()
        {
            _age++;
            var culledPopulation = new List<IPerson>();
            var children = new List<IPerson>();
            for (int i = 0; i < _population.Populus.Count; i++)
            {
                var person = _population.Populus[i];
                person.AgePerson();
                bool survives = CheckSurvival(person.Age);
                if (!survives) culledPopulation.Add(person);
                IPerson partner = ProcreateCheck(person, 100);
                if(partner != null)
                    children.Add(_personFactory.CreateChild(person, partner));
            }
            _population.Populus.RemoveAll(x => !culledPopulation.Exists(y => y.PersonId == x.PersonId));
            _population.Populus.AddRange(children);
            NotifyPropertyChanged("Age");
            foreach (var locus in _registeredGenes.Loci)
            {
                locus.AlleleManager.UpdateControls();
            }
        }

        private IPerson ProcreateCheck(IPerson initializer, int procreateChance)
        {
            var rand = new Random();
            if (TrulyRandomGenerator.BooleanGenerator(1000, procreateChance))
            {
                var seed = 0;
                if (initializer.IsFemale)
                {
                    if (_population.Males == 0) return null;
                    seed = rand.Next(_population.Males);
                    return _population.Populus.Where(a => !a.IsFemale).ToList()[seed];
                }
                if (_population.Females == 0) return null;
                seed = rand.Next(_population.Females);
                return  _population.Populus.Where(a => a.IsFemale).ToList()[seed];
            }
            return null;
        }



        private bool CheckSurvival(int age)
        {
            return TrulyRandomGenerator.BooleanGenerator(1000, 500);
        }

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
            var spList = new List<StackPanel>();
            spList.Add(_controlManager.CreateDataPair("pop", "Populus", "Population.PopulationSize", this));
            spList.Add(_controlManager.CreateDataPair("male", "Males", "Population.Males", this));
            spList.Add(_controlManager.CreateDataPair("female", "Females", "Population.Females", this));
            spList.Add(_controlManager.CreateDataPair("age", "World Age", "Age", this));
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
    }
}