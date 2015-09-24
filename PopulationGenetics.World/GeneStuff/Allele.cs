using System;
using System.Windows.Controls;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library
{
    public class Allele : IAllele
    {
        private bool _isDominant;
        private string _representation;
        private double _defaultFrequency;
        private Guid _id;

        public bool IsDominant => _isDominant;
        public string Representation => _representation;
        public double DefaultFrequency => _defaultFrequency;
        public Guid Id => _id;

        public Allele(string representation, bool isDominant, double defaultFreq = 0)
        {
            _defaultFrequency = defaultFreq;
            _representation = representation;
            _isDominant = isDominant;
            _id = Guid.NewGuid();
        }

        public Allele() : this("test", true)
        {
        }

        public Allele(double defaultFreq) : this("test", true, defaultFreq)
        {
        }
    }
}