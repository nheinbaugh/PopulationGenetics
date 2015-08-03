using PopulationGenetics.Library.Factories;
using PopulationGenetics.Library.SeedMaterial;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace PopulationGenetics.Library
{

    public interface IWorld
    {
        int Age { get; }
        ILocusBank RegisteredGenes { get; }
        List<IPerson> Population { get; }
        int PopulationSize { get; }
        void ProcessTurn();
        /// <summary>
        /// Clear the current population from the world. Will reset the age of the world to zero and remove all population.
        /// </summary>
        /// <param name="removeGenes">If true then will remove all loci from the locus bank</param>
        void CleanWorld(bool removeGenes);
        void SeedWorld(int seedSize);
        StackPanel CreateControls();
    }
    public class World : IWorld
    {
        private List<IPerson> _population;
        private ILocusBank _registeredGenes;
        private IPersonFactory _personFactory;
        private int _age;
        private IControlManager _controlManager;

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

        public void SeedWorld(int seedSize)
        {
            for (int i = 0; i < seedSize; i++)
            {
                _population.Add(_personFactory.CreateNewPerson());
            }
        }

        public void ProcessTurn()
        {
            _age++;
            foreach (var person in _population)
            {

            }
        }

        public void CleanWorld(bool clearGenes)
        {
            _population.Clear();
            if (clearGenes)
            {
                _registeredGenes.Genes.Clear();
            }
        }

        public StackPanel CreateControls()
        {
            return _controlManager.CreateDataPair("pop", "Population");
        }
    }
}

