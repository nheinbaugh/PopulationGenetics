using System;
using System.Collections.Generic;
using PopulationGenetics.Library.Interfaces;

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
            var a = new Allele("A", true, .33);
            var b = new Allele("B", true, .33);
            var o = new Allele("O", false, .33);
            bloodType.AddAllele(a);
            bloodType.AddAllele(b);
            bloodType.AddAllele(o);
            return bloodType;
        }
    }
}
