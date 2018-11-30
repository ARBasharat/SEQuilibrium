using Spectral_Alignment.Utilities;

namespace Spectral_Alignment
{
    public class Program
    {
        public static void Main()
        {
            // Not Required, we will be using RunTheCode(~)
        }

        /// <summary>
        ///     This function is being used in place of Main(~) and will be called from the RunMe GUI. Moreover, the Protein
        ///     Database "uniprot_human.fasta" only contain 2 proteins. One will be filtered by Protein mass
        ///     filter. So, the alignment is only performed on the Histone H4 (P62805).
        /// </summary>
        /// <param name="peakListFileAddress">Contains MS data (*.txt) file address</param>
        /// <param name="databaseFileAddress">Conatins protein database (*.fasta) file address</param>
        /// <param name="mwTolerance">Tolerance for filtering Proteins having theoretical mass in comparison with MS1</param>
        /// <param name="fragmentTolerance">Tolerance for aligning theoretical and experimental spectra</param>
        /// <param name="maximumNoOfModifications">Maximum number of mass shifts allowed in an alignment</param>
        public static void RunTheCode(string peakListFileAddress, string databaseFileAddress, double mwTolerance,
            double fragmentTolerance, int maximumNoOfModifications)
        {
            // Load Mass Spectrometry Data from *.txt data file
            var massSpectrometryData = PeakListReader.ReadPeakListFile(peakListFileAddress);

            // Load proteins from *.fasta protein database
            var proteinList = LoadProteinDatabase.GetProteins(databaseFileAddress);

            // Applying Terminal Modification, cuurently only handles NME_Accetylation
            var modifiedProteinList = TerminalModification.ModifyProteins(proteinList);

            // Filter Proteins using user provided MW Tolerance
            var filteredProteinList = FilterCandidateProteinsByMs1.FilterProteinDb(modifiedProteinList,
                massSpectrometryData.WholeProteinMolecularWeight, mwTolerance);

            // Evaluate Spectral Alignment and Print the Results in Console
            SpectralAlignment.Align(massSpectrometryData, filteredProteinList, fragmentTolerance,
                maximumNoOfModifications);
        }
    }
}
