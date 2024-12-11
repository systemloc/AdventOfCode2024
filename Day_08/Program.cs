using System.Numerics;

namespace Day_08;

internal class Program {
    static void Main(string[] args) {
        string input = Input.testInput;
        HashSet<char> antennaSymbols = FindAntennaChars(input);
        int width = input.Split('\n')[0].Trim().Length;
        int length = input.Split('\n').Length;

        Dictionary<char, List<(int x, int y)>> antennas = new();
        foreach (char c in antennaSymbols) 
            antennas.Add(c, new());

        foreach (char c in antennas.Keys) {
            antennas[c] = FindAntennaPositions(input, c);
        }

    }

    private static List<(int x, int y)> FindAntennaPositions(string input, char c) {
        List<(int x, int y)> output = new();
        

        return output;
    }

    public static HashSet<char> FindAntennaChars(string input) { 
        HashSet<char> result = new();
        foreach ( char c in input.ToCharArray() ) {
            switch (c) {
                case '.':
                    break;
                case '\n':
                    break;
                case '\r':
                    break;
                case ' ':
                    break;
                case '\t':
                    break;
                default:
                    result.Add(c);
                    break;
            }
        }
        return result;
    }
}
