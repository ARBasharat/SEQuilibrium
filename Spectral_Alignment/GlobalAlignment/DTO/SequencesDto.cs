using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalAlignment.DTO
{
    public class SequencesDto
    {
        public string Sequence1;
        public string Sequence2;

        public SequencesDto()
        {
            Sequence1 = "CGGTTCGACGTTAC";
            Sequence2 = "CCGTTACGGCTTGC";
        }

        public SequencesDto(string s1, string s2)
        {
            Sequence1 = s1;
            Sequence2 = s2;
        }
    }
}
