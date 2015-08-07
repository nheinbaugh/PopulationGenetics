using System;
using System.Collections.Generic;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library
{


    public class LocusManager : ILocusManager
    {
        private List<IAllele> _alleles;

        public List<IAllele> Alleles => _alleles;

        public LocusManager()
        {
            _alleles = new List<IAllele>();
        }
    }
}
