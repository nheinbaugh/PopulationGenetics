using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PopulationGenetics.Library.Interfaces
{
    public interface IGeneBank
    {
        List<ILocus> Loci { get; }
        void AddToBank(ILocus newLocus);
        void AddToBank();
        void UpdateVisibleControls(Grid geneGrid);
        IAllele GetAlleleById(Guid alleleId);
    }
}
