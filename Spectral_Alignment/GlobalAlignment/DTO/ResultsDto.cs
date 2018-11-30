using System;
using System.Collections.Generic;
using System.Text;
using GlobalAlignment.Utilities;

namespace GlobalAlignment.DTO
{
    public class ResultsDto
    {
        public List<Alignment> alignments;
        public int SimilarityScore;

        public ResultsDto()
        {
            alignments = new List<Alignment>();
            SimilarityScore = -1;
        }

        public ResultsDto(List<Alignment> align, int ss)
        {
            alignments = align;
            SimilarityScore = ss;
        }
    }

    
}
