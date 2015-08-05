using System.Collections.Generic;

namespace PopulationGenetics.Library.Interfaces
{
    public interface ILocusManager
    {
        List<IAllele> Alleles { get; }
    }
}
