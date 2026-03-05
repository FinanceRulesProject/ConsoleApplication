using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firsttry
{
    public class Rule
    {
        public int Priority { get; set; }
        public List<Condition> Conditions { get; set; }

        public string Action { get; set; }
    }
}
