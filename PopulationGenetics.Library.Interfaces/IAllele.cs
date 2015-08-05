namespace PopulationGenetics.Library.Interfaces
{
    public interface IAllele
    {
        double DefaultFrequency { get; }
        string Representation { get; }
        bool IsDominant { get; }
    }
}
