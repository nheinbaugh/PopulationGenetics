using System.Collections.Concurrent;
using System.Collections.Generic;

namespace PopulationGenetics.Library.Interfaces
{
    public interface IPopulation
    {
        HashSet<IPerson> Populus { get; }
        int PopulationSize { get; }
        int Males { get; }
        int Females { get; }
        void CreatePopulation(int numberToCreate, IGeneBank loci);
        void DestroyPopulation();
        void UpdatePopulus();
        void AddGeneration(ConcurrentBag<IPerson> children);
    }
}
