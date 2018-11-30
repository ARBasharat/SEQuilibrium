using System;
using System.Collections.Generic;
using System.Text;

namespace LocalAlignment.DTO
{
    public class EvaluateDto
    {
        public int[,] matrix;
        public List<int>[,] directs;

        public EvaluateDto()
        {
            matrix = null;
            directs = null;
        }

        public EvaluateDto(int[,] m, List<int>[,] d)
        {
            matrix = m;
            directs = d;
        }

    }
}
