using System;
using System.Collections.Generic;

namespace PopulationGenetics.Library.Interfaces
{
    public interface IGene
    {
        Guid LocusId { get; }
        int FitnessGain { get; }
        string Representation { get; }
        List<IAllele> Alleles { get; }
    }
}
