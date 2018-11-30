using System;
using System.Collections.Generic;
using System.Text;
using GlobalAlignment.DTO;
using System.Linq;

namespace GlobalAlignment.Utilities
{
    public static class WriteAlignment
    {
        public static void Write(ResultsDto results, ScoringParameterDto scores)
        {
            List<Alignment> alignments = results.alignments;
            int simScore = results.SimilarityScore;
            int count = 0;
            int mismatch = 0;
            int gap = 0;
            int match = 0;

            Console.WriteLine("Global Alignment Results \n");

            foreach (var align in alignments)
            {
                int maxlength = Math.Max(align.Sequence1.Length, align.Sequence2.Length);
                Console.WriteLine("Alignment " + ++count);
                Console.WriteLine("Match: {0}", scores.match);
                Console.WriteLine("Mismatch: {0}", scores.mismatch);
                Console.WriteLine("Gap: {0}", scores.gap);
                Console.WriteLine("Similarity Score: " + simScore + "\n");
                Console.WriteLine(align.Sequence1);
                for (int i = 0; i<align.Sequence1.Length; i++)
                {
                    if (align.Sequence1[i] == align.Sequence2[i])
                    {
                        if (align.Sequence1[i].Equals('_')) {
                            gap = gap + 2;
                            Console.Write(" ");
                        }
                        else
                        {
                            Console.Write("|");
                            match++;
                        }            
                    }
                    else if (align.Sequence1[i].Equals('_') || align.Sequence2[i].Equals('_'))
                    {
                        Console.Write(" ");
                        gap++;
                    }
                    else
                    {
                        Console.Write(" ");
                        mismatch++;
                    }
                }
                Console.WriteLine("\n" + align.Sequence2 + "\n");
                Console.WriteLine("Identity: {0}/{1} ({2:F1}%)", match, maxlength, (match/(double)maxlength * 100));
                Console.WriteLine("Gaps: {0}/{1} ({2:F1}%) \n\n", gap, maxlength, (gap / (double)maxlength * 100));

                mismatch = 0;
                gap = 0;
                match = 0;

            }

            Console.ReadKey();

        }

    }
}
