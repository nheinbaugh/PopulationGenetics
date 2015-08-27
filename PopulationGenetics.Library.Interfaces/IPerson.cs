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
        void AgePerson();
        bool EligibleForBreeding { get; }
        void HaveBaby();
    }
}
