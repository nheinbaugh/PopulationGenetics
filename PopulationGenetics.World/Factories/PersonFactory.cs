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
        IPerson CreateChild(IPerson person, IPerson partner, ILocusBank locusBank);
        IPerson MakeBaby(IPerson initializer, List<IPerson> genderList, ILocusBank locusBank);
    }
    public class PersonFactory : IPersonFactory
    {
        private IRandomGenerator _random;

        public PersonFactory(IRandomGenerator random)
        {
            _random = random;
        }

        public Person CreateNewPerson(ILocusBank locusBank)
        {
            var genes = new List<IGene>();
            foreach (var locus in locusBank.Loci)
            {
                var gene = GenerateNewGene(locus);
                genes.Add(gene);
            }

            var person = new Person(_random, genes, CreateMaleOrFemale());
            return person;
        }

        public IPerson CreateChild(IPerson person, IPerson partner, ILocusBank locusbank)
        {
            var genes = new List<IGene>();
            foreach (var gene in person.Genes)
            {
                var alleles = new List<IAllele>();
                alleles.Add(locusbank.GetAlleleById(SelectAllele(gene)));
                var partnerGene = from g in partner.Genes
                    where g.LocusId == gene.LocusId
                    select g;
                var bob = partnerGene.ToList();
                    alleles.Add(locusbank.GetAlleleById(SelectAllele(bob[0])));

                genes.Add(new Gene(alleles[0], alleles[1], gene.LocusId));
            }
            var child = new Person(_random, genes, _random.BooleanGenerator(1000, 500));
            return child;
        }

        public Guid SelectAllele(IGene gene)
        {
            // no room for mutation at this time :)
            if (_random.BooleanGenerator(1000, 500))
                return gene.Alleles[0];
            return gene.Alleles[1];
        }
        public IAllele RetrieveAllele(IAlleleManager manager)
        {
            var num = _random.DoubleGenerator(1000);
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

        private Gene GenerateNewGene(ILocus locus)
        {
            return new Gene(RetrieveAllele(locus.AlleleManager), RetrieveAllele(locus.AlleleManager), locus.LocusId);
        }


        private bool CreateMaleOrFemale()
        {
            return (_random.BooleanGenerator(1000, 500));
        }

        public IPerson MakeBaby(IPerson initializer, List<IPerson> genderList, ILocusBank _registeredGenes)
        {
            var partner = FindPartner(initializer, genderList);
            if (partner != null)
            {
                var baby = CreateChild(initializer, partner, _registeredGenes);
                if (initializer.IsFemale) initializer.GetPregnant();
                return baby;
            }
            return null;
        }

        /// <summary>
        /// Check if the selected person object will create an offspring
        /// </summary>
        /// <param name="initializer">The person being checked</param>
        /// <param name="procreateChance">The odds that a person will procreate (based on age?)</param>
        /// <returns></returns>
        private IPerson FindPartner(IPerson initializer, List<IPerson> genderList)
        {
            var rand = new Random();
            if (_random.BooleanGenerator(1000, 100))
            {
                int seed;
                if (genderList.Count == 0) return null;
                seed = rand.Next(genderList.Count);
                var partner = genderList[seed];
                return partner.IsPregnant ? null : partner;
            }
            return null;
        }
    }
}
