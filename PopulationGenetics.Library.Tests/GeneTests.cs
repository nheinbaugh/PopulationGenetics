using System;
using NUnit.Framework;

namespace PopulationGenetics.Library.Tests
{
    [TestFixture]
    public class GeneTests
    {
        [TestCase("A", true, "B", false)]
        [TestCase("B", true, "G", false)]
        [TestCase("C", true, "A", false)]
        [TestCase("1", true, "A", false)]
        [TestCase("C", true, "3", false)]
        public void BuildRepresentation_OneDominant(string a1r, bool a1d, string a2r, bool a2d)
        {
            var a1 = new Allele(a1r, a1d);
            var a2 = new Allele(a2r, a2d);
            var gene = new Gene(a1, a2, Guid.NewGuid());
            Assert.AreEqual(a1r, gene.Representation);
        }

        [TestCase("A", "B", "AB")]
        [TestCase("G", "B", "BG")]
        [TestCase("A", "P", "AP")]
        [TestCase("A", "b", "AB")]
        [TestCase("d", "b", "BD")]
        [TestCase("1", "3", "13")]
        [TestCase("6", "3", "36")]
        public void BuildRepresentation_NoDominant(string a1r, string a2r, string expected)
        {
            var a1 = new Allele(a1r, false);
            var a2 = new Allele(a2r, false);
            var gene = new Gene(a1, a2, Guid.NewGuid());
            Assert.AreEqual(expected, gene.Representation);
        }

        [TestCase("A", "B", "AB")]
        [TestCase("E", "D", "DE")]
        [TestCase("a", "b", "AB")]
        [TestCase("d", "b", "BD")]
        [TestCase("1", "3", "13")]
        [TestCase("6", "3", "36")]
        public void BuildRepresentation_BothDominant(string a1r, string a2r, string expected)
        {
            var a1 = new Allele(a1r, true);
            var a2 = new Allele(a2r, true);
            var gene = new Gene(a1, a2, Guid.NewGuid());
            Assert.AreEqual(expected, gene.Representation);
        }
    }
}