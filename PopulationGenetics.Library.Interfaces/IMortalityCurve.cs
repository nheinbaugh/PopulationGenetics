using System.Collections.Generic;

namespace PopulationGenetics.Library.Interfaces
{
    public interface IMortalityCurve
    {
        int GetMortalityByAge(int age);
        void SetMortalityCurve(List<int> curve);
    }
}