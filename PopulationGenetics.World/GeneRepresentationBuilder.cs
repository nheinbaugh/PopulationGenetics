using System;
using PopulationGenetics.Library.Interfaces;

namespace PopulationGenetics.Library
{
    public static class GeneRepresentationBuilder
    {
        public static string CreateName(IAllele first, IAllele second)
        {
            var rep = string.Empty;
            var val = String.CompareOrdinal(first.Representation, second.Representation);
            if (val == -1)
                rep = first.Representation + second.Representation;
            else rep = second.Representation + first.Representation;
            return rep;
        }
    }
}