using System;
using System.Collections.Generic;

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
            foreach (var locus in _locusBank.Genes)
            {
                var gene = GenerateNewGene(locus);
                genes.Add(gene);
            }
            var person = new Person(genes, true);
            return person;
        }

        private Gene GenerateNewGene(ILocus locus)
        {
            var gene = new Gene(GenerateAllele(locus.LocusManager), GenerateAllele(locus.LocusManager));

            return gene;
        }

        private IAllele GenerateAllele(ILocusManager manager)
        {
            var rand = new Random();
            var num = rand.Next(1001);
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
