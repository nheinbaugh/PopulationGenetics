using System.Collections.Generic;
using PopulationGenetics.Library.Interfaces;
using PopulationGenetics.Library.Managers;

namespace PopulationGenetics.Library.SeedMaterial
{
    public static class WorldSeeds
    {
        public static void BaseGenes(IGeneBank geneBank, IWorld world)
        {
            CreateMortalityCurve(world);
            var geneLocations = new List<ILocus>();
            geneLocations.Add(BloodTypeAlleles(world));
            geneLocations.Add(SecondAllele(world));
            // TODO generate the genes!
            foreach (var gene in geneLocations)
            {
                geneBank.AddToBank(gene);
            }
        }

        private static Locus SecondAllele(IWorld world)
        {
            var mgr = new AlleleManager(world);
            var locus = new Locus("Eye Color", mgr);
            locus.isVisibleLocus = true;
            var h = new Allele("H", false, .4);
            var bl = new Allele("B", true, .25);
            var red = new Allele("R", false, .1);
            var blue = new Allele("L", false, .25);
            locus.AddAllele(h);
            locus.AddAllele(bl);
            locus.AddAllele(blue);
            locus.AddAllele(red);
            return locus;
        }

        private static Locus BloodTypeAlleles( IWorld world)
        {
            var alleleMgr = new AlleleManager(world);
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
