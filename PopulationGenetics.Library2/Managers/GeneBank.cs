using System;
using System.Collections.Generic;
using System.Windows;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library.Managers
{//TODO: Strip the reference to the control manager from this, we don't use it!
    /// <summary>
    /// The GeneBank class contains the representation of every locus that has been introduced into the population. 
    /// Each locus will have all alleles that are registered to it. 
    /// </summary>
    public class GeneBank: IGeneBank
    {
        private List<ILocus> _loci;
        private Dictionary<Guid, IAllele> _alleleMap;
        private IWorld _world;

        public List<ILocus> Loci => _loci;

        public GeneBank(IWorld world)
        {
            _world = world;
            _loci = new List<ILocus>();
            _alleleMap = new Dictionary<Guid, IAllele>();
        }
        /// <summary>
        /// Use to add a predefined gene to the gene pool
        /// </summary>
        /// <param name="newLocus">The locus to be added to the pool of known gene positions</param>
        public void AddToBank(ILocus newLocus)
        {
            _loci.Add(newLocus);
            foreach (var allele in newLocus.AlleleManager.Alleles)
            {
                _alleleMap.Add(allele.Id, allele);
            }
        }

        /// <summary>
        /// Generate a new gene to 'randomly' add to the gene pool
        /// </summary>
        public void AddToBank()
        {
            throw new NotImplementedException();
        }

        private void GetLocusByName()
        {
            //var bo = _loci.AsQueryable().Select
        }

        public IAllele GetAlleleById(Guid alleleId)
        {
            IAllele allele;
            _alleleMap.TryGetValue(alleleId, out allele);
            return allele;
        }
    }
}
