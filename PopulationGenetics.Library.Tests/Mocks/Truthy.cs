using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library.Tests.Mocks
{
    public class Truthy : IRandomGenerator
    {
        private int _num;

        public Truthy()
        {
            
        }

        public Truthy(int defaultNum)
        {
            _num = defaultNum;
        }
        public double DoubleGenerator(int max)
        {
            return _num;
        }

        public bool BooleanGenerator(int maxNumber, int min)
        {
            return true;
        }
    }
}