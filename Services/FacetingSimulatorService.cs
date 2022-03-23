using System;
using System.Linq;
using System.Threading.Tasks;
using LostArkTools.Extensions;
using LostArkTools.Misc;
using LostArkTools.Models.Faceting;

namespace LostArkTools.Services;

public class FacetingSimulatorService
{
    public Scoring Scoring { get; set; }
    public Dictionary<State, List<Answer>> Optimal { get; set; }
    public byte Count { get; set; }

    public bool IsBuilt => Optimal != null;
    
    public async Task BuildOptimalLookup(Scoring scoring, byte count)
    {
        Scoring = scoring;
        Optimal = new Dictionary<State, List<Answer>>();
        Count = count;
        
        var remaining = new byte[] { 0, 0, 0 };

        while (true)
        {
            foreach (var chance in EnumExtensions.All<Chance>())
            {
                var state = new State(chance, new[] { remaining[0], remaining[1], remaining[2] });
                var availableChoices = state.AvailableChoices();
                if (!availableChoices.Any()) continue;
                var scores = new Stack<Answer>();
                var probSuccess = state.Chance.AsDouble();
                var probFail = 1.0 - probSuccess;
                foreach (var index in availableChoices)
                {
                    var (successState, failState) = state.Transition(index);
                    var lookup = await Lookup(successState);
                    var successScore = lookup is not null ? lookup[0].Score : 0.0;
                    lookup = await Lookup(failState);
                    var failScore = lookup is not null ? lookup[0].Score : 0.0;
                    var score = probSuccess * (Scoring.Success[index] + successScore)
                                + probFail * (Scoring.Fail[index] + failScore);
                    scores.Push(new Answer
                    {
                        Index = index,
                        Score = score
                    });
                }

                var orderedScores = scores.OrderByDescending(x => x.Score).ToList();
                Optimal.Add(state, orderedScores);
            }

            remaining[2]++;
            if (remaining[2] <= Count) continue;
            remaining[2] = 0;
            remaining[1]++;
            if (remaining[1] <= Count) continue;
            remaining[1] = 0;
            remaining[0]++;
            if (remaining[0] > Count) break;
        }
    }

    private Task<List<Answer>> Lookup(State state)
    {
        return Optimal.TryGetValue(state, out var res) ? Task.FromResult(new List<Answer>(res)) : Task.FromResult<List<Answer>>(null);
    }

    public async Task<List<Answer>> SortedChoices(GameState gameState)
    {
        var partialScore = Scoring.EvalPartial(gameState);
        var state = new State(gameState);
        var answers = Lookup(state);
        var res = new List<Answer>();
        foreach (var a in await answers)
        {
            var ans = a;
            ans.Score += partialScore;
            res.Add(ans);
        }

        return res;
    }

    public async Task<byte[]> SimulateOnce(GameState start)
    {
        if (Count != start.NumSlots) throw new Exception();
        var state = new State(start);
        var scores = new[]
        {
            (byte)start.Rows[0].Count,
            (byte)start.Rows[1].Count,
            (byte)start.Rows[2].Count
        };
        while (state.AvailableChoicesStack().TryDequeue(out var choice))
        {
            var ans = Lookup(state);
            if (ans is null) throw new Exception();
            var best = (await ans)[0];
            if (state.Update(best.Index))
                scores[best.Index]++;
        }

        return scores;
    }

    public async Task<List<SimResult>> SimulateTopN(int simTries, GameState start, int n = 10)
    {
        var counts = new Dictionary<byte[], int>(new ByteArrayComparer());
        for (var i = 0; i < simTries; i++)
        {
            var sim = await SimulateOnce(start);
            if (counts.ContainsKey(sim))
                counts[sim]++;
            else
                counts.Add(sim, 1);
        }

        return counts.OrderByDescending(x => x.Value).Take(n).Select(x => new SimResult
        {
            Counts = x.Key,
            Probability = (double)x.Value / simTries,
            Score = Scoring.Eval(x.Key, Count)
        }).ToList();
    }
}