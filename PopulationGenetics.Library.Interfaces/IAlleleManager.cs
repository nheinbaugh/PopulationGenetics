using System.Collections.Generic;

namespace PopulationGenetics.Library.Interfaces
{
    public interface IAlleleManager
    {
        List<IAllele> Alleles { get; }
    }
}
