namespace LostArkTools.Models.Faceting;

public struct Scoring
{
    public double[] Success { get; }
    public double[] Fail { get; }

    public Scoring()
    {
        Success = new double[3];
        Fail = new double[3];
    }

    public double Eval(byte[] scores, byte count)
    {
        return Success[0] * scores[0]
               + Success[1] * scores[1]
               + Success[2] * scores[2]
               + Fail[0] * (count - scores[0])
               + Fail[1] * (count - scores[1])
               + Fail[2] * (count - scores[2]);
    }

    public double EvalPartial(GameState state)
    {
        var score = 0.0;
        for (var i = 0; i < 3; i++)
            foreach (var succeeded in state.Rows[i])
                score += succeeded ? Success[i] : Fail[i];
        return score;
    }
}