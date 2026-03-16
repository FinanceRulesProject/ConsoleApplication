using firsttry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace consoleapp
{
    public class RuleEvaluator
    {

        public bool evaluate(Condition condition, Expense expense)
        {
            Console.WriteLine(typeof(Expense));
            var property = typeof(Expense).GetProperty(condition.Field); // Find the property in Expense whose name equals condition.Field
            Console.WriteLine(property); // PropertyInfo - Expense.Amount

            if (property == null)
            {
                return false;
            }
            else
            {
                var expenseValue = property.GetValue(expense);
                var ruleValue = Convert.ChangeType(condition.Value, property.PropertyType);


                switch (condition.Operator)
                {
                    case ">":
                        return (decimal)expenseValue > (decimal)ruleValue;

                    case "<=":
                        return (decimal)expenseValue <= (decimal)ruleValue;

                    case "=":
                        return expenseValue.Equals(ruleValue);

                    default:
                        return false;
                }
            }
        }

        public string evaluateRule(List<Rule> rules, Expense expense)
        {

            foreach (var rule in rules.OrderBy(r => r.Priority))
            {
                bool matches = true;
                foreach (var condition in rule.Conditions)
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


    }
}
