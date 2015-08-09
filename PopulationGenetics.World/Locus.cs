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

        public IAlleleManager AlleleManager => _alleleManager;
        public string LocusName => _locusName;

        public Locus(string name)
        {
            _locusName = name;
            _alleleManager = new AlleleManager();
        }

        public Locus() : this("Random Gene")
        {
        }


        public void AddAllele(IEnumerable<IAllele> alleles)
        {
            _alleleManager.Alleles.AddRange(alleles);
        }

        public void AddAllele(IAllele allele)
        {
            _alleleManager.Alleles.Add(allele);
        }

        public void AddAllele()
        {
            throw new NotImplementedException();
        }

        public List<StackPanel> CreateAlleleControls()
        {
            var spList = new List<StackPanel>();
            foreach (var allele in _alleleManager.Alleles)
            {
                                                                                
            }
            return spList;
        }
    }
}
