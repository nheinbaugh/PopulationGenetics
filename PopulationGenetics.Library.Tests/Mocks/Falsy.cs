using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library.Tests.Mocks
{
    public class Falsy : IRandomGenerator
    {
        public double DoubleGenerator(int max)
        {
            return 1;
        }

        public bool BooleanGenerator(int maxNumber, int min)
        {
            return false;
        }
    }
}