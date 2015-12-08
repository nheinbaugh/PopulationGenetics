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
        private Guid _personId;
        private bool _eligibleForBreeding;
        private bool _pregnant;
        private bool _hadBaby;
        private IRandomGenerator _random;

        public Guid PersonId => _personId;
        public int Age => _age;
        public bool IsFemale => _isFemale;
        public List<IGene> Genes => _genes;
        public bool EligibleForBreeding => _eligibleForBreeding;
        public bool IsPregnant => _pregnant;
        public IRandomGenerator Random => _random;

        public Person(IRandomGenerator random, List<IGene> genes, bool isFemale)
        {
            _random = random;
            _personId = Guid.NewGuid();
            _age = 0;
            _isFemale = isFemale;
            _genes = genes;
        }
        public Person(List<IGene> genes, bool isFemale)
        {
            _personId = Guid.NewGuid();
            _age = 0;
            _isFemale = isFemale;
            _genes = genes;
        }

        public Person(IRandomGenerator random)
        {
            _random = random;
            _personId = Guid.NewGuid();
            _genes = new List<IGene>();
        }

        public Person()
        {
            _personId = Guid.NewGuid();
            _genes = new List<IGene>();
        }

        public void GetPregnant()
        {
            _pregnant = true;
            _eligibleForBreeding = false;
        }

        /// <summary>
        /// Increase the age of the current person
        /// </summary>
        public bool AgePerson(IMortalityCurve curve)
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
            return CheckSurvival(curve);
        }

        public bool CheckIfBreedingEligible()
        {
            if (_isFemale)
            {
                if (_pregnant || _hadBaby) return false;
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

        /// <summary>
        /// Check if a person object is going to survive the current generation
        /// </summary>
        /// <param name="curve">The mortality curve used for that world</param>
        /// <returns></returns>
        public bool CheckSurvival(IMortalityCurve curve)
        {
            var survivalRate = curve.GetMortalityByAge(_age);
            return _random.BooleanGenerator(1000, survivalRate);
        }
    } 
}
