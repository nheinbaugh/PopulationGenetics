namespace PopulationGenetics.Library.Interfaces
{
    public interface IGene
    {
        int FitnessGain { get; }
        string Representation { get; }
    }
}
