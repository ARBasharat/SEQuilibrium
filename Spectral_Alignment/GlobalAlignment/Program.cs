using GlobalAlignment.DTO;
using GlobalAlignment.Utilities;
using Spectral_Alignment.Utilities;

namespace GlobalAlignment
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Not Required, we will be using RunTheCode(~)
        }

        public static void RunTheCode(string seq1File, string seq2File, int matchWeight, int misMatchWeight, int indlWeight)
        {
            ScoringParameterDto scoringParameter = new ScoringParameterDto(matchWeight, misMatchWeight, indlWeight);
            var seq1= LoadProteinDatabase.GetProteins(seq1File);
            var seq2= LoadProteinDatabase.GetProteins(seq2File);
            SequencesDto sequences = new SequencesDto(seq1[0].Seq, seq2[0].Seq);
            
            EvaluateDto evaluation = GridEvaluateFunction.Evaluate(scoringParameter, sequences);
            ResultsDto results = Traceback.Trace(evaluation, sequences);
            WriteAlignment.Write(results, scoringParameter);
        }
    }
}