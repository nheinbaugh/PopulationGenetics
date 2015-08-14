using System;
using System.Collections.Generic;
using System.Windows.Controls;

namespace PopulationGenetics.Library.Interfaces
{
    public interface ILocusBank
    {
        List<ILocus> Loci { get; }
        void AddToBank(ILocus newLocus);
        void AddToBank();
        void CreateGeneControls(IWorld world, Func<IAllele, int> allelePopulation, Grid geneGrid);
    }
}
