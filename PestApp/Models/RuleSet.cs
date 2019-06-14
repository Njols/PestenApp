using Enums;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{

    public class RuleSet : IRuleSet
    {
        public List<additionalRule> ExtraRules { get; set; }
        public List<IRule> Rules { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public int Id { get; set; }
        public RuleSet ( List<IRule> rules)
        {
            Rules = rules;
        }
        public RuleSet ( List<IRule> rules, List<additionalRule> additionalRules)
        {
            ExtraRules = additionalRules;
            Rules = rules;
        }
    }
}
