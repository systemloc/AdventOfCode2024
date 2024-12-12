using Xunit;
using FluentAssertions;
using Day_01;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Day_01.Test;

public class _01_Tests {
    [Fact]
    public void InputParser_ParsesTestInput() {

        InputProcessor inputProcessor = new InputProcessor(Input.testInput);
        inputProcessor.set1.Count().Should().Be(TestInput.set1.Count());
        inputProcessor.set2.Count().Should().Be(TestInput.set2.Count());
        for (int i = 0; i < TestInput.set1.Count(); i++) {
            inputProcessor.set1[i].Should().Be(TestInput.set1[i]);
            inputProcessor.set2[i].Should().Be(TestInput.set2[i]);
        }
    }

    [Fact]
    public void CalcSortedDifferenceList_Works() {
        List<int> differences = Util.CalcSortedDifferenceList(
            TestInput.set1.ToList(), TestInput.set2.ToList());
        int[] desiredOutput = { 2, 1, 0, 1, 2, 5 };
        differences.Count().Should().Be(desiredOutput.Count());
        for (int i = 0; i < differences.Count();i++)
            differences[i].Should().Be(desiredOutput[i]);
    }

    [Fact]
    public void CalcSimilarityScore_Test() {
        int[] desiredOutput = new int[] { 9, 4, 0, 0, 9, 9 };
        var output = Util.CalcSimilarityScore(TestInput.set1, TestInput.set2);
        output.Count().Should().Be(desiredOutput.Count());
        for (int i = 0; i < output.Count(); i++)
            output[i].Should().Be(desiredOutput[i]);
    }
}

public static class TestInput {
    public static int[] set1 = { 3, 4, 2, 1, 3, 3 };
    public static int[] set2 = { 4, 3, 5, 3, 9, 3 };
}