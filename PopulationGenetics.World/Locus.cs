using System;
using System.Collections.Generic;

namespace PopulationGenetics.Library
{
    public interface ILocus
    {
        ILocusManager LocusManager { get; }
        string LocusName { get; }
        void AddAllele(IEnumerable<IAllele> alleles);
        void AddAllele(IAllele allele);
        void AddAllele();
    }

    public class Locus : ILocus
    {
        private string _locusName;
        private ILocusManager _locusManager;

        public ILocusManager LocusManager { get { return _locusManager; } }
        public string LocusName { get { return _locusName; } }

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
