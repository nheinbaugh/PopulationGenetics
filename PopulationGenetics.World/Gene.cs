using System;

namespace PopulationGenetics.Library
{
    public interface IGene
    {
        int FitnessGain { get; }
    }

    public class Gene : IGene
    {

        private int _fitnessGain;

        public int FitnessGain { get { return _fitnessGain; } }


        public Gene(IAllele firstAllele, IAllele secondAllele)
        {
            
        }

        public Gene(IAllele parentAllele)
        {

        }
    }
}