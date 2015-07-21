using PopulationGenetics.Library.Factories;
using PopulationGenetics.Library.SeedMaterial;
using System;
using System.Collections.Generic;
namespace PopulationGenetics.Library
{

    public interface IWorld
    {
        ILocusBank RegisteredGenes { get; }
        List<IPerson> Population { get; }
        int PopulationSize { get; }
        void ProcessTurn();
    }
    public class World : IWorld
    {
        private List<IPerson> _population;
        private ILocusBank _registeredGenes;
        private IPersonFactory _personFactory;

        public IPersonFactory PersonFactory { get { return _personFactory; } }
        public List<IPerson> Population { get { return _population; } }
        public ILocusBank RegisteredGenes { get { return _registeredGenes; } }
        public int PopulationSize { get { return _population.Count; } }



        public World(List<IPerson> pop, ILocusBank genes, IPersonFactory personFactory)
        {
            _population = pop;
            _registeredGenes = genes;
            _personFactory = personFactory;
            // TODO Question: should I pass in the gene bank and add the stuff behind the scenes or should I make it obvious? 
            // I can always just rename the method to AddBaseGenes...
            WorldSeeds.BaseGenes(_registeredGenes);
            SeedWorld(9);
        }

        public World(int seedSize)
        {
            SeedWorld(seedSize);
        }

        private void SeedWorld(int seedSize)
        {
            for (int i = 0; i < seedSize; i++)
            {
                _population.Add(_personFactory.CreateNewPerson());
            }
        }


        public void ProcessTurn()
        {
            foreach (var person in _population)
            {

            }
        }
    }
}

