using System;
using System.Collections.Generic;
using System.Windows.Documents;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library
{
    public class Gene : IGene
    {

        private int _fitnessGain;
        private IAllele _firstAllele;
        private IAllele _secondAllele;
        private string _representation;
        private List<IAllele> _alleles;

        public int FitnessGain => _fitnessGain;
        public string Representation => _representation;
        public List<IAllele> Alleles { get { return _alleles; } }

        public Gene(IAllele firstAllele, IAllele secondAllele)
        {
            this._firstAllele = firstAllele;
            this._secondAllele = secondAllele;
            _alleles = new List<IAllele> {_firstAllele, _secondAllele};
            BuildRepresentation();   
        }

        public Gene(IAllele parentAllele)
        {

        }

        private void BuildRepresentation()
        {
            if (_firstAllele.Representation == _secondAllele.Representation)
            {
                _representation = _firstAllele.Representation;
                return;
            }
            if(_firstAllele.IsDominant && _secondAllele.IsDominant)
            {
                _representation = GeneRepresentationBuilder.CreateName(_firstAllele, _secondAllele);
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