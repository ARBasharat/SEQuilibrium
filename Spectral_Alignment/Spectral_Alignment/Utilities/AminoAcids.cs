namespace Spectral_Alignment.Utilities
{
    public class AminoAcids
    {
        /// <summary>
        /// This function will take amino acid symbol and will return its corresponding Molecular Weight (Mw)
        /// </summary>
        /// <param name="aminoAcid">Amino Acid Symbol</param>
        /// <returns>Molecular Weight of Amino Acid</returns>
        public static double GetMwOfAminoAcid(char aminoAcid)
        {
            var aminoAcidMw = -1.0;
            switch (aminoAcid)
            {
                case 'M':
                    aminoAcidMw = 131.04049;
                    break;
                case 'Q':
                    aminoAcidMw = 128.05858;
                    break;
                case 'A':
                    aminoAcidMw = 71.03711;
                    break;
                case 'R':
                    aminoAcidMw = 156.10111;
                    break;
                case 'N':
                    aminoAcidMw = 114.04293;
                    break;
                case 'D':
                    aminoAcidMw = 115.02694;
                    break;
                case 'C':
                    aminoAcidMw = 103.00919;
                    break;
                case 'E':
                    aminoAcidMw = 129.04259;
                    break;
                case 'G':
                    aminoAcidMw = 57.02146;
                    break;
                case 'H':
                    aminoAcidMw = 137.05891;
                    break;
                case 'I':
                    aminoAcidMw = 113.08406;
                    break;
                case 'L':
                    aminoAcidMw = 113.08406;
                    break;
                case 'K':
                    aminoAcidMw = 128.09496;
                    break;
                case 'F':
                    aminoAcidMw = 147.06841;
                    break;
                case 'P':
                    aminoAcidMw = 97.05276;
                    break;
                case 'S':
                    aminoAcidMw = 87.03203;
                    break;
                case 'T':
                    aminoAcidMw = 101.04768;
                    break;
                case 'W':
                    aminoAcidMw = 186.07931;
                    break;
                case 'Y':
                    aminoAcidMw = 163.06333;
                    break;
                case 'V':
                    aminoAcidMw = 99.06841;
                    break;
                case 'U':
                    aminoAcidMw = 168.964203;
                    break;
                case 'O':
                    aminoAcidMw = 255.158295;
                    break;
                case 'X':
                    aminoAcidMw = 110;
                    break;
            }

            return aminoAcidMw;
        }
    }
}
