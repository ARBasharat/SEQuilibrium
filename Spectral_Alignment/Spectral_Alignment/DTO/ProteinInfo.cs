using System.Collections.Generic;

namespace Spectral_Alignment.DTO
{
    /// <summary>
    /// This Data Transfer Object will help with proteins data
    /// </summary>
    public class ProteinInfo
    {
        public string Id { get; set; }
        public string Seq { get; set; }
        public double Mw { get; set; }
        public List<double> TheoreticalFragments { get; set; }
    }
}