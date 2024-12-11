namespace Day_02_redo;

internal class Program {
    static void Main(string[] args) {
        List<List<int>> reports = Util.ParseInput(Input.input);
        int total = 0;
        foreach (var report in reports)    {
            if (Util.IsSafe(report))
                total++;
            else if ( Util.IsSafeAfterScreen(report) ) {
                total++;
            }
        }
        Console.WriteLine(total);
    }
}

public static class Util {
    public static List<List<int>> ParseInput(string input) {
        List<List<int>> reports = new();
        foreach (var line in input.Split('\n')) {
            List<int> list = new List<int>();
            reports.Add(list);
            foreach (var digit in line.Trim().Split(' ').ToList()) {
                list.Add(Convert.ToInt32(digit));
            }
        }
        return reports;
    }

    public static bool IsSafe(List<int> report) {
        bool ascending = true, descending = true;
        for (int i = 0; i < report.Count-1; i++) {
            if (report[i] - report[i + 1] >= 1
                && report[i] - report[i + 1] <= 3) 
                { }
            else  {
                descending = false;
            }    

            if (report[i+1] - report[i] >= 1
                && report[i+1] - report[i] <= 3) 
                { }
            else {
                ascending = false;
            }
        }
        return ascending | descending;
    }

    public static bool IsSafeAfterScreen(List<int> report) {
        bool ascending = true, descending = true;
        for (int i = 0; i < report.Count-1; i++) {
            if (report[i] - report[i + 1] >= 1
                && report[i] - report[i + 1] <= 3) 
                { }
            else  {
                List<int> screenReport1 = new(report);
                List<int> screenReport2 = new(report);
                screenReport1.RemoveAt(i);
                screenReport2.RemoveAt(i+1);
                if (! (Util.IsSafe(screenReport1)
                    || Util.IsSafe(screenReport2)) )
                    descending = false;
            }    

            if (report[i+1] - report[i] >= 1
                && report[i+1] - report[i] <= 3) 
                { }
            else {
                List<int> screenReport1 = new(report);
                List<int> screenReport2 = new(report);
                screenReport1.RemoveAt(i);
                screenReport2.RemoveAt(i+1);
                if (! (Util.IsSafe(screenReport1)
                    || Util.IsSafe(screenReport2)) )
                    ascending = false;
            }
        }
        return ascending | descending;
    }



}
