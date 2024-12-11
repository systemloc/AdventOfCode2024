using System.Text;
using System.Text.RegularExpressions;

namespace Day_04;

internal class Program {
    static void Main(string[] args) {
        Grid grid = new Grid();
        GridPosition pos = new();
        grid.Populate(Input.input);
        do {
            Console.Write(grid.Value(pos));
            pos = pos.Right(1);
        } while (grid.IsValid(pos));
        Console.WriteLine();

        pos = new();
        int count = 0;
        while (grid.MoveNext())
            count += grid.SearchPosition("XMAS");
        Console.WriteLine("Day 4 Part 1: " + count);

        pos = new();
        count = 0;
        grid.Position = new GridPosition();
        while (grid.MoveNext())
            if (grid.SearchPosition2("MAS") > 1)
                count++;
        Console.WriteLine("Day 4 Part 1: " + count);

        Console.WriteLine(grid.SearchPosition2(new GridPosition(1,2), "MAS"));
 
    }
}

public class Grid {
    List<List<char>> _grid = new();
    public int Height => _grid.Count;
    public int Width => _grid[0].Count;

    public GridPosition Position = new();

    public void Populate(string input) {
        string[] _rows = Regex.Split(input, "\n");
        foreach (string _row in _rows)
            _grid.Add(_row.TrimEnd().ToList());
    }

    public bool IsValid(GridPosition position) {
        if ( position.Row >= 0 
            && position.Row < this.Height
            && position.Column >= 0
            && position.Column < this.Width )
            return true;
        else
            return false;
    }


    public char Value(GridPosition position) {
        if (this.IsValid(position)) 
            return _grid[position.Row][position.Column];
        else
            throw new Exception("Invalid grid position");
    }

    public int SearchPosition(string target) {
        return SearchPosition(this.Position, target);
    }
    
    public int SearchPosition(GridPosition position, string target) {
        int count = 0;
        if (this.SearchPosition(position, target, (m, p) => p.Right(m)))
            count++;
        if ( this.SearchPosition(position, target, (m, p) => p.Left(m) ))
            count++;
        if ( this.SearchPosition(position, target, (m, p) => p.Up(m) ))
            count++;
        if ( this.SearchPosition(position, target, (m, p) => p.Down(m) ))
            count++;
        if ( this.SearchPosition(position, target, (m, p) => p.Up(m).Right(m) ))
            count++;
        if ( this.SearchPosition(position, target, (m, p) => p.Up(m).Left(m) ))
            count++;
        if ( this.SearchPosition(position, target, (m, p) => p.Down(m).Right(m) ))
            count++;
        if ( this.SearchPosition(position, target, (m, p) => p.Down(m).Left(m) ))
            count++;
        return count;
    }

    public int SearchPosition2(string target) {
        return SearchPosition2(this.Position, target);
    }
 
    public int SearchPosition2(GridPosition position, string target) {
        int count = 0;
        if (this.SearchPosition(position.Down(1).Left(1), target, (m, p) => p.Up(m).Right(m)))
            count++;
        if (this.SearchPosition(position.Down(1).Right(1), target, (m, p) => p.Up(m).Left(m)))
            count++;
        if (this.SearchPosition(position.Up(1).Left(1), target, (m, p) => p.Down(m).Right(m)))
            count++;
        if (this.SearchPosition(position.Up(1).Right(1), target, (m, p) => p.Down(m).Left(m)))
            count++;
        return count;
    }

    public bool SearchPosition(GridPosition position, string target, Func<int, GridPosition, GridPosition> direction) {
        StringBuilder subject = new();

        if (this.IsValid(position)
            && this.IsValid(direction(target.Count()-1, position))) 
            for (int i = 0; i<target.Count(); i++) 
                subject.Append(this.Value(direction(i, position)));
        else
            return false;

        return target.Equals(subject.ToString());
    }


    public bool MoveNext() {
        if (this.IsValid(Position.Right(1)))
            Position = Position.Right(1);
        else {
            Position = Position.Down(1);
            Position.Column = 0;
        }
        return this.IsValid(Position);
    }
}

public class GridPosition {
    public int Row = 0;
    public int Column = 0;

    public GridPosition() { }
    public GridPosition(int row, int column) {
        this.Row = row;
        this.Column = column;
    }

    public GridPosition Left(int distance) =>
        new GridPosition(this.Row, this.Column-distance);     
    public GridPosition Right(int distance) => 
        new GridPosition(this.Row, this.Column+distance);     
    public GridPosition Up(int distance) => 
        new GridPosition(this.Row-distance, this.Column);     
    
    public GridPosition Down(int distance) =>
        new GridPosition(this.Row+distance, this.Column);     
    
}
