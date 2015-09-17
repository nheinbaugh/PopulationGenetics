using System;
using System.Collections.Generic;
using System.Windows.Documents;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library
{
    public class Gene : IGene
    {

        private int _fitnessGain;
        private Guid _firstAlleleId;
        private Guid _secondAlleleId;
        private string _representation;
        private List<Guid> _alleles;
        private Guid _locusId;

        public int FitnessGain => _fitnessGain;
        public string Representation => _representation;
        public List<Guid> Alleles { get { return _alleles; } }
        public Guid LocusId => _locusId;

        public Gene()
        {
            
        }

        public Gene(IAllele firstAllele, IAllele secondAllele, Guid locusId)
        {
            _locusId = locusId;
            this._firstAlleleId = firstAllele.Id;
            this._secondAlleleId = secondAllele.Id;
            _alleles = new List<Guid> {firstAllele.Id, secondAllele.Id};
            BuildRepresentation(firstAllele, secondAllele);   
        }

        /// <summary>
        /// Build the representation of a gene for easy identification
        /// </summary>
        /// <param name="firstAllele"></param>
        /// <param name="secondAllele"></param>
        public void BuildRepresentation(IAllele firstAllele, IAllele secondAllele)
        {
            if (firstAllele.Representation == secondAllele.Representation)
            {
                _representation = firstAllele.Representation;
                return;
            }
            if(firstAllele.IsDominant && secondAllele.IsDominant)
            {
                _representation = GeneRepresentationBuilder.CreateName(firstAllele.Representation, secondAllele.Representation);
                return;
            }
            if (firstAllele.IsDominant)
            {
                _representation = firstAllele.Representation;
                return;
            }
            if (secondAllele.IsDominant)
            {
                _representation = secondAllele.Representation;
                return;
            }
            _representation = GeneRepresentationBuilder.CreateName(firstAllele.Representation,
                secondAllele.Representation);
        }
    }
}