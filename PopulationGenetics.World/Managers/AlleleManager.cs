using System.Collections.Generic;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library.Managers
{


    public class AlleleManager : IAlleleManager
    {
        private List<IAllele> _alleles;

        public List<IAllele> Alleles => _alleles;

        public AlleleManager()
        {
            _alleles = new List<IAllele>();
        }
    }
}
