using System.Collections.Generic;

namespace PopulationGenetics.Library.Interfaces
{
    public interface IWorld
    {
        int Age { get; }
        IMortalityCurve MortalityCurve { get; }
        IGeneBank GeneBank { get; }
        IPopulation Population { get; }
        void ProcessTurn();
        /// <summary>
        /// Clear the current population from the world. Will reset the age of the world to zero and remove all population.
        /// </summary>
        /// <param name="removeGenes">If true then will remove all loci from the locus bank</param>
        void CleanWorld(bool removeGenes);
        void SeedWorld(int seedSize);
    }
}
