using System;
using System.Collections.Generic;
using System.Text;

namespace LocalAlignment.DTO
{
    public class ScoringParameterDto
    {
        public int match;
        public int mismatch;
        public int gap;

        public ScoringParameterDto()
        {
            match = 1;
            mismatch = -1;
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

