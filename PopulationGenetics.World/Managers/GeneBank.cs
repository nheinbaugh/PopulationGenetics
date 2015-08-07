﻿using System;
using System.Collections.Generic;
using System.Linq;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library
{
    /// <summary>
    /// The GeneBank class contains the representation of every locus that has been introduced into the population. 
    /// Each locus will have all alleles that are registered to it. 
    /// </summary>
    public class LocusBank: ILocusBank
    {
        private List<ILocus> _genes;

        public List<ILocus> Genes => _genes;

        public LocusBank()
        {
            _genes = new List<ILocus>();
        }

        /// <summary>
        /// Use to add a predefined gene to the gene pool
        /// </summary>
        /// <param name="newLocus">The locus to be added to the pool of known gene positions</param>
        public void AddToBank(ILocus newLocus)
        {
            _genes.Add(newLocus);
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
            //var bo = _genes.AsQueryable().Select
        }
    }
}
