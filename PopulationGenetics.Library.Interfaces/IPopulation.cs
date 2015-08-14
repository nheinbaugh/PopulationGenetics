using System.Collections.Generic;

namespace PopulationGenetics.Library.Interfaces
{
    public interface IPopulation
    {
        List<IPerson> Populus { get; }
        int PopulationSize { get; }
        void CreatePopulation(int numberToCreate, ILocusBank loci);
        void DestroyPopulation();
    }
}
