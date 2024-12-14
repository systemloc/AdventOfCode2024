using MyUtils;

namespace Day_12;

internal class Program {
    static void Main(string[] args) {
        MyUtils.Grid grid = new(Input.realInput);
        grid.PrintGrid();

        Part1 part1 = new(grid, grid.Value(new GridPosition(0,0)));
        part1.Run();
        Console.WriteLine(part1.Total());
    }
}

