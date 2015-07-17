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

