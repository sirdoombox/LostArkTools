using System;
using System.Collections;
using LostArkTools.Extensions;
using LostArkTools.Misc;

namespace LostArkTools.Models.Faceting;

public struct State
{
    private static readonly Random Rand = new();
    
    public Chance Chance { get; private set; }
    public byte[] Remaining { get; private set; }

    public State(GameState state)
    {
        Chance = state.CurrChance;
        Remaining = new[]
        {
            (byte)(state.NumSlots - state.Rows[0].Count),
            (byte)(state.NumSlots - state.Rows[1].Count),
            (byte)(state.NumSlots - state.Rows[2].Count)
        };
    }

    public State(Chance chance, byte[] remaining)
    {
        Chance = chance;
        Remaining = (byte[])remaining.Clone();
    }

    public List<int> AvailableChoices()
    {
        var res = new List<int>();
        for (var i = 0; i < 3; i++)
            if (Remaining[i] > 0)
                res.Add(i);
        return res;
    }

    public Queue<int> AvailableChoicesStack() => new(AvailableChoices());

    public (State, State) Transition(int choice)
    {
        if (Remaining[choice] <= 0) throw new Exception();
        var success = new State(Chance, Remaining);
        var fail = new State(Chance, Remaining);
        success.Remaining[choice]--;
        fail.Remaining[choice]--;
        success.Chance = success.Chance.Up();
        fail.Chance = fail.Chance.Down();
        return (success, fail);
    }

    public bool Update(int choice)
    {
        if (Remaining[choice] <= 0) throw new Exception();
        Remaining[choice]--;
        if (Rand.NextDouble() <= Chance.AsDouble())
        {
            Chance = Chance.Down();
            return true;
        }

        Chance = Chance.Up();
        return false;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Chance,
            ((IStructuralEquatable)Remaining).GetHashCode(EqualityComparer<byte>.Default));
    }

    public override bool Equals(object obj)
    {
        if (obj is State state)
            return state.GetHashCode() == GetHashCode();
        return false;
    }
}