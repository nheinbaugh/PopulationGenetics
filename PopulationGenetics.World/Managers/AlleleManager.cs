using System;
using System.Collections.Generic;

namespace PopulationGenetics.Library
{
    public interface IAlleleManager
    {
        List<IAllele> Alleles { get; }
    } 

    public class AlleleManager : IAlleleManager
    {
        private List<IAllele> _alleles;

        public List<IAllele> Alleles { get { return _alleles; } }

        public AlleleManager()
        {

        }
    }
}
