using System;
using System.Collections.Generic;
using System.Text;
using GlobalAlignment.DTO;
using System.Linq;

namespace GlobalAlignment.Utilities
{
    public static class GridEvaluateFunction
    {
        public static EvaluateDto Evaluate(ScoringParameterDto scoringParameters, SequencesDto sequences)
        {
            int[,] matrx = new int[sequences.Sequence1.Length + 1, sequences.Sequence2.Length + 1];
            List<int>[,] direx = new List<int>[sequences.Sequence1.Length + 1, sequences.Sequence2.Length + 1];

            for (int j = 0; j < sequences.Sequence2.Length + 1; j++)
            {
                for (int i = 0; i < sequences.Sequence1.Length + 1; i++)
                {
                    direx[i, j] = new List<int>();
                    if (i == 0 || j == 0)
                    {
                        direx[i, j].Add(0);
                        direx[i, j].Add(0);
                        direx[i, j].Add(0);
                    }
                }
            }

            for (int j = 1; j < sequences.Sequence2.Length + 1; j++)
            {
                for (int i = 1; i < sequences.Sequence1.Length + 1; i++)
                {

                    if (sequences.Sequence1[i - 1] == sequences.Sequence2[j - 1])
                    {
                        direx[i, j].Add(matrx[i, j - 1] + scoringParameters.gap);
                        direx[i, j].Add(matrx[i - 1, j] + scoringParameters.gap);
                        direx[i, j].Add(matrx[i - 1, j - 1] + scoringParameters.match);

                    }
                    else
                    {
                        direx[i, j].Add(matrx[i, j - 1] + scoringParameters.gap);
                        direx[i, j].Add(matrx[i - 1, j] + scoringParameters.gap);
                        direx[i, j].Add(matrx[i - 1, j - 1] + scoringParameters.mismatch);

                    }

                    matrx[i, j] = direx[i, j].Max();

                }
            }

            EvaluateDto evaluation = new EvaluateDto(matrx, direx);
            return evaluation;
        }
    }
}
