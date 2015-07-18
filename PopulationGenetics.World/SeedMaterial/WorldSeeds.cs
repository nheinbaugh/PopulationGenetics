using System;
using System.Collections.Generic;

namespace PopulationGenetics.Library.SeedMaterial
{
    public static class WorldSeeds
    {
        public static void BaseGenes(IGeneBank geneBank)
        {
            var genes = new List<IGene>();
            // TODO generate the genes!
            foreach (var gene in genes)
            {
                geneBank.AddToBank(gene);
            }
        }
    }
}
