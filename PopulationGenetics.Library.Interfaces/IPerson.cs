using System.Collections.Generic;

namespace PopulationGenetics.Library.Interfaces
{
    public interface IPerson
    {
        int Age { get; }
        bool IsFemale { get; }
        List<IGene> Genes { get; }
    }
}
