using System;

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
            var person = new Person();

            return person;
        }
    }
}
