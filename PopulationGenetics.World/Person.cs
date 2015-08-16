using System;
using System.Collections.Generic;
using PopulationGenetics.Library.Interfaces;
using System.Linq;

namespace PopulationGenetics.Library
{
    public class Person : IPerson
    {
        private int _age;
        private bool _isFemale;
        private List<IGene> _genes;
        private Guid _personId;

        public Guid PersonId => _personId;
        public int Age => _age;
        public bool IsFemale => _isFemale;
        public List<IGene> Genes => _genes;
        public void AgePerson()
        {
            _age++;
        }

        public Person(List<IGene> genes, bool isFemale)
        {
            _personId = Guid.NewGuid();
            _age = 0;
            _isFemale = isFemale;
            _genes = genes;
        }

        public Person()
        {
            _personId = Guid.NewGuid();
            _genes = new List<IGene>();
        }

    } 
}
