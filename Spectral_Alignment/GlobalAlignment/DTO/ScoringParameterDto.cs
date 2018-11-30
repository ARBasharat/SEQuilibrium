using System;
using System.Collections.Generic;
using System.Text;

namespace GlobalAlignment.DTO
{
    public class ScoringParameterDto
    {
        public int match;
        public int mismatch;
        public int gap;

        public ScoringParameterDto()
        {
            match = 1;
            mismatch = -5;
            gap = -5;
        }

        public ScoringParameterDto(int m, int i, int g)
        {
            match = m;
            mismatch = i;
            gap = g;
        }

    }
}
