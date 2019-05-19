using PestApp.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{

    public class RuleSet
    {
        public List<additionalRule> ExtraRules { get; set; }
        public List<Rule> Rules { get; set; }
        public string Name { get; set; }
        public RuleSet ( List<Rule> rules)
        {
            Rules = rules;
        }
        public RuleSet ( List<Rule> rules, List<additionalRule> additionalRules)
        {
            ExtraRules = additionalRules;
            Rules = rules;
        }
    }
}
