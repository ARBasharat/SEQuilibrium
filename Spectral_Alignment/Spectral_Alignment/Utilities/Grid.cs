using System;
using System.Collections.Generic;
using System.Linq;
using Spectral_Alignment.DTO;

namespace Spectral_Alignment.Utilities
{
    public static class Grid
    {
        /// <summary>
        ///     This is a recursive function and will be employed for determining spectral alignments with 1 or more mass shifts
        /// </summary>
        /// <param name="numOfShifts">
        ///     Will be used to control the recursion. It will also determine the maximum number of mass
        ///     shifts in the alignment during each cycle
        /// </param>
        /// <param name="theoreticaList">The theoretical list may contain modification depending upon recursive cycle.</param>
        public static void EvaluateGridWithMassShifts(int numOfShifts, List<double> theoreticaList)
        {
            // When maximum number of mass shifts have been considered, the recursion will return to its previous state 
            if (numOfShifts == 0)
                return;

            // pass by Reference to get the possible modifications and their corresponding indices
            var modifications = new List<double>();
            var modificationIndex = new List<Indices>();

            // Find possible modifications by considering a predefined list of modifications 
            FindModifications(modifications, modificationIndex, theoreticaList);


            if (modifications.Count != 0)
            {
                // For each modification, modify theoretical spectrum, evaluate alignment grid and recursive call with modified theoretical spectrum
                for (var i = 0; i < modifications.Count; i++) 
                {
                    var thr = theoreticaList.ToList(); // Make a deep copy

                    // Modify the theoretical mass List for selected modification
                    for (var j = modificationIndex[i].Y; j < theoreticaList.Count; j++)
                    {
                        thr[j] = thr[j] + modifications[i];
                    }

                    // Determine alignment using modified theoretical spectrum
                    SpectralAlignment.AlignmentScores.Add(new AlignmentScore(EvaluateGrid(thr),
                        Math.Abs(SpectralAlignment.MaximumNoOfShifts - numOfShifts) + 1));

                    // Recursive call for allowing an additional mass shift on modified spectrum 
                    EvaluateGridWithMassShifts(numOfShifts - 1, thr);
                }
            }
            else // If we are unable to find the possible modifications, the alignment will be determined without modifying theoretical mass list
            {
                var thr = theoreticaList.ToList();
                SpectralAlignment.AlignmentScores.Add(new AlignmentScore(EvaluateGrid(thr),
                    Math.Abs(SpectralAlignment.MaximumNoOfShifts - numOfShifts) + 1));
                EvaluateGridWithMassShifts(numOfShifts - 1, thr);
            }
        }

        /// <summary>
        ///     This function will evaluate the grid for the given theoretical fragment list. The theoretical list may contain
        ///     modification depending upon calling (parent) function.
        /// </summary>
        /// <param name="theoreticaList">Theoretical Fragment List</param>
        /// <returns>Alignment path for given Theoretical List</returns>
        public static List<Indices> EvaluateGrid(IList<double> theoreticaList)
        {
            // This will contain indices of alignment path for the given theoretical fragment list 
            var alignmentPath = new List<Indices>(); 
            
            // We will start alignment from the index where last element was added to optimize the process 
            var lastTheoreticalForOptimization = 0;

            // Filling the Grid
            for (var spectralIndex = 0; spectralIndex < SpectralAlignment.Index.X; spectralIndex++)
            {
                for (var theoreticalIndex = lastTheoreticalForOptimization; theoreticalIndex < SpectralAlignment.Index.Y; theoreticalIndex++)
                {
                    // Convert Fragment Tolerance value in ppm to Da. 
                    var ppmTol = SpectralAlignment.Tolerance*SpectralAlignment.ExperimentalMassList[spectralIndex]/
                                 1000000;

                    // Compute the difference between theoretical and experimental spectrum
                    var diff = theoreticaList[theoreticalIndex] - SpectralAlignment.ExperimentalMassList[spectralIndex];

                    // If diff is within tolerance fill the Grid and store the alignment path indices
                    if (!(Math.Abs(diff) <= ppmTol)) continue;
                    SpectralAlignment.Grid[spectralIndex, theoreticalIndex] = 1;
                    alignmentPath.Add(new Indices(spectralIndex, theoreticalIndex));
                    lastTheoreticalForOptimization = theoreticalIndex + 1;
                    break;
                }
            }
            return alignmentPath;
        }

        /// <summary>
        ///     This function will find the possible modifications in a theoretical list by considering modification list. In
        ///     current case, only 3 specific modifications has been considered.
        /// </summary>
        /// <param name="returnModifications">List for returing possible modifications by Reference</param>
        /// <param name="returnModificationsIndex">
        ///     List for returing possible modifications Indices by Reference, will be used for
        ///     shifting theoretical fragments list
        /// </param>
        /// <param name="theoreticalList">
        ///     Theoretical Fragment List, he theoretical list may contain modification depending upon
        ///     state of calling function.
        /// </param>
        public static void FindModifications(List<double> returnModifications, List<Indices> returnModificationsIndex,
            List<double> theoreticalList)
        {
            // Find the modifications by accomodating predefind list of mass shifts (modifications)
            for (var spectralIndex = 0; spectralIndex < SpectralAlignment.Index.X; spectralIndex++)
            {
                for (var theoreticalIndex = 0; theoreticalIndex < SpectralAlignment.Index.Y; theoreticalIndex++)
                {
                    foreach (var modification in SpectralAlignment.SpecificModifications)
                    {
                        // Convert Fragment Tolerance value in ppm to Da. 
                        var ppmTol = SpectralAlignment.Tolerance*SpectralAlignment.ExperimentalMassList[spectralIndex]/
                                     1000000;

                        // Compute the difference between theoretical and experimental spectrum after considering modification
                        var diff = theoreticalList[theoreticalIndex] + modification -
                                   SpectralAlignment.ExperimentalMassList[spectralIndex];

                        // If diff is within tolerance, save the modification and its corresponding indices
                        if (!(Math.Abs(diff) <= ppmTol) || SpectralAlignment.Grid[spectralIndex, theoreticalIndex] != 0)
                            continue;
                        returnModifications.Add(modification);
                        returnModificationsIndex.Add(new Indices(spectralIndex, theoreticalIndex));
                        break;
                    }
                }
            }
        }

        /// <summary>
        ///     This function will print the alignment Grid in Console.
        /// </summary>
        public static void PrintMatrix()
        {
            var count = 0;
            for (var i = 0; i < SpectralAlignment.Index.X; i++)
                for (var j = 0; j < SpectralAlignment.Index.Y; j++)
                {
                    if (SpectralAlignment.Grid[i, j] == 0) continue;
                    count++;
                    Console.WriteLine(count + ") Aligned Points: (" + i + "," + j + ") = " +
                                      SpectralAlignment.Grid[i, j]);
                }
        }

    }
}