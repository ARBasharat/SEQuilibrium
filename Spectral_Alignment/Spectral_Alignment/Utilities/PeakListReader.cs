using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Spectral_Alignment.DTO;

namespace Spectral_Alignment.Utilities
{
    public static class PeakListReader
    {
        /// <summary>
        ///     This function will read the MS data file and return the data in MsPeaksDto
        /// </summary>
        /// <param name="fileAddress">Contains MS data (*.txt) file address</param>
        /// <returns>MS1, MS2 and Intensities</returns>
        public static MsPeaksDto ReadPeakListFile(string fileAddress)
        {
            // Variables to store peak intensity and m/z values
            var intensity = new List<double>();
            var mz = new List<double>();

            // m/z and intensity values can have tab or single space separation
            string[] separators = {"\t", " "};

            // Read File and return MS data
            string line;
            var file = new StreamReader(fileAddress);
            while ((line = file.ReadLine()) != null)
            {
                if (line.Contains('\t') || line.Contains(' '))
                {
                    // split m/z and intensity values in each row
                    var splittedLine = line.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                    // Save mass and intensities
                    mz.Add(double.Parse(splittedLine[0]));
                    intensity.Add(double.Parse(splittedLine[1]));
                }
                else //If peak list data does not contain intensities, add zero in place of intensities
                {
                    // Save mass and intensities
                    mz.Add(double.Parse(line));
                    intensity.Add(0);
                }
            }
            file.Close(); // File reader closed
            var mWeight = mz[0]; // MS1 is the first m/z value in peaklist file
            return new MsPeaksDto(intensity, mz, mWeight);
        }
    }
}
