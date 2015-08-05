using System.Collections.Generic;

namespace PopulationGenetics.Library.Interfaces
{
    public interface ILocusBank
    {
        List<ILocus> Genes { get; }
        void AddToBank(ILocus newLocus);
        void AddToBank();
    }
}
