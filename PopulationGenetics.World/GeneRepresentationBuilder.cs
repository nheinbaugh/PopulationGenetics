using System;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library
{
    public static class GeneRepresentationBuilder
    {
        /// <summary>
        /// Take two co-dominant alleles and determine how to display the name in a proper manner (alphabetical)
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static string CreateName(string first, string second)
        {
            first = first.ToUpper();
            second = second.ToUpper();
            var rep = string.Empty;
            var val = String.CompareOrdinal(first, second);
            if (val <= -1)
                rep = first + second;
            else rep = second + first;
            return rep;
        }
    }
}