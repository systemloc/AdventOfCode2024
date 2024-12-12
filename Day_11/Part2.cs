using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_11;
public class Part2 {
    public StoneDictionary stones;
    StoneDictionary newStones;

    public Part2(StoneDictionary stones) {
        this.stones = stones;
    }

    public void Iterate(int iterations) {
        for (int i = 0; i < iterations; i++) {
            newStones = new();
            foreach (var stoneValue in stones.Keys) {
                ProcessStoneValue(stoneValue);
            }
            stones = newStones;
        }
    }

    void ProcessStoneValue(ulong stoneValue) {
        ulong stoneQuantity = stones[stoneValue];
        var stoneValueDigitArr =
            stoneValue.ToString().ToCharArray();
        bool valueHasEvenNumberOfDigits =
            stoneValueDigitArr.Length % 2 == 0;

        if (stoneValue == 0)
            newStones.Increment(1, stoneQuantity);
        else if (valueHasEvenNumberOfDigits) {
            ulong front = Convert.ToUInt64(
                new string(
                new ArraySegment<char>(stoneValueDigitArr,
                        0, stoneValueDigitArr.Length / 2)));
            ulong back = Convert.ToUInt64(
                new string(
                new ArraySegment<char>(stoneValueDigitArr,
                        stoneValueDigitArr.Length / 2, stoneValueDigitArr.Length / 2)));
            newStones.Increment(front, stoneQuantity);
            newStones.Increment(back, stoneQuantity);
        } else
            newStones.Increment(stoneValue * (ulong)2024, stoneQuantity);
    }

}

