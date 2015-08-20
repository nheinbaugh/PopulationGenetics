using System;
using System.Collections.Generic;
using System.Windows.Controls;
using PopulationGenetics.Library.Interfaces;
using PopulationGenetics.Library.Managers;

namespace PopulationGenetics.Library
{

    public class Locus : ILocus
    {
        private string _locusName;
        private IAlleleManager _alleleManager;
        private Guid _locusId;

        public IAlleleManager AlleleManager => _alleleManager;
        public string LocusName => _locusName;
        public Guid LocusId => _locusId;

        public Locus(string name, IAlleleManager alleleManager)
        {
            _locusId = Guid.NewGuid();
            _locusName = name;
            _alleleManager = alleleManager;
        }

        public void AddAllele(IEnumerable<IAllele> alleles)
        {
            _alleleManager.CreateMultipleAlleles(alleles);
        }

        public void AddAllele(IAllele allele)
        {
            _alleleManager.CreateAllele(allele);
        }

        public void AddAllele()
        {
            throw new NotImplementedException();
        }
    }
}
