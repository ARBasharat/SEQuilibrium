using System;
using System.Collections.Generic;
using System.Linq;
using Spectral_Alignment.DTO;

namespace Spectral_Alignment.Utilities
{
    public static class FilterCandidateProteinsByMs1
    {
        /// <summary>
        ///     This function will return those candidate proteins whose mass difference with MS1 lies within user provided
        ///     tolerance.
        /// </summary>
        /// <param name="proteins">List of candidate proteins</param>
        /// <param name="experimentalProteinMass">MS1</param>
        /// <param name="tolerance">MW Tolerance</param>
        /// <returns>Filtered Proteins</returns>
        public static List<ProteinInfo> FilterProteinDb(List<ProteinInfo> proteins, double experimentalProteinMass,
            double tolerance)
        {
            return proteins.Where(protein => Math.Abs(protein.Mw - experimentalProteinMass) <= tolerance).ToList();
        }
    }
}
