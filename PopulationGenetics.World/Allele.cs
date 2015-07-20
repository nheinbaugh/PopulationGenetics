using System;

namespace PopulationGenetics.Library
{
    public interface IAllele
    {
        string Representation { get; }
        bool IsDominant { get; }
    }

    public class Allele : IAllele
    {
        private bool _isDominant;
        private string _representation;

        public bool IsDominant { get { return _isDominant; } }
        public string Representation { get { return _representation; } }

        public Allele(string representation, bool isDominant)
        {
            _representation = representation;
            _isDominant = isDominant;
        }

        public Allele()
        {

        }
    }
}