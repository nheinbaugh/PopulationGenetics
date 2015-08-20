using System.Collections.Generic;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library
{
    public class MortalityCurve : IMortalityCurve
    {
        private List<int> _survivalRates;

        public MortalityCurve()
        {
            
        }
        public int GetMortalityByAge(int age)
        {
            return _survivalRates[age];
        }

        public void SetMortalityCurve(List<int> rates)
        {
            _survivalRates = rates;
        }
    }
}