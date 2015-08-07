using System;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library
{
    public class Allele : IAllele
    {
        private bool _isDominant;
        private string _representation;
        private double _defaultFrequency;

        public bool IsDominant => _isDominant;
        public string Representation => _representation;
        public double DefaultFrequency => _defaultFrequency;

        public Allele(string representation, bool isDominant, double defaultFreq = 0)
        {
            _defaultFrequency = defaultFreq;
            _representation = representation;
            _isDominant = isDominant;
        }

        public Allele()
        {

        }
    }
}