using firsttry;
using System.Data;
using Rule = firsttry.Rule;

var rules = new List<firsttry.Rule>
{
    new Rule
    {
        Priority = 1,
        Conditions = new List<Condition>
        {
            new Condition { Field = "Amount", Operator = "<=", Value = "5000"}
        },
        Action = "AutoApprove"
    },
     new Rule
    {
        Priority = 2,
        Conditions = new List<Condition>
        {
            new Condition { Field = "Amount", Operator = ">", Value = "10000"},
            new Condition { Field = "Category", Operator = "=", Value = "Travel"}
        },
        Action = "ApprovalChain"
    }
};

 bool evaluate(Condition condition, Expense expense)
{
    if(condition.Field == "Amount")
    {
        var expenseAmount = expense.Amount;
        var ruleValue = decimal.Parse(condition.Value);

        if(condition.Operator == "<=")
        {
            return expenseAmount <= ruleValue;
        }
        if (condition.Operator == ">")
        {
            return expenseAmount > ruleValue;
        }
    }

    if(condition.Field == "Category")
    {
        if (condition.Operator == "=")
        {
            return expense.Category == condition.Value;
        }
    }

    return false;

}

// check expense against each rule
string evaluateRule(List<Rule> rules, Expense expense)
{

    foreach (var rule in rules.OrderBy(r => r.Priority))
    {
        bool matches = true;
        foreach(var condition in rule.Conditions)
        {
            if (!evaluate(condition, expense))
            {
                matches = false;
                break;
            }
        }

        if (matches)
        {
            return rule.Action;
        }
    }

    return "Not specified";

}


var expense1 = new Expense { Amount = 15000, Category = "Travel" };
var expense2 = new Expense { Amount = 2000, Category = "Printing" };
var expense3 = new Expense { Amount = 9000, Category = "Team Dinner" };

var result1 = evaluateRule(rules, expense1);
var result2 = evaluateRule(rules, expense2);
var result3 = evaluateRule(rules, expense3);

Console.WriteLine($"Expense 1: {result1}"); // Should print "ApprovalChain"
Console.WriteLine($"Expense 2: {result2}"); // Should print "AutoApprove"
Console.WriteLine($"Expense 3: {result3}"); // Should print "Not specified" 