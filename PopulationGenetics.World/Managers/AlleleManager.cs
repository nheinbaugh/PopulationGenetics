using System;
using System.Collections.Generic;

namespace PopulationGenetics.Library
{
    public interface ILocusManager
    {
        List<IAllele> Alleles { get; }
    } 

    public class LocusManager : ILocusManager
    {
        private List<IAllele> _alleles;

        public List<IAllele> Alleles { get { return _alleles; } }

        public LocusManager()
        {
            _alleles = new List<IAllele>();
        }
    }
}
