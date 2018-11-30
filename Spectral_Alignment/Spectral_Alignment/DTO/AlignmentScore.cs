using System.Collections.Generic;

namespace Spectral_Alignment.DTO
{
    /// <summary>
    /// This Data Transfer Object (DTO) will help in keeping track of spectral alignment score. 
    /// </summary>
    public class AlignmentScore
    {
        public AlignmentScore(List<Indices> indiceses, int f)
        {
            Indices = indiceses;
            NoOfShifts = f;
        }

        public List<Indices> Indices { get; set; }
        public int NoOfShifts { get; set; }
    }
}
