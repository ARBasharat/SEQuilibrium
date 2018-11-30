using System;
using System.Collections.Generic;
using System.Text;
using GlobalAlignment.DTO;
using System.Linq;
using System.IO;

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

            string sep = "";

            Console.WriteLine("Global Alignment Results \n");

            string baseAbsoluteUri = Directory.GetCurrentDirectory();
            string relativePath = @"\Output\GlobalAlignmentResults.txt";
            string path = Path.GetFullPath(baseAbsoluteUri + relativePath);

            if (!File.Exists(path))
            {
                var outFile = File.Create(path);
                outFile.Close();
                TextWriter tw = new StreamWriter(path);

                tw.WriteLine("Alignment " + ++count);
                tw.WriteLine("Match: {0}", scores.match);
                tw.WriteLine("Mismatch: {0}", scores.mismatch);
                tw.WriteLine("Gap: {0}", scores.gap);
                tw.WriteLine("Similarity Score: " + simScore + "\n");
                foreach (var align in alignments)
                {
                    int maxlength = Math.Max(align.Sequence1.Length, align.Sequence2.Length);
                    for (int i = 0; i < align.Sequence1.Length; i++)
                    {
                        if (align.Sequence1[i] == align.Sequence2[i])
                        {
                            if (align.Sequence1[i].Equals('_'))
                            {
                                gap = gap + 2;
                                sep += " ";
                            }
                            else
                            {
                                sep += "|";
                                match++;
                            }
                        }
                        else if (align.Sequence1[i].Equals('_') || align.Sequence2[i].Equals('_'))
                        {
                            sep += " ";
                            gap++;
                        }
                        else
                        {
                            sep += " ";
                            mismatch++;
                        }
                    }
                    for (int line = 0; line < align.Sequence1.Length; line += 100)
                    {
                        if (line + 100 < align.Sequence1.Length)
                        {
                            tw.WriteLine(align.Sequence1.Substring(line, 100));
                            tw.WriteLine(sep.Substring(line, 100));
                            tw.WriteLine(align.Sequence2.Substring(line, 100) + "\n");
                        }
                        else
                        {
                            tw.WriteLine(align.Sequence1.Substring(line));
                            tw.WriteLine(sep.Substring(line));
                            tw.WriteLine(align.Sequence2.Substring(line) + "\n");
                        }
                    }
                    tw.WriteLine("Identity: {0}/{1} ({2:F1}%)", match, maxlength, (match / (double)maxlength * 100));
                    tw.WriteLine("Gaps: {0}/{1} ({2:F1}%) \n\n", gap, maxlength, (gap / (double)maxlength * 100));

                    mismatch = 0;
                    gap = 0;
                    match = 0;
                    tw.Close();
                }
            }
            else if (File.Exists(path))
            {
                using (var tw = new StreamWriter(path, true))
                {
                    tw.WriteLine("Alignment " + ++count);
                    tw.WriteLine("Match: {0}", scores.match);
                    tw.WriteLine("Mismatch: {0}", scores.mismatch);
                    tw.WriteLine("Gap: {0}", scores.gap);
                    tw.WriteLine("Similarity Score: " + simScore + "\n");

                    foreach (var align in alignments)
                    {
                        int maxlength = Math.Max(align.Sequence1.Length, align.Sequence2.Length);
                        for (int i = 0; i < align.Sequence1.Length; i++)
                        {
                            if (align.Sequence1[i] == align.Sequence2[i])
                            {
                                if (align.Sequence1[i].Equals('_'))
                                {
                                    gap = gap + 2;
                                    sep += " ";
                                }
                                else
                                {
                                    sep += "|";
                                    match++;
                                }
                            }
                            else if (align.Sequence1[i].Equals('_') || align.Sequence2[i].Equals('_'))
                            {
                                sep += " ";
                                gap++;
                            }
                            else
                            {
                                sep += " ";
                                mismatch++;
                            }
                        }
                        for (int line = 0; line < align.Sequence1.Length; line += 100)
                        {
                            if (line + 100 < align.Sequence1.Length)
                            {
                                tw.WriteLine(align.Sequence1.Substring(line, 100));
                                tw.WriteLine(sep.Substring(line, 100));
                                tw.WriteLine(align.Sequence2.Substring(line, 100) + "\n");
                            }
                            else
                            {
                                tw.WriteLine(align.Sequence1.Substring(line));
                                tw.WriteLine(sep.Substring(line));
                                tw.WriteLine(align.Sequence2.Substring(line) + "\n");
                            }
                        }
                        tw.WriteLine("Identity: {0}/{1} ({2:F1}%)", match, maxlength, (match / (double)maxlength * 100));
                        tw.WriteLine("Gaps: {0}/{1} ({2:F1}%) \n\n", gap, maxlength, (gap / (double)maxlength * 100));

                        mismatch = 0;
                        gap = 0;
                        match = 0;

                    }
                }
            }
            foreach (var align in alignments)
            {
                int maxlength = Math.Max(align.Sequence1.Length, align.Sequence2.Length);
                Console.WriteLine("Alignment " + ++count);
                Console.WriteLine("Match: {0}", scores.match);
                Console.WriteLine("Mismatch: {0}", scores.mismatch);
                Console.WriteLine("Gap: {0}", scores.gap);
                Console.WriteLine("Similarity Score: " + simScore + "\n");

                for (int i = 0; i<align.Sequence1.Length; i++)
                {
                    if (align.Sequence1[i] == align.Sequence2[i])
                    {
                        if (align.Sequence1[i].Equals('_')) {
                            gap = gap + 2;
                            sep += " ";
                        }
                        else
                        {
                            sep += "|";
                            match++;
                        }            
                    }
                    else if (align.Sequence1[i].Equals('_') || align.Sequence2[i].Equals('_'))
                    {
                        sep += " ";
                        gap++;
                    }
                    else
                    {
                        sep += " ";
                        mismatch++;
                    }
                }
                for (int line = 0; line < align.Sequence1.Length; line+=100)
                {
                    if (line + 100 < align.Sequence1.Length)
                    {
                        Console.WriteLine(align.Sequence1.Substring(line, 100));
                        Console.WriteLine(sep.Substring(line, 100));
                        Console.WriteLine(align.Sequence2.Substring(line, 100) + "\n");
                    }
                    else
                    {
                        Console.WriteLine(align.Sequence1.Substring(line));
                        Console.WriteLine(sep.Substring(line));
                        Console.WriteLine(align.Sequence2.Substring(line) + "\n");
                    }
                }
                Console.WriteLine("Identity: {0}/{1} ({2:F1}%)", match, maxlength, (match/(double)maxlength * 100));
                Console.WriteLine("Gaps: {0}/{1} ({2:F1}%) \n\n", gap, maxlength, (gap / (double)maxlength * 100));

                mismatch = 0;
                gap = 0;
                match = 0;

            }
            Console.WriteLine("\n*** Press Any Key to Continue!");
            Console.ReadKey();

        }

    }
}
