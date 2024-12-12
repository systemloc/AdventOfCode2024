using Xunit;
using FluentAssertions;
using Day_02;

namespace Day_02.Test;

public class Day2_Tests {
    [Theory]
    [InlineData("1 2 3 4", new int[] { 1, 2, 3, 4 })]
    public void ParseLine_Works(string input, int[] desiredOutput) {
        var output = Utils.ParseLine(input);
        output.Count.Should().Be(desiredOutput.Length);
        for(int i = 0; i < desiredOutput.Length; i++)
            output[i].Should().Be(desiredOutput[i]);
    }

    [Fact]
    public void ParseInput_Works() {
        var desiredOutput = new List<List<int>>();
        desiredOutput.Add(new List<int>() { 7, 6, 4, 2, 1 });
        desiredOutput.Add(new List<int>() { 1, 2, 7, 8, 9 });
        desiredOutput.Add(new List<int>() { 9, 7, 6, 2, 1});
        desiredOutput.Add(new List<int>() { 1, 3, 2, 4, 5});
        desiredOutput.Add(new List<int>() { 8, 6, 4, 4, 1});
        desiredOutput.Add(new List<int>() { 1, 3, 6, 7, 9});

        var output = Utils.ParseInput(Input.testInput);

        output.Count().Should().Be(desiredOutput.Count);
        for (int i = 0; i < output.Count(); i++) {
            output[i].SequenceEqual(desiredOutput[i])
                .Should().Be(true);
        }

    }

    [Theory]
    [InlineData(true, new int[] { 7, 6, 4, 2, 1 })]
    [InlineData(false,new int[] { 1, 2, 7, 8, 9 })]
    [InlineData(false,new int[] { 9, 7, 6, 2, 1})]
    [InlineData(false,new int[] { 1, 3, 2, 4, 5})]
    [InlineData(false,new int[] { 8, 6, 4, 4, 1})]
    [InlineData(true,new int[] { 1, 3, 6, 7, 9})]
    public void ReportIsSafe_Works(bool desiredOutput, int[] input) {
        Utils.ReportIsSafe(input.ToList()).Should().Be(desiredOutput);
    }

    [Theory]
    [InlineData(1,1,false)]
    [InlineData(2,1,false)]
    [InlineData(1,5,false)]
    [InlineData(1,2,true)]
    [InlineData(1,4,true)]
    public void IsSafeChange_Works(int first, int second, 
        bool desiredResult) {
        bool output = Utils.IsSafeAscChange(first, second);
    }

    [Theory]
    [InlineData(-1, new int[] { 7, 6, 4, 2, 1 })]
    [InlineData(2,new int[] { 1, 2, 7, 8, 9 })]
    [InlineData(3,new int[] { 9, 7, 6, 2, 1})]
    [InlineData(2,new int[] { 1, 3, 2, 4, 5})]
    [InlineData(3,new int[] { 8, 6, 4, 4, 1})]
    [InlineData(-1,new int[] { 1, 3, 6, 7, 9})]
    [InlineData(0,new int[] { 9, 5, 6, 7, 9})]
    public void FindBreakingChange_Works(int desiredOutput, int[] input) {
        int output = Utils.FindBreakingChange(input.ToList());
        output.Should().Be(desiredOutput);
    }
 
}