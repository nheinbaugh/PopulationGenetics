using System;
using System.Collections.Generic;
using PopulationGenetics.Library.Interfaces;
using PopulationGenetics.Library.Managers;

namespace PopulationGenetics.Library.SeedMaterial
{
    public static class WorldSeeds
    {
        public static void BaseGenes(ILocusBank geneBank, IControlManager cm, IWorld world)
        {
            CreateMortalityCurve(world);
            var geneLocations = new List<ILocus>();
            geneLocations.Add(BloodTypeAlleles(cm, world));
            
            // TODO generate the genes!
            foreach (var gene in geneLocations)
            {
                geneBank.AddToBank(gene);
            }
        }

        private static Locus BloodTypeAlleles(IControlManager cm, IWorld world)
        {
            var alleleMgr = new AlleleManager(cm, world);
            var bloodType = new Locus("Blood Type", alleleMgr);
            var a = new Allele("A", true, .33);
            var b = new Allele("B", true, .33);
            var o = new Allele("O", false, .33);
            bloodType.AddAllele(a);
            bloodType.AddAllele(b);
            bloodType.AddAllele(o);
            return bloodType;
        }

        private static void CreateMortalityCurve(IWorld world)
        {
            var curve = new List<int>
            {
                700,
                300,
                300,
                200,
                300,
                400,
                500,
                700,
                800,
                900,
                1000
            };
            world.MortalityCurve.SetMortalityCurve(curve);
        }
    }
}
