using System;

namespace PopulationGenetics.Library.Interfaces
{
    public interface IAllele
    {
        Guid Id { get; }
        double DefaultFrequency { get; }
        string Representation { get; }
        bool IsDominant { get; }
    }
}
