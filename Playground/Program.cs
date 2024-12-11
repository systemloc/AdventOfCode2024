using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
namespace Playground;

internal class Program {

    static void Main(string[] args) {
        List<int> p = new();
        p.
        p.Add(1);
        int i = p[2];
    }
}


public class MyDict : Dictionary<char, List<int>> {

   public void Add(char c, int i) {
        if (!this.ContainsKey(c))
            this.Add(c,new List<int>());
        this[c].Add(i);
    }
}
public class MyList : List<int> {
}


