using System;
using System.Collections.Generic;

namespace PopulationGenetics.Library
{
    public interface ILocus
    {
        List<IAllele> Alleles { get; }
        string LocusName { get; }
        void AddAllele(IAllele allele);
        void AddAllele();
    }

    public class Locus : ILocus
    {
        private string _locusName;
        private List<IAllele> _alleles;
        public List<IAllele> Alleles
        {
            get { return _alleles; }
        }

        public string LocusName { get { return _locusName; } }

        public void AddAllele(IAllele allele)
        {
            throw new NotImplementedException();
        }

        public void AddAllele()
        {
            throw new NotImplementedException();
        }
    }
}
