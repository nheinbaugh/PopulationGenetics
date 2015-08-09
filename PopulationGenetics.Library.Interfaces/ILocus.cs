using System.Collections.Generic;
using System.Windows.Controls;

namespace PopulationGenetics.Library.Interfaces
{
    public interface ILocus
    {
        IAlleleManager AlleleManager { get; }
        string LocusName { get; }
        void AddAllele(IEnumerable<IAllele> alleles);
        void AddAllele(IAllele allele);
        void AddAllele();
        List<StackPanel> CreateAlleleControls();
    }
}

