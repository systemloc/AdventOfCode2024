using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Day_01;

internal class Program {


    static void Main(string[] args) {

        InputProcessor inputProcessor = new InputProcessor(Input.input);
        List<int> set1 = inputProcessor.set1;
        List<int> set2 = inputProcessor.set2;
        inputProcessor = null;

        List<int> difference = Util.CalcSortedDifferenceList(set1, set2);
        Console.WriteLine("Day 1 Answer 1: " + difference.Sum().ToString());

        List<int> similarityScore = Util.CalcSimilarityScore(set1, set2);
        Console.WriteLine("Day 1 Answer 2: " + similarityScore.Sum().ToString());
    }

}

public static class Util {
    public static List<int> CalcSortedDifferenceList(List<int> set1, List<int> set2) {
        if (set1.Count != set2.Count)
            throw new Exception("Parsed integer inputs are unequal length.");
        set1.Sort();
        set2.Sort();

        List<int> differences = 
            Enumerable.Zip(set1, set2, (x,y) => Math.Abs(x-y))
            .ToList();
        return differences;
    }

    public static List<int> CalcSimilarityScore(
        IEnumerable<int> set1, IEnumerable<int> set2) {

        if (set1.Count() != set2.Count())
            throw new Exception("CalcSimilarityScore recieved invalid input. Sets were unequal length.");

        List<int> output = new List<int>();
        foreach (int i in set1)
            output.Add(
                i * set2.Where(p => p.Equals(i)).Count());
        return output;
    }
}


public class InputProcessor {
    public List<int> set1 = new List<int>();
    public List<int > set2 = new List<int>();

    public InputProcessor(string input)
    {
        StringReader reader = new StringReader(input);

        string? line = reader.ReadLine();
        if (line is null)
            throw new Exception($"ParseLine failed. First line of input was null.");
        while (line is not null) {
            ParseLine(line);
            line = reader.ReadLine();
        }
    }

    void ParseLine(string input) {
        string[] splitInputString = Regex.Split(input, @"\s+");
        if (splitInputString.Length != 2)
            throw new Exception($"ParseLine string split failed on line: \n{input}");
        set1.Add(Convert.ToInt32(splitInputString[0]));
        set2.Add(Convert.ToInt32(splitInputString[1]));
    }
}

