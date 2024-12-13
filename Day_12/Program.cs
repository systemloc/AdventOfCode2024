namespace Day_12;

internal class Program {
    static void Main(string[] args) {
        Parse(Input.testInput1);
        Console.WriteLine("Hello, World!");
    }

    static void Parse(string input) {
        string[] lines = input.Split('\n', StringSplitOptions.TrimEntries);
        int gridWidth = lines[0].Length;
        int gridLength = lines.Length;

        MyUtils.Grid grid = new(gridWidth, gridLength);

    }
}

