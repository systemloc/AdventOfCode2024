using System.ComponentModel.Design;

namespace Day_11;

internal class Program {

    static void Main(string[] args) {
        Part2 solution;
        ulong numberOfStones = 0;

        solution = new(Parse(Input.realInput));
        solution.Iterate(25);

        numberOfStones = 0;
        foreach (var stoneCount in solution.stones.Values)
            numberOfStones += stoneCount;
        Console.WriteLine("Day 11 Part 1: " + numberOfStones);
 
        
        solution = new(Parse(Input.realInput));
        solution.Iterate(75);
        numberOfStones = 0;
        foreach (var stoneCount in solution.stones.Values)
            numberOfStones += stoneCount;
        Console.WriteLine("Day 11 Part 2: " + numberOfStones);
    }

    static StoneDictionary Parse(string input) {
        StoneDictionary stones = new();
        var numberStrings = input.Split(' ');
        foreach (string numberString in numberStrings)
            stones.Increment(Convert.ToUInt64(numberString));
        return stones;
    }
}

public class StoneDictionary : Dictionary<ulong, ulong> {
    public void Increment(ulong stoneNumber) {
        if (this.ContainsKey(stoneNumber))
            this[stoneNumber]++;
        else
            this.Add(stoneNumber, 1);
    }

    public void Increment(ulong stoneNumber, ulong value) {
        if (this.ContainsKey(stoneNumber))
            this[stoneNumber] += value;
        else
            this.Add(stoneNumber, value);
    }
}
