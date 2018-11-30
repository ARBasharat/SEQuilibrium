using System.Collections.Generic;
using Spectral_Alignment.DTO;

namespace Spectral_Alignment.Utilities
{
    public class TerminalModification
    {
        /// <summary>
        ///     This function will take list of candidate proteins as input and will return those proteins after applying terminal
        ///     modification. However, in its current state the function only provides for N-terminal Methionine Excision and Acetylation
        ///     (NME_Acetylation).
        ///     It also performs oxidation on C-Terminal.
        /// </summary>
        /// <param name="proteinList">List of candidate proteins</param>
        /// <returns>List of modified candidate proteins</returns>
        public static List<ProteinInfo> ModifyProteins(List<ProteinInfo> proteinList)
        {
            // Variable Declaration
            var modifiedProteinList = new List<ProteinInfo>();
            const double methionine = 131.04049; //Mass of Methionine
            const double acetylation = 42.0106;
            const double oxidation = 31.9898;

            // N-Terminal Methionine Excision and Acetylation along with C-Terminal Oxidation
            foreach (var protein in proteinList)
            {
                var temporaryFragments = new List<double>();

                // N-Terminal Methionine Excision and Acetylation
                for (var i = 0; i < protein.TheoreticalFragments.Count; i++)
                {
                    if (i == 0)
                        temporaryFragments.Add(protein.TheoreticalFragments[i] - methionine);
                    else
                        temporaryFragments.Add(protein.TheoreticalFragments[i] - methionine + acetylation);
                }

                // Oxidation at last amino acid i.e. C-Terminal
                temporaryFragments[temporaryFragments.Count - 1] = temporaryFragments[temporaryFragments.Count - 1] +
                                                                   oxidation;

                // Saving Modified Protein
                modifiedProteinList.Add(new ProteinInfo
                {
                    Id = protein.Id,
                    Mw = protein.Mw - methionine + acetylation + oxidation,
                    Seq = protein.Seq,
                    TheoreticalFragments = temporaryFragments
                });
            }

            return modifiedProteinList;
        }
    }
}
