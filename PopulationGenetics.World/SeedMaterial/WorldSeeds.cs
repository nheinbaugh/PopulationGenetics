using System;
using System.Collections.Generic;

namespace PopulationGenetics.Library.SeedMaterial
{
    public static class WorldSeeds
    {
        public static void BaseGenes(IGeneBank geneBank)
        {
            var geneLocations = new List<ILocus>();
            // TODO generate the genes!
            foreach (var gene in geneLocations)
            {
                geneBank.AddToBank(gene);
            }
        }
    }
}
