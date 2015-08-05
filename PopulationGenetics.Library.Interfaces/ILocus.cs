using System.Collections.Generic;

namespace PopulationGenetics.Library.Interfaces
{
    public interface ILocus
    {
        ILocusManager LocusManager { get; }
        string LocusName { get; }
        void AddAllele(IEnumerable<IAllele> alleles);
        void AddAllele(IAllele allele);
        void AddAllele();
    }
}

