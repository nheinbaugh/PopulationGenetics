using System;

namespace PopulationGenetics.Library
{
    public interface IPerson
    {
        int Age { get; }
        bool IsFemale { get; }
    }
    public class Person : IPerson
    {

        private int _age;
        private bool _isFemale;

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public bool IsFemale
        {
            get { return _isFemale; }
            set { _isFemale = value; }
        }

        public Person()
        {

        }
    } 
}
