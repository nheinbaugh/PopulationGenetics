using System;
using System.Security.Cryptography;

namespace PopulationGenetics.Library
{
    public static class TrulyRandomGenerator
    {
        private static double Generator(int max)
        {
            var rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[4];

            rng.GetBytes(buffer);
            int result = BitConverter.ToInt32(buffer, 0);

            return new Random(result).Next(0, max + 1);
        }

        public static double DoubleGenerator(int max)
        {
            return Generator(max);
        }
        public static bool BooleanGenerator(int maxNumber, int min)
        {
            var res = Generator(maxNumber);
            return res > min;
        }
    }
}