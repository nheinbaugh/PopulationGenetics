using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PopulationGenetics.Library.Interfaces
{
    public interface ILocus
    {
        Guid LocusId { get; }
        IAlleleManager AlleleManager { get; }
        string LocusName { get; }
        bool isVisibleLocus { get; set; }
        void AddAllele(IEnumerable<IAllele> alleles);
        void AddAllele(IAllele allele);
        void AddAllele();
    }
}

