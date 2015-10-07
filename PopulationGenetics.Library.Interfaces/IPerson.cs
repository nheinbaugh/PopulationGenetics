using System;
using System.Collections.Generic;
using System.Linq;

namespace PopulationGenetics.Library.Interfaces
{
    public interface IPerson
    {
        Guid PersonId { get; }
        int Age { get; }
        bool IsFemale { get; }
        bool IsPregnant { get; }
        List<IGene> Genes { get; }
        bool AgePerson(IMortalityCurve curve);
        bool EligibleForBreeding { get; }
        void GetPregnant();
    }
}
