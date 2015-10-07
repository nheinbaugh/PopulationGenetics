using System.Collections.Generic;
using NUnit.Framework;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library.Tests
{
    [TestFixture]
    public class PersonTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void NewPersonShouldBeAge_Zero()
        {
            var person = new Person();
            Assert.AreEqual(0, person.Age);
        }

        [Test]
        public void NewPersonShouldBeMaleUnlessSpecified()
        {
            var person = new Person();
            Assert.AreEqual(false, person.IsFemale);
        }

        [Test]
        public void NewPersonShouldBeFemaleIfSpecified()
        {
            var person = new Person(new List<IGene>(), true);
            Assert.AreEqual(true, person.IsFemale);
        }

        [Test]
        public void NewPersonShouldNotBePregnant()
        {
            var person = new Person(new List<IGene>(), true);
            Assert.AreEqual(false, person.IsPregnant);
        }

        [TestCase(0)]
        [TestCase(3)]
        [TestCase(265)]
        public void GeneCountShouldMatchNumberOfGenesPassedIn(int numberOfGenes)
        {
            var genes = new List<IGene>();
            for (int i = 0; i < numberOfGenes; i++)
            {
                genes.Add(new Gene());
            }
            var person = new Person(genes, true);
            Assert.AreEqual(numberOfGenes, person.Genes.Count);
        }

        [Test]
        public void GetPregnantShouldMakePersonPreggers()
        {
            var person = new Person(new List<IGene>(), true);
            person.GetPregnant();
            Assert.AreEqual(true, person.IsPregnant);
        }

        //CheckIfBreedingEligible Tests

        [TestCase(0, true)]
        [TestCase(1, true)]
        [TestCase(0, false)]
        [TestCase(1, false)]
        public void PeopleShouldNotBeBreedingEligibleBelow2(int toAge, bool isFemale)
        {
            var curve = new MortalityCurve();
            var person = new Person(new List<IGene>(), isFemale);
            for (int i = 0; i < toAge; i++)
            {
                person.AgePerson(curve);
            }
            Assert.AreEqual(false, person.EligibleForBreeding);
        }

        [TestCase(2, true)]
        [TestCase(3, true)]
        [TestCase(4, true)]
        [TestCase(5, true)]
        [TestCase(6, true)]
        [TestCase(2, false)]
        [TestCase(3, false)]
        [TestCase(4, false)]
        [TestCase(5, false)]
        [TestCase(6, false)]
        public void PeopleAboveAge2ShouldBeEligibleToBreed(int toAge, bool isFemale)
        {
            var curve = new MortalityCurve();
            var person = new Person(new List<IGene>(), isFemale);
            for (int i = 0; i < toAge; i++)
            {
                person.AgePerson(curve);
            }
            Assert.AreEqual(true, person.EligibleForBreeding);
        }

        [TestCase(7)]
        [TestCase(19)]
        [TestCase(9)]
        [TestCase(235)]
        public void FemalesAboveAge_6_ShouldNotBeBreedingEligible(int toAge)
        {
            var curve = new MortalityCurve();
            var person = new Person(new List<IGene>(), true);
            for (int i = 0; i < toAge; i++)
            {
                person.AgePerson(curve);
            }
            Assert.AreEqual(false, person.EligibleForBreeding);
        }

        [Test]
        public void IfPregnant_ShouldNotBeBreedingEligible()
        {
            var curve = new MortalityCurve();
            var person = new Person(new List<IGene>(), true);
            for (int i = 0; i < 5; i++)
            {
                person.AgePerson(curve);
            }
            person.GetPregnant();
            Assert.AreEqual(false, person.EligibleForBreeding);
        }

        [Test]
        public void PersonToggledAsPregnant_ShouldNotBeBreedingEligible()
        {
            var curve = new MortalityCurve();
            var person = new Person(new List<IGene>(), true);
            for (int i = 0; i < 3; i++)
            {
                person.AgePerson(curve);
            }
            person.GetPregnant();
            Assert.AreEqual(false, person.EligibleForBreeding);
        }

        [Test]
        public void IfPersonIsPregnant_ShouldNotBeEligibleToBreed()
        {
            var curve = new MortalityCurve();

            var person = new Person(new List<IGene>(), true);
            for (int i = 0; i < 3; i++)
            {
                person.AgePerson(curve);
            }
            person.GetPregnant();
            // make the person actually preggers
            person.AgePerson(curve);
            Assert.AreEqual(false, person.EligibleForBreeding);
        }

        [Test]
        public void IfPersonHadBabyLastTurn_ShouldNotBeEligibleToBreed()
        {
            var curve = new MortalityCurve();
            var person = new Person(new List<IGene>(), true);
            for (int i = 0; i < 3; i++)
            {
                person.AgePerson(curve);
            }
            person.GetPregnant();
            // make the person actually preggers
            person.AgePerson(curve);
            // this is the turn post preggers
            person.AgePerson(curve);
            Assert.AreEqual(false, person.EligibleForBreeding);
        }

    }
}