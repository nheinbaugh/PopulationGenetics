using System;

namespace PopulationGenetics.World
{
    public interface IGene
    {
        IAllele[] Alleles { get; }
    }

    public class Gene : IGene
    {
        IAllele[] _alleles;
        public IAllele[] Alleles
        {
            get { return _alleles; }
        }

        public Gene(IAllele firstAllele, IAllele secondAllele)
        {

        }

        public Gene(IGene parentOne, IGene parentTwo)
        {

        }
    }
}