namespace Day_10;

public class Program {
    static void Main(string[] args) {
        var input = ParseInput(Input.RealInput);

        PathListMap pathsMap = new();
        for(int x = 0; x < input[0].Count; x++) 
            for(int y = 0; y < input.Count; y++) 
                if (input[x][y] == 0)
                    pathsMap.Add((x, y),FindPaths(input, x, y));


        int total = 0;
        foreach (List<Path> paths in pathsMap.Values) {
            HashSet<(int x, int y)> uniquePathEnds = new();
            foreach (Path path in paths) {
                uniquePathEnds.Add((path[^1].x, path[^1].y));
            }
            total += uniquePathEnds.Count;
        }

        Console.WriteLine("Day 10 Part 1: " + total);

        total = 0;
        foreach (List<Path> paths in pathsMap.Values) {
            total += paths.Count;
        }


        Console.WriteLine("Day 10 Part 2: " + total);
    }

    public class PathListMap : Dictionary<(int x, int y), List<Path>> { 
        public void Add((int x, int y) position, Path path) {
            if(!this.ContainsKey(position))
                this.Add(position, new List<Path>());
            this[position].Add(path);
        }
    }

    public class Path : List<(int x, int y)> { }

    public static List<Path> FindPaths(List<List<int>> input, int x, int y) {
        List<Path> results = new();
        int xMax = input.Count-1;
        int yMax = input[0].Count-1;
        int currentValue = input[x][y];


        if (currentValue == 9)
            results.Add(new Path() { (x, y) });
        else {
            if (x < xMax)
                foreach (Path candidatePath in CheckPath(x+1, y)) {
                        candidatePath.Insert(0,(x,y));
                        results.Add(candidatePath);
                }
            if (y < yMax)
                foreach (Path candidatePath in CheckPath(x, y+1)) {
                    candidatePath.Insert(0,(x,y));
                    results.Add(candidatePath);
                }
            if (x > 0)
               foreach (Path candidatePath in CheckPath(x-1, y)) {
                    candidatePath.Insert(0,(x,y));
                    results.Add(candidatePath);
               }
           if (y > 0)
               foreach (Path candidatePath in CheckPath(x, y-1)) {
                    candidatePath.Insert(0,(x,y));
                    results.Add(candidatePath);
               }
      }
        return results;

        List<Path> CheckPath(int x, int y) { 
            int nextValue = input[x][y];
            if (currentValue + 1 == nextValue)
                return FindPaths(input, x, y);
            else return new List<Path>();
        }
    }

    public static List<List<int>> ParseInput(string input) {
        List<List<int>> parsedGraph = new();

        var lines = input.AsSpan().Split("\r\n");
        foreach (var line in lines) {
            parsedGraph.Add(ParseLine(input[line]));
        }
        return parsedGraph;
    }

    public static List<int> ParseLine(string input) {
        List<int> parsedLine = new();
        foreach (char c in input.ToCharArray()) {
            parsedLine.Add(Convert.ToInt32(c-48));
        }
        return parsedLine;
    }
}
