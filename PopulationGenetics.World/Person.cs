using System;
using System.Collections.Generic;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library
{
    public class Person : IPerson
    {
        private int _age;
        private bool _isFemale;
        private List<IGene> _genes;

        public int Age => _age;
        public bool IsFemale => _isFemale;
        public List<IGene> Genes => _genes;

        public Person(List<IGene> genes, bool isFemale)
        {
            _age = 0;
            _isFemale = isFemale;
            _genes = genes;
        }

        public Person()
        {

        }
    } 
}
