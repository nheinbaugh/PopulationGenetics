using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library.Factories
{
    public interface IPersonFactory
    {
        Person CreateNewPerson(ILocusBank locusBank);
        IPerson CreateChild(IPerson person, IPerson partner);
    }
    public class PersonFactory : IPersonFactory
    {
        public PersonFactory()
        {
        }

        public Person CreateNewPerson(ILocusBank locusBank)
        {
            var genes = new List<IGene>();
            foreach (var locus in locusBank.Loci)
            {
                var gene = GenerateNewGene(locus);
                genes.Add(gene);
            }

            var person = new Person(genes, CreateMaleOrFemale());
            return person;
        }

        public IPerson CreateChild(IPerson person, IPerson partner)
        {
            foreach (var gene in person.Genes)
            {

            }
            var child = new Person();
            return child;
        }

        private Gene GenerateNewGene(ILocus locus)
        {
            var gene = new Gene(GenerateAllele(locus.AlleleManager), GenerateAllele(locus.AlleleManager), locus.LocusId);

            return gene;
        }


        private bool CreateMaleOrFemale()
        {
            return (TrulyRandomGenerator.BooleanGenerator(1000, 500));
        }
        private IAllele GenerateAllele(IAlleleManager manager)
        {

            var num = TrulyRandomGenerator.DoubleGenerator(1000);
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
