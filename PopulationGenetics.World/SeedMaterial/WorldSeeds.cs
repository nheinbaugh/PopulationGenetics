using System;
using System.Collections.Generic;

namespace PopulationGenetics.Library.SeedMaterial
{
    public static class WorldSeeds
    {
        public static void BaseGenes(ILocusBank geneBank)
        {
            var geneLocations = new List<ILocus>();
            geneLocations.Add(BloodTypeAlleles());
            
            // TODO generate the genes!
            foreach (var gene in geneLocations)
            {
                geneBank.AddToBank(gene);
            }
        }

        private static Locus BloodTypeAlleles()
        {
            var bloodType = new Locus("Blood Type");
            var a = new Allele("A", true);
            var b = new Allele("B", true);
            var o = new Allele("O", false);
            bloodType.AddAllele(a);
            bloodType.AddAllele(b);
            bloodType.AddAllele(o);
            return bloodType;
        }
    }
}
