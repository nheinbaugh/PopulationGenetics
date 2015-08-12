using System.Collections.Generic;

namespace PopulationGenetics.Library.Interfaces
{
    public interface IGene
    {
        int FitnessGain { get; }
        string Representation { get; }
        List<IAllele> Alleles { get; }
    }
}
