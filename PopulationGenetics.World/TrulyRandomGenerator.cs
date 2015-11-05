using System;
using System.Security.Cryptography;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library
{

    public class TrulyRandomGenerator : IRandomGenerator
    {
        Random rando;

        public TrulyRandomGenerator()
        {
            CreateRandom();
        }

        private void CreateRandom()
        {
            var rng = new RNGCryptoServiceProvider();
            byte[] buffer = new byte[4];

            rng.GetBytes(buffer);
            int result = BitConverter.ToInt32(buffer, 0);

            rando = new Random(result);
        }
        private double Generator(int max)
        {
            var res = rando.Next(max);
            if(res == 0)
            {
                CreateRandom();
                return Generator(max);
            }
            return res;
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