
using System.Text.RegularExpressions;

namespace Day_03;

internal class Program {
    static void Main(string[] args) {
        int result = Utils.ParsePart1(Input.input);
        Console.WriteLine(result.ToString());
        result = Utils.ParsePart2(Input.input);
        Console.WriteLine(result.ToString());
    }
}

public static class Utils {
    public static int ParsePart1(string inputString) {
        int accum = 0;
        var matches = Regex.Matches(inputString, @"mul\((\d+),(\d+)\)");
        foreach (Match match in matches) {
            accum += Convert.ToInt32(match.Groups[1].Value) *
                     Convert.ToInt32(match.Groups[2].Value);
        }
        return accum;
    }

    public static int ParsePart2(string inputString) {
        int accum = 0;
        var matches = Regex.Matches(inputString, 
            @"mul\((\d+),(\d+)\)|do\(\)|don't\(\)");
        bool state = true;
        foreach (Match match in matches) {
            switch (match.Value) {
                case "don't()":
                    state = false;
                    break;
                case "do()":
                    state = true;
                    break;
                default:
                    if (state)
                        accum += Convert.ToInt32(match.Groups[1].Value) *
                                 Convert.ToInt32(match.Groups[2].Value);
                    break;
            }
        }

        return accum;
    }


}
