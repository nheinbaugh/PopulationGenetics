using System;

namespace PopulationGenetics.Library
{
    public interface IAllele
    {
        double DefaultFrequency { get; }
        string Representation { get; }
        bool IsDominant { get; }
    }

    public class Allele : IAllele
    {
        private bool _isDominant;
        private string _representation;
        private double _defaultFrequency;

        public bool IsDominant { get { return _isDominant; } }
        public string Representation { get { return _representation; } }
        public double DefaultFrequency { get { return _defaultFrequency; } }

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