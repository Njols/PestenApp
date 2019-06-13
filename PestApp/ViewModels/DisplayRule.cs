using PestApp.Models;
using PestApp.Models.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.ViewModels
{
    public class DisplayRule : Rule
    {
        public RuleType RuleType { get; set; }
        public DisplayRule (string ruleType)
        {
            Type t = Type.GetType(ruleType);
            RuleType = (RuleType)Activator.CreateInstance(t);
        }
    }
}
