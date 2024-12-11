using System.Text.RegularExpressions;

namespace Day_07;

internal class Program {
    static void Main(string[] args) {

        var match = Regex.Split(Input.input, @"\n");
        List<Equation> equations = new List<Equation>();
        Int128 total = 0;
        foreach (var equationString in match) {
            Equation eq = new(equationString);
            if (Utils.TryCombo(eq) > 0)
                total += eq.result;
        }
        Console.WriteLine(total);
    }
}

public static class Utils {
    public static int TryCombo(Equation eq) {
        int successCount = 0;
        if (eq.operands.Count == 2) {
            if (Utils.Try(eq, (p,q) => p+q))
                successCount++;
            if (Utils.Try(eq, (p,q) => p*q))
                successCount++;
            if (Utils.Try(eq, (p,q) => Utils.Concat(p,q)))
                successCount++;
        } else {
            successCount += Utils.TryCombo(Utils.Operate(eq, (p,q) => p+q));
            successCount += Utils.TryCombo(Utils.Operate(eq, (p,q) => p*q));
            successCount += Utils.TryCombo(Utils.Operate(eq, (p,q) => Utils.Concat(p,q)));
        }
        return successCount;
    }

    public static Int128 Concat(Int128 int1, Int128 int2) {
        return
            Int128.Parse(String.Concat(int1.ToString(),int2.ToString()));
    }

    public static Equation Operate(Equation eq, Func<Int128, Int128, Int128> value) {
        List<Int128> operands = new(eq.operands);
        Int128 in1 = operands[0];
        Int128 in2 = operands[1];
        operands.RemoveAt(0);
        operands[0] = value(in1,in2);

        Equation output = new(eq.result, operands);
        return output;
    }

    public static bool Try(Equation eq, Func<Int128, Int128, Int128> value) {
        Int128 operandTotal = value(eq.operands[0], eq.operands[1]);
        if (operandTotal == eq.result)
            return true;
        else
            return false;
    }
}


public class Equation {
    public Int128 result = 0;
    public List<Int128> operands = new List<Int128>();

    public Equation(string input) {
        this.Parse(input);
    }

    public Equation(Int128 result, List<Int128> operands) {
        this.result = result;
        this.operands = operands;
    }

    public void Parse(string input) {
        Match match = Regex.Match(input, @"^(\d*):(.*)$");
        result = Int128.Parse(match.Groups[1].Value);
        foreach (string operandString in Regex.Split(match.Groups[2].Value.Trim(), @"\s+")) {
            operands.Add(Int128.Parse(operandString.Trim()));
        } 
    }
}
