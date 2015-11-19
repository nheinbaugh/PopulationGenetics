using System;
using System.Collections.Generic;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library
{

    public class Locus : ILocus
    {
        private string _locusName;
        private IAlleleManager _alleleManager;
        private Guid _locusId;

        public IAlleleManager AlleleManager => _alleleManager;
        public string LocusName => _locusName;
        public bool isVisibleLocus { get; set; }
        public Guid LocusId => _locusId;

        public Locus(string name, IAlleleManager alleleManager)
        {
            _locusId = Guid.NewGuid();
            _locusName = name;
            _alleleManager = alleleManager;
            isVisibleLocus = false;
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

        public override string ToString()
        {
            return _locusName;
        }
    }
}
