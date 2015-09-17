namespace PopulationGenetics.Library.Interfaces
{
    public interface IRandomGenerator
    {
        double DoubleGenerator(int max);
        bool BooleanGenerator(int maxNumber, int min);
    }
}