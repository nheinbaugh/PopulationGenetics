using PopulationGenetics.Library.SeedMaterial;
using System;
using System.Collections.Generic;
namespace PopulationGenetics.Library
{

    public interface IWorld
    {
        IGeneBank RegisteredGenes { get; }
        List<IPerson> Population { get; }
        int PopulationSize { get; }
        void ProcessTurn();
    }
    public class World : IWorld
    {
        private List<IPerson> _population;
        private IGeneBank _registeredGenes;

        public List<IPerson> Population { get { return _population; } }
        public IGeneBank RegisteredGenes { get { return _registeredGenes; } }
        public int PopulationSize { get { return _population.Count; } }


        public World(List<IPerson> pop, IGeneBank genes)
        {
            _population = pop;
            _registeredGenes = genes;
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
                _population.Add(new Person());
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

