using System;
using System.Collections.Generic;
using NUnit.Framework;
using PopulationGenetics.Library.Factories;
using PopulationGenetics.Library.Interfaces;
using PopulationGenetics.Library.Managers;
using PopulationGenetics.Library.Tests.Mocks;

namespace PopulationGenetics.Library.Tests
{
    [TestFixture]
    public class PersonFactoryTests
    {
        [Test]
        public void SelectAllele_ReturnFirstAllele()
        {
            var random = new Truthy();
            var factory = new PersonFactory(random);
            var first = new Allele();
            var second = new Allele();
            var gene = new Gene(first, second, Guid.NewGuid());
            var bob = factory.SelectAllele(gene);
            Assert.AreEqual(bob, first.Id);
        }

        [Test]
        public void SelectAllele_ReturnSecondAllele()
        {
            var random = new Falsy();
            var factory = new PersonFactory(random);
            var first = new Allele();
            var second = new Allele();
            var gene = new Gene(first, second, Guid.NewGuid());
            var bob = factory.SelectAllele(gene);
            Assert.AreEqual(bob, second.Id);
        }

        [TestCase(3, 0)]
        [TestCase(354, 1)]
        [TestCase(893, 2)]
        [TestCase(11054, 0)]
        public void RetrieveAllele(int number, int indexOfResult)
        {
            var random = new Truthy(number);
            var firstAllele = new Allele(.33);
            var secondAllele = new Allele(.33);
            var thirdAllele = new Allele(.33);
            var alleleList = new List<IAllele> {firstAllele, secondAllele, thirdAllele};
            var mgr = new AlleleManager(alleleList);
            var factory = new PersonFactory(random);
            var actual = factory.RetrieveAllele(mgr);
            Assert.AreEqual(alleleList[indexOfResult].Id, actual.Id);
        }
    }
}