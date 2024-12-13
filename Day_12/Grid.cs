using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MyUtils;
public class Grid : IEnumerable<char> {
    char[,] _grid;
    public int Height => _grid.Count;
    public int Width => _grid[0].Count;

    public GridPosition Position = new();

    public Grid(int x, int y) {
        _grid = new char[x, y];
    }

    public void ParseString(string input) {
        string[] _rows = input.Split('\n', StringSplitOptions.TrimEntries);
        int gridWidth = _rows[0].Length;
        int gridLength = _rows.Length;
        _grid = new char[gridWidth, gridLength];

        foreach (string _row in _rows)
            _grid.Add(_row.TrimEnd().ToList());
        this.FindPerson();
    }

    public bool IsValid(GridPosition position) {
        if (position.Row >= 0
            && position.Row < this.Height
            && position.Column >= 0
            && position.Column < this.Width)
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

    public char Value(GridPosition position, char input) {
        if (this.IsValid(position))
            _grid[position.Row][position.Column] = input;
        else
            throw new Exception("Invalid grid position");
        return _grid[position.Row][position.Column];
    }

    public void FindPerson() {
        this.ResetPosition();
        this.MoveNext();
        do {
            if ( '>' == this.Value(Position)
                || '<' == this.Value(Position)
                || '^' == this.Value(Position)
                || 'v' == this.Value(Position) ) {
                break;
            }
            this.MoveNext();
        } while (this.IsValid(this.Position));
    }

    public void MovePerson() {
        if (!this.IsValid(Position))
            return;
        char person = this.Value(Position);
        switch (person) {
            case '>':
                MoveCase('>', 'v', p => p.Right(1));
                break;
            case '<':
                MoveCase('<', '^', p => p.Left(1));
                break;
            case 'v':
                MoveCase('v', '<', p => p.Down(1));
                break;
            case '^':
                MoveCase('^', '>', p => p.Up(1));
                break;
            default:
                throw new Exception("MovePerson fatal error.");
            }


        void MoveCase(char person, char rotation, Func<GridPosition, GridPosition> posMove) {
            if (!this.IsValid(posMove(Position))) {
                this.Value(Position, 'X');
                Position = posMove(Position);
            } else if (this.Value(posMove(Position)) == '#') {
                this.Value(Position, rotation);
                this.MovePerson();
            } else {
                this.Value(Position, 'X');
                this.Value(posMove(Position), person);
                Position = posMove(Position);
            }
        }
    }

    public void PrintGrid() {
        foreach (List<char> gridLine in _grid) {
            foreach (char c in gridLine)
                Console.Write(c);
            Console.WriteLine();
        }
    }

    public int SearchPosition(string target) {
        return SearchPosition(this.Position, target);
    }

    public int SearchPosition(GridPosition position, string target) {
        int count = 0;
        if (this.SearchPosition(position, target, (m, p) => p.Right(m)))
            count++;
        if (this.SearchPosition(position, target, (m, p) => p.Left(m)))
            count++;
        if (this.SearchPosition(position, target, (m, p) => p.Up(m)))
            count++;
        if (this.SearchPosition(position, target, (m, p) => p.Down(m)))
            count++;
        if (this.SearchPosition(position, target, (m, p) => p.Up(m).Right(m)))
            count++;
        if (this.SearchPosition(position, target, (m, p) => p.Up(m).Left(m)))
            count++;
        if (this.SearchPosition(position, target, (m, p) => p.Down(m).Right(m)))
            count++;
        if (this.SearchPosition(position, target, (m, p) => p.Down(m).Left(m)))
            count++;
        return count;
    }

    public bool SearchPosition(GridPosition position, string target, Func<int, GridPosition, GridPosition> direction) {
        StringBuilder subject = new();

        if (this.IsValid(position)
            && this.IsValid(direction(target.Count() - 1, position)))
            for (int i = 0; i < target.Count(); i++)
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
    
    public void ResetPosition() {
        this.Position.Row = 0;
        this.Position.Column = -1;
    }

    public IEnumerator<char> GetEnumerator() {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator() {
        throw new NotImplementedException();
    }
}

public class GridPosition {
    public int Row = 0;
    public int Column = -1;

    public GridPosition() { }
    public GridPosition(int row, int column) {
        this.Row = row;
        this.Column = column;
    }

    public GridPosition Left(int distance) =>
        new GridPosition(this.Row, this.Column - distance);
    public GridPosition Right(int distance) =>
        new GridPosition(this.Row, this.Column + distance);
    public GridPosition Up(int distance) =>
        new GridPosition(this.Row - distance, this.Column);

    public GridPosition Down(int distance) =>
        new GridPosition(this.Row + distance, this.Column);

}


