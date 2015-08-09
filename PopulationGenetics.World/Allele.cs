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

        public StackPanel CreateControl(IControlManager controlManager)
        {
            var sp = new StackPanel();
            controlManager.CreateDataPair(_representation, _representation + " Population", "bob", this);
            return sp;
        }
    }
}