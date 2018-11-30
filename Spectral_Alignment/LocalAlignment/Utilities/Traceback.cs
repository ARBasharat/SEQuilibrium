using System;
using System.Collections.Generic;
using System.Text;
using LocalAlignment.DTO;
using System.Linq;

namespace LocalAlignment.Utilities
{
    public static class Traceback
    {
        public static ResultsDto Trace(EvaluateDto eval, SequencesDto seqs)
        {
            string seq1 = seqs.Sequence1;
            string seq2 = seqs.Sequence2;

            int largest = eval.matrix.Cast<int>().Max();

            List<Alignment> aligns = new List<Alignment>();

            int c = 0;
            List<int> rowmax = new List<int>();
            List<int> colmax = new List<int>();

            for (int row = 0; row < eval.matrix.GetLength(0); row++)
            {
                for (int col = 0; col < eval.matrix.GetLength(1); col++)
                {
                    if (eval.matrix[row, col] == largest)
                    {
                        rowmax.Add(row);
                        colmax.Add(col);
                        c++;
                    }
                }
            }


            List<string> A = new List<string>();
            List<string> B = new List<string>();

            for (int count = 0; count < c; count++)
            {
                string remstrA = seq1.Substring(0, rowmax[count] - 1);
                string remstrB = seq2.Substring(0, colmax[count] - 1);

                int i = rowmax[count];
                int j = colmax[count];

                A.Add("");
                B.Add("");

                while (remstrA.Length > 0 && remstrB.Length > 0)
                {
                    if (eval.directs[i, j][0] == eval.matrix[i, j])
                    {
                        B[count] = seq2[j - 1].ToString() + B[count];
                        A[count] = '_'.ToString() + A[count];
                        j--;
                    }
                    else if (eval.directs[i, j][1] == eval.matrix[i, j])
                    {
                        A[count] = seq1[i - 1].ToString() + A[count];
                        B[count] = '_'.ToString() + B[count];
                        i--;
                    }
                    else if (eval.directs[i, j][2] == eval.matrix[i, j])
                    {
                        A[count] = seq1[i - 1].ToString() + A[count];
                        B[count] = seq2[j - 1].ToString() + B[count];
                        i--;
                        j--;
                        remstrA = remstrA.Substring(0, i);
                        remstrB = remstrB.Substring(0, j);
                    }
                    else
                    {
                        break;
                    }
                }

                Alignment align = new Alignment(A[count], B[count]);

                aligns.Add(align);
            }

            int simScore = largest;
            ResultsDto results = new ResultsDto(aligns, simScore);
            return results;
        }
    }
}
