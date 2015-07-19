using System;
using System.Collections.Generic;

namespace PopulationGenetics.Library
{
    public interface IGeneBank
    {
        List<ILocus> Genes { get; }
        void AddToBank(ILocus newGene);
        void AddToBank();
    }

    public class GeneBank : IGeneBank
    {
        private List<ILocus> _genes;

        public List<ILocus> Genes
        {
            get { return _genes; }
        }

        public GeneBank()
        {

        }
        /// <summary>
        /// Use to add a predefined gene to the gene pool
        /// </summary>
        /// <param name="newGene"></param>
        public void AddToBank(ILocus newGene)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generate a new gene to 'randomly' add to the gene pool
        /// </summary>
        public void AddToBank()
        {
            throw new NotImplementedException();
        }
    }
}
