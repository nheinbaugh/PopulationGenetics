using System;
using System.Collections.Generic;
using PopulationGenetics.Library.Interfaces;
using PopulationGenetics.Library.Managers;

namespace PopulationGenetics.Library
{

    public class Locus : ILocus
    {
        private string _locusName;
        private ILocusManager _locusManager;

        public ILocusManager LocusManager => _locusManager;
        public string LocusName => _locusName;

        public Locus(string name)
        {
            _locusName = name;
            _locusManager = new LocusManager();
        }

        public Locus() : this("Random Gene")
        {
        }


        public void AddAllele(IEnumerable<IAllele> alleles)
        {
            _locusManager.Alleles.AddRange(alleles);
        }

        public void AddAllele(IAllele allele)
        {
            _locusManager.Alleles.Add(allele);
        }

        public void AddAllele()
        {
            throw new NotImplementedException();
        }
    }
}
