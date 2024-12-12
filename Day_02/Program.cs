namespace Day_02;

internal class Program {
    static void Main(string[] args) {
        List<List<int>> reports = Utils.ParseInput(Input.input);
        int safeCount = 0;

        foreach (var report in reports) 
            if (Utils.ReportIsSafe(report)) 
                safeCount++;

        Console.WriteLine("Day 2, Part 1: " + safeCount);
    }
}

public static class Utils {
    public static List<List<int>> ParseInput(string input) {
        List<List<int>> reports = new List<List<int>>();
        if (string.IsNullOrEmpty(input))
            throw new Exception("ParseInput recieved empty or null input");
        StringReader reader = new StringReader(input);

        for (string? line = reader.ReadLine();  
             line != null; 
             line = reader.ReadLine()) 
            reports.Add(ParseLine(line));
 
        return reports;
    }

    public static List<int> ParseLine(string line) {
        List<int> report = new();
        if (string.IsNullOrEmpty(line))
            throw new Exception("ParseLine recieved null or empty input");
        foreach (string number in line.Split(' '))
            report.Add(Convert.ToInt32(number));
        if (report.Count == 0)
            throw new Exception("ParseLine failed on line: " + line);
        return report;
    }

    public static bool ReportIsSafe(List<int> report) {
        bool isSafe = true;
        if (report[0] > report[^1]) {
            for (int i = 0; i < report.Count - 1; i++) {
                if (Utils.IsSafeAscChange(report[i+1], report[i])) {
                } else { isSafe = false; break; }
            }
        } else if (report[0] < report[^1]) {
            for (int i = 0; i < report.Count - 1; i++) {
                if (Utils.IsSafeAscChange(report[i], report[i+1])) {
                } else { isSafe = false; break; }
            }
        } else
            isSafe = false;
        return isSafe;
    }

    public static int FindBreakingChange(List<int> report) {
        if (report[0] > report[^1]) {
            for (int i = 0; i < report.Count - 1; i++) {
                if (Utils.IsSafeAscChange(report[i + 1], report[i])) {
                } else { return i+1; }
            }
        } else if (report[0] < report[^1]) {
            for (int i = 0; i < report.Count - 1; i++) {
                if (Utils.IsSafeAscChange(report[i], report[i + 1])) {
                } else { return i+1; }
            }
        } else
            return 0;
        return -1;
    }

    public static bool IsSafeAscChange(int first, int second) {
        if (0 < second - first &&
            second - first < 4)
            return true;
        else 
            return false;
    }
 

}
