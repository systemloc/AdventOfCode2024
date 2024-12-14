using MyUtils;

namespace Day_12;

internal class Program {
    static void Main(string[] args) {
        MyUtils.Grid grid = new(Input.testInput1);
        grid.PrintGrid();

        GridPosition p = new(0, 0);
        HashSet<GridPosition> visitedPositons = new();


        visitedPositons.Add(p);

        Queue<GridPosition> queue = new();
        char current = grid.Value(p);
        EnqueueIfContains(p.Right(1), current);
        EnqueueIfContains(p.Down(1), current);
        EnqueueIfContains(p.Left(1), current);
        EnqueueIfContains(p.Up(1), current);


        void EnqueueIfContains(GridPosition next, char current) {
            if (grid.IsValid(next)
                && grid.Value(next) == current)
                queue.Enqueue(next);
        }
    }
}

