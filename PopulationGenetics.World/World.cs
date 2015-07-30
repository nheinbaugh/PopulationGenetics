using PopulationGenetics.Library.Factories;
using PopulationGenetics.Library.SeedMaterial;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PopulationGenetics.Library
{

    public interface IWorld
    {
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
    }
    public class World : IWorld
    {
        private List<IPerson> _population;
        private ILocusBank _registeredGenes;
        private IPersonFactory _personFactory;

        public IPersonFactory PersonFactory { get { return _personFactory; } }
        public List<IPerson> Population { get { return _population; } }
        public int APop { get {
                return _population.AsQueryable()
                    .Where(a => a.Genes[0].Representation == "A").ToList().Count;
            }
            set { APop = value; }
        }
        public int BPop
        {
            get
            {
                return _population.AsQueryable()
                    .Where(a => a.Genes[0].Representation == "B").ToList().Count;
            }
            set { BPop = value; }

        }
        public int OPop
        {
            get
            {
                return _population.AsQueryable()
                    .Where(a => a.Genes[0].Representation == "O").ToList().Count;
            }
            set { OPop = value; }

        }
        public int ABPop
        {
            get
            {
                return _population.AsQueryable()
                    .Where(a => a.Genes[0].Representation.Length == 2).ToList().Count;
            }
            set { ABPop = value; }

        }
        public ILocusBank RegisteredGenes { get { return _registeredGenes; } }
        public int PopulationSize { get { return _population.Count; } }



        public World(List<IPerson> pop, ILocusBank genes, IPersonFactory personFactory)
        {
            if (pop.Count > 0) pop.Clear();
            _population = pop;
            _registeredGenes = genes;
            _personFactory = personFactory;
            // TODO Question: should I pass in the gene bank and add the stuff behind the scenes or should I make it obvious? 
            // I can always just rename the method to AddBaseGenes...
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
    }
}

