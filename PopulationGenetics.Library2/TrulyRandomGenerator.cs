using System;
using PopulationGenetics.Library.Interfaces;
using System.Security.Cryptography;


namespace PopulationGenetics.Library
{
public class TrulyRandomGenerator : RandomNumberGenerator, IRandomGenerator
    {
        RandomNumberGenerator r;

        public TrulyRandomGenerator()
        {
            r = RandomNumberGenerator.Create();
        }

        private double Generator(int max)
        {
            return Next(0, max);
        }

        ///<summary>
        /// Fills the elements of a specified array of bytes with random numbers.
        ///</summary>
        ///<param name=”buffer”>An array of bytes to contain random numbers.</param>
        public override void GetBytes(byte[] buffer)
        {
            r.GetBytes(buffer);
        }

        ///<summary>
        /// Returns a random number within the specified range.
        ///</summary>
        ///<param name=”minValue”>The inclusive lower bound of the random number returned.</param>
        ///<param name=”maxValue”>The exclusive upper bound of the random number returned. maxValue must be greater than or equal to minValue.</param>
        public int Next(int minValue, int maxValue)
        {
            return (int)Math.Round(NextDouble() * (maxValue - minValue - 1)) + minValue;
        }

        ///<summary>
        /// Returns a random number between 0.0 and 1.0.
        ///</summary>
        public double NextDouble()
        {
            byte[] b = new byte[4];
            r.GetBytes(b);
            return (double)BitConverter.ToUInt32(b, 0) / UInt32.MaxValue;
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