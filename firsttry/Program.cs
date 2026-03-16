using consoleapp;
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
    },
     new Rule
     {
         Priority = 3,
         Conditions = new List<Condition>
         {
             new Condition { Field = "Department", Operator = "=", Value = "Software"},
             new Condition { Field = "Amount", Operator = ">", Value = "20000"},
         },
         Action = "Manager"
     }
};

var evaluator = new RuleEvaluator();
    



// check expense against each rule

var expense1 = new Expense { Amount = 15000, Category = "Travel", Department = "HR" };
var expense2 = new Expense { Amount = 2000, Category = "Printing" , Department = "HR" };
var expense3 = new Expense { Amount = 25000, Department = "Software", Category = "Petty Cash" };
var expense4 = new Expense { Amount = 30000, Department = "Software", Category = "Travel" };

var result1 = evaluator.evaluateRule(rules, expense1);
var result2 = evaluator.evaluateRule(rules, expense2);
var result3 = evaluator.evaluateRule(rules, expense3);
var result4 = evaluator.evaluateRule(rules, expense4);

Console.WriteLine($"Expense 1: {result1}"); // Should print "ApprovalChain"
Console.WriteLine($"Expense 2: {result2}"); // Should print "AutoApprove"
Console.WriteLine($"Expense 3: {result3}"); // Should print "Manager" 
Console.WriteLine($"Expense 4: {result4}"); // Should print "ApprovalChain" 