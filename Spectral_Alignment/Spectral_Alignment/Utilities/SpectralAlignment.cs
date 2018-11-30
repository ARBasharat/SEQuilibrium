using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Spectral_Alignment.DTO;

namespace Spectral_Alignment.Utilities
{
    public static class SpectralAlignment
    {
        // These variable will be used in Grid.cs functions for evaluating grids
        public static double Tolerance;
        public static int MaximumNoOfShifts;
        public static List<double> SpecificModifications;
        public static List<AlignmentScore> AlignmentScores;
        public static List<double> ExperimentalMassList;
        public static Indices Index;
        public static int[,] Grid;

        /// <summary>
        ///     This function will align the theoretical and experimental spectra and will print the results in the Console.
        ///     Please note, only three general modifications have been considered in alignment process.
        /// </summary>
        /// <param name="experiment"> Experimental MS data </param>
        /// <param name="proteins"> Candidate Protein List </param>
        /// <param name="errorTolerance"> Fragment tolerance for determing fragment match </param>
        /// <param name="noOfModificationsAllowedInAlignment"> Maximum no of Mass shifts allowed in spectral alignment </param>
        public static void Align(MsPeaksDto experiment, List<ProteinInfo> proteins, double errorTolerance, int noOfModificationsAllowedInAlignment)
        {
            // Initializing variables
            ExperimentalMassList = new List<double>();
            AlignmentScores = new List<AlignmentScore>();
            MaximumNoOfShifts = noOfModificationsAllowedInAlignment;
            Tolerance = errorTolerance;
            
            // Calculating Prefix Mass List
            ExperimentalMassList.AddRange(experiment.Mass.Select(t => t - 17.026));
            ExperimentalMassList.AddRange(experiment.Mass.Select(t => experiment.WholeProteinMolecularWeight - t));
            ExperimentalMassList.Sort();
            var n = ExperimentalMassList.Count;

            string baseAbsoluteUri = Directory.GetCurrentDirectory();
            string relativePath = @"\Output\SpectralAlignmentResults.txt";
                       
            string path = Path.GetFullPath(baseAbsoluteUri + relativePath);

            if (proteins.Count == 0)
            {
				if (!File.Exists(path))
				{
                    var outFile = File.Create(path);
                    outFile.Close();
                    TextWriter tw = new StreamWriter(path);
					tw.WriteLine("No Protein Found!");
					tw.Close();
				}
				else if (File.Exists(path))
				{
					using(var tw = new StreamWriter(path, true))
					{
						tw.WriteLine("No Protein Found!");
					}
				}

                //Printing Results if no of candidate proteins are zero
                Console.WriteLine("No Protein Found.");
                Console.WriteLine("\nPress Any Key to Continue!");
                Console.ReadKey();
            }
            else
            {
                // Protein theoretical fragments
                var theoreticalMassList = proteins[0].TheoreticalFragments;
                theoreticalMassList.Sort();
                var m = theoreticalMassList.Count;
                
                // A variable containing theoretical and experimental fragment count
                Index = new Indices(n, m);

                // Specific modifications ( Acetylation, Methylation, DiMethylation)
                SpecificModifications = new List<double> { 42.0106, 14.0156, 28.0313 };

                // Initialize the Grid with 0s
                Grid = new int[Index.X, Index.Y];
                for (var i = 0; i < Index.X; i++)
                    for (var j = 0; j < Index.Y; j++)
                        Grid[i, j] = 0;

                // For determining alignment without mass Shifts
                AlignmentScores.Add(new AlignmentScore(Utilities.Grid.EvaluateGrid(theoreticalMassList), 0));

                // For determining alignment with mass Shifts
                Utilities.Grid.EvaluateGridWithMassShifts(MaximumNoOfShifts, theoreticalMassList);

                // Get Longest Alignment with allowed number of mass shifts
                var shortlistedAllignments =
                    AlignmentScores.Where(x => x.NoOfShifts == noOfModificationsAllowedInAlignment)
                        .Select(t => t.Indices)
                        .ToList();

                if (shortlistedAllignments.Count > 0)
                {
                    // Get the Longest Alignment
                    var alignment = shortlistedAllignments.OrderByDescending(x => x.Count).First();

					if (!File.Exists(path))
					{
                        var outFile = File.Create(path);
                        outFile.Close();
                        TextWriter tw = new StreamWriter(path);
						// Printing Results
						foreach (var index in alignment)
						{
							tw.WriteLine("(" + index.X + ", " + index.Y + "): Exp[" + index.X + "] = " + ExperimentalMassList[index.X] + ", Thr["+index.Y+"] = " +
											  theoreticalMassList[index.Y] +
											  " & Difference = " + Convert.ToSingle(Math.Abs(ExperimentalMassList[index.X] - theoreticalMassList[index.Y])));
							Console.WriteLine("(" + index.X + ", " + index.Y + "): Exp[" + index.X + "] = " + ExperimentalMassList[index.X] + ", Thr["+index.Y+"] = " +
											  theoreticalMassList[index.Y] +
											  " & Difference = " + Convert.ToSingle(Math.Abs(ExperimentalMassList[index.X] - theoreticalMassList[index.Y])));
						}
						Console.WriteLine("No of Peaks Matched: " + (alignment.Count-1));
						tw.WriteLine("No of Peaks Matched: " + (alignment.Count-1));
						tw.Close();
						
					}
					else if (File.Exists(path))
					{
						using(var tw = new StreamWriter(path, true))
						{
							// Printing Results
							foreach (var index in alignment)
							{
								tw.WriteLine("(" + index.X + ", " + index.Y + "): Exp[" + index.X + "] = " + ExperimentalMassList[index.X] + ", Thr["+index.Y+"] = " +
												  theoreticalMassList[index.Y] +
												  " & Difference = " + Convert.ToSingle(Math.Abs(ExperimentalMassList[index.X] - theoreticalMassList[index.Y])));
								Console.WriteLine("(" + index.X + ", " + index.Y + "): Exp[" + index.X + "] = " + ExperimentalMassList[index.X] + ", Thr["+index.Y+"] = " +
												  theoreticalMassList[index.Y] +
												  " & Difference = " + Convert.ToSingle(Math.Abs(ExperimentalMassList[index.X] - theoreticalMassList[index.Y])));
							}
							Console.WriteLine("No of Peaks Matched: " + (alignment.Count-1));
							tw.WriteLine("No of Peaks Matched: " + (alignment.Count-1));
						}
					}
                }
                else
				{
					if (!File.Exists(path))
					{
						File.Create(path);
						TextWriter tw = new StreamWriter(path);
						tw.WriteLine("\nNo of Peaks Matched at f = "+ MaximumNoOfShifts +" are " + 0);
						tw.Close();
					}
					else if (File.Exists(path))
					{
						using(var tw = new StreamWriter(path, true))
						{
							tw.WriteLine("\nNo of Peaks Matched at f = "+ MaximumNoOfShifts +" are " + 0);
						}
					}
                    Console.WriteLine("\nNo of Peaks Matched at f = "+ MaximumNoOfShifts +" are " + 0);
				}
                Console.WriteLine("\n*** Press Any Key to Continue!");
                Console.ReadKey();
            }
        }

    }
}

