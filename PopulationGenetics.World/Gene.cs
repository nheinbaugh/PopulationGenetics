using System;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library
{
    public class Gene : IGene
    {

        private int _fitnessGain;
        private IAllele _firstAllele;
        private IAllele _secondAllele;
        private string _representation;

        public int FitnessGain { get { return _fitnessGain; } }
        public string Representation { get { return _representation; } }

        public Gene(IAllele firstAllele, IAllele secondAllele)
        {
            this._firstAllele = firstAllele;
            this._secondAllele = secondAllele;
            BuildRepresentation();   
        }

        public Gene(IAllele parentAllele)
        {

        }

        private void BuildRepresentation()
        {
            if(_firstAllele.IsDominant && _secondAllele.IsDominant)
            {
                _representation = _firstAllele.Representation + _secondAllele.Representation;
                return;
            }
            if (_firstAllele.IsDominant)
            {
                _representation = _firstAllele.Representation;
                return;
            }
            _representation = _secondAllele.Representation;
        }
    }
}