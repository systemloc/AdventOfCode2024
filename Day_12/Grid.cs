using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MyUtils;
public class Grid : IEnumerable<GridPosition> {
    char[] _grid;
    public int Length = 0;
    public int Width = 0;


    public Grid(int Width, int Length) {
        _grid = new char[Width * Length];
    }

    public Grid(string input) {
        string[] _rows = input.Split('\n', StringSplitOptions.TrimEntries);
        Width = _rows[0].Length;
        Length = _rows.Length;
        _grid = new char[Width * Length];

        for (int row=0;  row<Length; row++) 
            Buffer.BlockCopy(_rows[row].ToCharArray(), 0*sizeof(char), 
                            _grid, row*Width*sizeof(char), 
                            Length * sizeof(char));
    }

    public bool IsValid(GridPosition position) {
        if (position.Row >= 0
            && position.Row < this.Length
            && position.Column >= 0
            && position.Column < this.Width)
            return true;
        else
            return false;
    }

    public char Value(GridPosition position) {
        if (this.IsValid(position))
            return _grid[position.Row * Width + position.Column];
        else
            throw new Exception("Invalid grid position");
    }

    public char Value(GridPosition position, char input) {
        if (this.IsValid(position))
            _grid[position.Row * Width + position.Column] = input;
        else
            throw new Exception("Invalid grid position");
        return _grid[position.Row * Width + position.Column];
    }

    //public void FindPerson() {
    //    this.ResetPosition();
    //    this.MoveNext();
    //    do {
    //        if ( '>' == this.Value(Position)
    //            || '<' == this.Value(Position)
    //            || '^' == this.Value(Position)
    //            || 'v' == this.Value(Position) ) {
    //            break;
    //        }
    //        this.MoveNext();
    //    } while (this.IsValid(this.Position));
    //}

    //public void MovePerson() {
    //    if (!this.IsValid(Position))
    //        return;
    //    char person = this.Value(Position);
    //    switch (person) {
    //        case '>':
    //            MoveCase('>', 'v', p => p.Right(1));
    //            break;
    //        case '<':
    //            MoveCase('<', '^', p => p.Left(1));
    //            break;
    //        case 'v':
    //            MoveCase('v', '<', p => p.Down(1));
    //            break;
    //        case '^':
    //            MoveCase('^', '>', p => p.Up(1));
    //            break;
    //        default:
    //            throw new Exception("MovePerson fatal error.");
    //        }


    //    void MoveCase(char person, char rotation, Func<GridPosition, GridPosition> posMove) {
    //        if (!this.IsValid(posMove(Position))) {
    //            this.Value(Position, 'X');
    //            Position = posMove(Position);
    //        } else if (this.Value(posMove(Position)) == '#') {
    //            this.Value(Position, rotation);
    //            this.MovePerson();
    //        } else {
    //            this.Value(Position, 'X');
    //            this.Value(posMove(Position), person);
    //            Position = posMove(Position);
    //        }
    //    }
    //}

    public void PrintGrid() {
        Span<char> grid = _grid;
        for (int row = 0; row < Length; row++) {
            Console.WriteLine(
                grid.Slice(row * Width, Length).ToString());
        }
    }

    //public int SearchPosition(string target) {
    //    return SearchPosition(this.Position, target);
    //}

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
   
    public IEnumerator<GridPosition> GetEnumerator() {
        return new GridEnum(Length, Width);
    }

    IEnumerator IEnumerable.GetEnumerator() {
        return (IEnumerator) GetEnumerator();
    }
}

public struct GridPosition {
    public int Column = -1;
    public int Row = 0;

    public GridPosition() { }
    public GridPosition(int column, int row) {
        this.Column = column;
        this.Row = row;
    }

    public GridPosition Left(int distance) =>
        new GridPosition(this.Column - distance, this.Row);
    public GridPosition Right(int distance) =>
        new GridPosition(this.Column + distance, this.Row) ;
    public GridPosition Up(int distance) =>
        new GridPosition(this.Column, this.Row - distance);
    public GridPosition Down(int distance) =>
        new GridPosition(this.Column, this.Row + distance);

    public string ToString() {
        return $"({Column}, {Row})";
    }
}

public class GridEnum : IEnumerator<GridPosition> {
    int length, width;
    GridPosition position = new();

    public GridEnum(int Length, int Width) { 
        length = Length;
        width = Width;
        position.Column = -1;
        position.Row = 0;
    }

    GridPosition IEnumerator<GridPosition>.Current => position;

    object IEnumerator.Current => position;

    public bool MoveNext() {
        if (position.Right(1).Column < width)
            position = position.Right(1);
        else {
            position = position.Down(1);
            position.Column = 0;
        }
        return position.Row < length;
    }

    void IEnumerator.Reset() {
        this.position.Row = 0;
        this.position.Column = -1;
    }

    void IDisposable.Dispose() {
    }
}
