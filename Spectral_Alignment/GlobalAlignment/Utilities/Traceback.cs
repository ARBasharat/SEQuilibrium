using System;
using System.Collections.Generic;
using System.Text;
using GlobalAlignment.DTO;

namespace GlobalAlignment.Utilities
{
    public static class Traceback
    {
        public static ResultsDto Trace(EvaluateDto eval, SequencesDto seqs)
        {
            string seq1 = seqs.Sequence1;
            string seq2 = seqs.Sequence2;

            string remstrA = seq1;
            string remstrB = seq2;

            int i = seq1.Length;
            int j = seq2.Length;

            string A = "";
            string B = "";

            while(remstrA.Length>0 && remstrB.Length>0)
            {
                if(eval.directs[i,j][0] == eval.matrix[i,j])
                {
                    B = seq2[j - 1].ToString() + B;
                    A = '_'.ToString() + A;
                    j--;
                }
                else if(eval.directs[i, j][1] == eval.matrix[i, j])
                {
                    A = seq1[i - 1].ToString() + A;
                    B = '_'.ToString() + B;
                    i--;
                }
                else if(eval.directs[i, j][2] == eval.matrix[i, j])
                {
                    A = seq1[i - 1].ToString() + A;
                    B = seq2[j - 1].ToString() + B;
                    i--;
                    j--;
                    remstrA = remstrA.Substring(0, i);
                    remstrB = remstrB.Substring(0, j);
                }


            }

            int simScore = eval.matrix[seq1.Length, seq2.Length];
            Alignment align = new Alignment(A, B);
            List<Alignment> aligns = new List<Alignment>();
            aligns.Add(align);
            ResultsDto results = new ResultsDto(aligns, simScore);
            return results;
        }
    }
}
