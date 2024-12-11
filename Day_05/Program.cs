using System.Diagnostics;
using System.Linq.Expressions;

namespace Day_05;

internal class Program {
    static void Main(string[] args) {
        RuleSet rules = new();
        rules.ParseRules(Input.Rules);

        Pages pages = new();
        pages.ParsePages(Input.Pages);

        Pages incorrectPages = new();

        int total = 0;
        foreach ( PageSet pageSet in pages.pages ) {
            if (rules.IsValid(pageSet)) {
                total += pageSet.MiddlePageValue();
            } else
                incorrectPages.pages.Add(pageSet);
        }

        Console.WriteLine("Day 5 Part 1: " + total);
        Console.WriteLine();

        total = 0;
        foreach ( PageSet pageSet in incorrectPages.pages) {
            Console.WriteLine(String.Join(", ", pageSet.pages));
            pageSet.OrderByRuleset(rules);
            Console.WriteLine(String.Join(", ", pageSet.pages));
            if (!rules.IsValid(pageSet))
                Console.WriteLine("Error!");
            total += pageSet.MiddlePageValue();
        }
        Console.WriteLine("Day 5 Part 2: " + total);
    }
}

public class RuleSet {
    public List<(int, int)> ruleList = new();
    public void ParseRules(string rules) {
        Array.ForEach(rules.Split('\n'),ParseRule);
    }

    public void ParseRule(string rule) {
        var ruleSubstrings = rule.Split('|');
        if (ruleSubstrings.Length != 2)
            throw new Exception("ParseRule failed.");
        ruleList.Add(
            (Convert.ToInt32(ruleSubstrings[0].Trim()),
             Convert.ToInt32(ruleSubstrings[1].Trim()))
        );
    }

    public bool IsValid(PageSet pageSet) {
        bool result = true;
        foreach (var rule in ruleList) 
            if (pageSet.PassesRule(rule) == false)
                result = false;
        return result;
    }

    public List<int> Starters() {
        HashSet<int> starter = new();
        foreach (var rule in ruleList)
            starter.Add(rule.Item1);
        foreach (var rule in ruleList)
            starter.Remove(rule.Item2);
        return starter.ToList();
    }

    public RuleSet RuleSetContainingPages(PageSet pageSet) {
        RuleSet result = new();
        foreach (var rule in ruleList)
            if (pageSet.Contains(rule))
                result.ruleList.Add(rule);
        return result;
    }
}

public class Pages { 
    public List<PageSet> pages = new();

    public void ParsePages(string pages) {
        Array.ForEach(pages.Split('\n'), ParsePageSet);
    }

    public void ParsePageSet(string pageSetString) {
        PageSet pageSet = new();
        pages.Add(pageSet);
        var pageSubstrings = pageSetString.Trim().Split(',');
        if (pageSubstrings.Length < 1)
            throw new Exception("ParsePageSet failed");
        foreach (var pageSubstring in pageSubstrings) {
            pageSet.pages.Add(Convert.ToInt32(pageSubstring));
        }
    }
}

public class PageSet {
    public List<int> pages = new();

    public bool PassesRule((int first, int second) rule) {
        int idxFirst = pages.IndexOf(rule.first);
        if (idxFirst == -1 )
            return true;
        int idxSecond = pages.IndexOf(rule.second);
        if ( idxSecond == -1)
            return true;
        if (idxFirst < idxSecond)
            return true;
        else
            return false;
    }

    public int MiddlePageValue() {
        if (pages.Count % 2 == 0)
            throw new Exception("MiddlePageValue recieved even numbered set");
        else
            return pages[(pages.Count + 1) / 2 - 1];

    }

    public void OrderByRuleset(RuleSet rules) {
        RuleSet rulesForThisPageSet = rules.RuleSetContainingPages(this);
        var starters = rulesForThisPageSet.Starters();
        if (starters.Count == 0)
            return;
        foreach (var page in starters) {
            PageSet orderedPages = new ();
            PageSet leftoverPages = new();
            leftoverPages.pages = new(pages);
            orderedPages.pages.Add(page);
            leftoverPages.pages.Remove(page);
            try {
                leftoverPages.OrderByRuleset(rulesForThisPageSet);
            } catch (Exception e) { continue; }
            orderedPages.pages.AddRange(leftoverPages.pages);
            if (rules.IsValid(orderedPages)) {
                this.pages = orderedPages.pages;
                return;
            }
        }
        throw new Exception("OrderByRuleSet failed.");
    }

    public bool Contains((int, int) rule) {
        if (this.pages.Contains(rule.Item1)
            && this.pages.Contains(rule.Item2))
            return true;
        else
            return false;
    }
}

