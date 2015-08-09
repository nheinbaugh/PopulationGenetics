using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library.Factories
{
    public interface IPersonFactory
    {
        Person CreateNewPerson();
    }
    public class PersonFactory : IPersonFactory
    {
        private ILocusBank _locusBank;

        public PersonFactory(ILocusBank locusBank)
        {
            _locusBank = locusBank;
        }

        public Person CreateNewPerson()
        {
            var genes = new List<IGene>();
            foreach (var locus in _locusBank.Loci)
            {
                var gene = GenerateNewGene(locus);
                genes.Add(gene);
            }
            var person = new Person(genes, true);
            return person;
        }

        private Gene GenerateNewGene(ILocus locus)
        {
            var gene = new Gene(GenerateAllele(locus.AlleleManager), GenerateAllele(locus.AlleleManager));

            return gene;
        }
        private double TrulyRandom(int max)
        {
            var rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[4];

            rng.GetBytes(buffer);
            int result = BitConverter.ToInt32(buffer, 0);

            return new Random(result).Next(0, max + 1);
        }
        private IAllele GenerateAllele(IAlleleManager manager)
        {

            var num = TrulyRandom(1000);
            double current = 0;
            foreach (var allele in manager.Alleles)
            {
                var max = (allele.DefaultFrequency * 1000) + current;
                if (num <= max)
                    return allele;
                current += max;
            }
            return manager.Alleles[0];
        }
    }
}
