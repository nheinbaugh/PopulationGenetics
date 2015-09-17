using System;
using System.Security.Cryptography;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library
{

    public class TrulyRandomGenerator : IRandomGenerator
    {
        private double Generator(int max)
        {
            var rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[4];

            rng.GetBytes(buffer);
            int result = BitConverter.ToInt32(buffer, 0);

            return new Random(result).Next(0, max + 1);
        }

        public double DoubleGenerator(int max)
        {
            return Generator(max);
        }
        public bool BooleanGenerator(int maxNumber, int min)
        {
            var res = Generator(maxNumber);
            return res > min;
        }
    }
}