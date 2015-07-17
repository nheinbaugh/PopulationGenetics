using System;
using System.Collections.Generic;
namespace PopulationGenetics.Library
{

    public interface IWorld
    {
        List<IPerson> Population { get; set; }
        int PopulationSize { get; }
        void ProcessTurn();
    }
    public class World : IWorld
    {
        private List<IPerson> population;
        public int PopulationSize { get { return population.Count; } }
        public List<IPerson> Population
        {
            get { return population; }
            set { population = value; }
        }

        public World(List<IPerson> pop)
        {
            population = pop;
        }

        public World(int seedSize)
        {
            SeedWorld(seedSize);
        }

        private void SeedWorld(int seedSize)
        {

        }

        public void ProcessTurn()
        {

        }
    }
}

