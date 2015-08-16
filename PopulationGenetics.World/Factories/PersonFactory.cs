using System;
using System.Collections.Generic;
using System.Linq;
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
            var genes = new List<IGene>();
            foreach (var gene in person.Genes)
            {
                var alleles = new List<IAllele>();
                alleles.Add(SelectAllele(gene));
                var partnerGene = partner.Genes.AsQueryable().FirstOrDefault(g => g.LocusId == gene.LocusId);
                if (partnerGene != null)
                {
                    alleles.Add(SelectAllele(partnerGene));
                }
                genes.Add(new Gene(alleles[0], alleles[1], gene.LocusId));
            }
            var child = new Person(genes, TrulyRandomGenerator.BooleanGenerator(1000, 500));
            return child;
        }

        private IAllele SelectAllele(IGene gene)
        {
            if (TrulyRandomGenerator.BooleanGenerator(1000, 500))
                return gene.Alleles[0];
            return gene.Alleles[1];
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
