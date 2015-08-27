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
        private bool _eligibleForBreeding;
        private bool _pregnant;
        private bool _hadBaby;

        public Guid PersonId => _personId;
        public int Age => _age;
        public bool IsFemale => _isFemale;
        public List<IGene> Genes => _genes;
        public bool EligibleForBreeding { get { return _eligibleForBreeding; } }
        public bool IsPregnant => _pregnant;
        public void HaveBaby()
        {
            _pregnant = true;
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

        /// <summary>
        /// Increase the age of the current person
        /// </summary>
        public void AgePerson()
        {
            
            _eligibleForBreeding = CheckIfBreedingEligible();
            _age++;
            if (_hadBaby)
            {
                _hadBaby = false;
            }
            if (_pregnant)
            {
                _hadBaby = true;
                _pregnant = false;
            }

        }

        private bool CheckIfBreedingEligible()
        {
            if (_isFemale)
            {
                if (_hadBaby) return false;
                if (_age >= 1 && _age < 6)
                {
                    return true;
                }
            }
            else
            {
                if (_age >= 1) return true;
            }
            return false;
        }
    } 
}
