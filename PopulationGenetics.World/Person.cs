using System;
using System.Collections.Generic;

namespace PopulationGenetics.Library
{
    public interface IPerson
    {
        int Age { get; }
        bool IsFemale { get; }
        List<IGene> Genes { get; }
    }
    public class Person : IPerson
    {
        private int _age;
        private bool _isFemale;
        private List<IGene> _genes;

        public int Age { get { return _age; } }
        public bool IsFemale { get { return _isFemale; } }
        public List<IGene> Genes { get { return _genes; } }

        public Person()
        {
            _age = 0;
            //set isfemale randomly
        }

        public Person(bool isFemale)
        {
            _isFemale = isFemale;
            _age = 0;
        }
    } 
}
