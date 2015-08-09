using System.Collections.Generic;

namespace PopulationGenetics.Library.Interfaces
{
    public interface ILocusBank
    {
        List<ILocus> Loci { get; }
        void AddToBank(ILocus newLocus);
        void AddToBank();
        void CreateGeneControls();
    }
}
