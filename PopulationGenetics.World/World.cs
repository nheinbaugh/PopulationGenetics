using System;
using System.Collections.Generic;
namespace PopulationGenetics.Library
{

    public interface IWorld
    {
        IGeneBank RegisteredGenes { get; }
        List<IPerson> Population { get; set; }
        int PopulationSize { get; }
        void ProcessTurn();
    }
    public class World : IWorld
    {
        private List<IPerson> population;
        private IGeneBank _registeredGenes;

        public List<IPerson> Population
        {
            get { return population; }
            set { population = value; }
        }

        public IGeneBank RegisteredGenes { get { return _registeredGenes; } }
        public int PopulationSize { get { return population.Count; } }


        public World(List<IPerson> pop, IGeneBank genes)
        {
            population = pop;
            _registeredGenes = genes;
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
                population.Add(new Person());
            }
        }

        public void ProcessTurn()
        {

        }
    }
}

