using System;
using System.Collections.Generic;

namespace PopulationGenetics.Library.Interfaces
{
    public interface IGeneBank
    {
        List<ILocus> Loci { get; }
        void AddToBank(ILocus newLocus);
        void AddToBank();
        IAllele GetAlleleById(Guid alleleId);
    }
}
