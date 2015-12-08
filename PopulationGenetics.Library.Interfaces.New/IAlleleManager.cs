using System.Collections.Generic;

namespace PopulationGenetics.Library.Interfaces
{
    public interface IAlleleManager
    {
        List<IAlleleControl> Controls { get; }
        List<IAllele> Alleles { get; }
        void CreateAllele(IAllele allele);
        void CreateMultipleAlleles(IEnumerable<IAllele> alleles);
        void UpdateControls();
    }
}
