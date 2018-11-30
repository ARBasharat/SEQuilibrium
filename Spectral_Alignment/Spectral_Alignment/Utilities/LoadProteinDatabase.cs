using System.Collections.Generic;
using System.IO;
using System.Linq;
using Spectral_Alignment.DTO;

namespace Spectral_Alignment.Utilities
{
    public class LoadProteinDatabase
    {
        /// <summary>
        ///     This function will read proteins from user provided protein database.
        /// </summary>
        /// <param name="proteinDatabasePath">Conatins protein database (*.fasta) file address</param>
        /// <returns>List of Proteins</returns>
        public static List<ProteinInfo> GetProteins(string proteinDatabasePath)
        {
            //variable initalization
            var counter = 0;
            var proteins = new List<ProteinInfo>();
            var proteinHeader = "";
            var proteinSequence = "";

            //Read protein database and return protein
            string line;
            var file = new StreamReader(proteinDatabasePath);
            while ((line = file.ReadLine()) != null)
            {
                if ((line.Contains('>') && counter > 0) || (line.Contains(' ') && counter > 0))
                {
                    // calculate protein Theoretical MW
                    var proteinMw = AminoAcids.GetMwOfAminoAcid(proteinSequence[0]);
                    proteinMw = proteinSequence.Aggregate(proteinMw,
                        (current, t) => current + AminoAcids.GetMwOfAminoAcid(t));

                    // Extract Protein ID from Protein Header
                    string proteinId;
                    if (proteinHeader.Contains('|'))
                    {
                        var indexofFirstBar = proteinHeader.IndexOf('|');
                        var indexOfSecondBar = proteinHeader.Substring(indexofFirstBar + 1).IndexOf('|');
                        proteinId = proteinHeader.Substring(indexofFirstBar + 1, indexOfSecondBar);
                    }
                    else
                        proteinId = proteinHeader.Substring(1);

                    // Calculate N-terminal theoretical fragments of Protein
                    var theoreticalFragments = new List<double> {AminoAcids.GetMwOfAminoAcid(proteinSequence[0])};
                    for (var fragmentationPositionIter = 1;
                        fragmentationPositionIter < proteinSequence.Length;
                        fragmentationPositionIter++)
                    {
                        theoreticalFragments.Add(theoreticalFragments[fragmentationPositionIter - 1] +
                                                 AminoAcids.GetMwOfAminoAcid(proteinSequence[fragmentationPositionIter]));
                    }

                    // Save Protein Info determined above
                    proteins.Add(new ProteinInfo
                    {
                        Id = proteinId,
                        Mw = proteinMw,
                        Seq = proteinSequence,
                        TheoreticalFragments = theoreticalFragments
                    });

                    //Reset variables
                    proteinHeader = "";
                    proteinSequence = "";
                }

                //Extract Protein Header and Sequence from file
                if (line.Contains(" "))
                    proteinHeader = proteinHeader + line;
                else
                    proteinSequence = proteinSequence + line;

                counter++;
            }

            return proteins;
        }
    }
}
