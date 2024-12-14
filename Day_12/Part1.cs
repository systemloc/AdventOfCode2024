using MyUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day_12;
internal class Part1 {
    public Grid grid;
    public char currentChar;
    List<Area> areas = new();
    HashSet<GridPosition> visitedPositions = new();

    public Part1(Grid grid, char currentChar) { 
        this.grid = grid;
        this.currentChar = currentChar;
    }

    public void Run() {

        foreach (GridPosition currentPosition in grid) {
            if (visitedPositions.Contains(currentPosition))
                continue;
            areas.Add(ProcessPosition(currentPosition));
        }
    }

    Area ProcessPosition(GridPosition p) {
        Queue<GridPosition> queue = new Queue<GridPosition>();
        var area = new Area();
        area.Character = grid.Value(p);
        queue.Enqueue(p);
        while (queue.Count > 0) {
            p = queue.Dequeue();
            if (area.Positions.Contains(p))
                continue;
            area.Positions.Add(p);
            visitedPositions.Add(p);
            area.area++;
            if (Contains(p.Right(1), area.Character))
                EnqueueIfNotVisited(p.Right(1), area.Character);
            else
                area.perimeter++ ;
            if (Contains(p.Down(1), area.Character))
                EnqueueIfNotVisited(p.Down(1), area.Character);
            else
                area.perimeter++;
            if (Contains(p.Left(1), area.Character))
                EnqueueIfNotVisited(p.Left(1), area.Character);
            else
                area.perimeter++;
            if (Contains(p.Up(1), area.Character))
                EnqueueIfNotVisited(p.Up(1), area.Character);
            else
                area.perimeter++;
        }
        return area;

        bool Contains(GridPosition next, Char c) {
            if (grid.IsValid(next)
                && grid.Value(next) == c)
                return true;
            else return false;
        }

        void EnqueueIfNotVisited(GridPosition next, Char c) {
            if (!area.Positions.Contains(next))
                queue.Enqueue(next);
        }
    }


    public int Total() {
        int total = 0;
        foreach (var area in areas) {
            total += area.perimeter * area.area;
        }
        return total;
    }
    public struct Area {
        public char Character;
        public HashSet<GridPosition> Positions;
        public int perimeter = 0;
        public int area = 0;

        public Area() {
            Positions = new();
        }
    }

}
