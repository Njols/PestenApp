using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enums;
using Interfaces;

namespace DataLibrary.Models
{

    public class RuleSet : IRuleSet
    {
        public int Id { get; set; }
        public List<additionalRule> ExtraRules { get; set; }
        public int UserId { get; set; }
        public List<IRule> Rules { get; set; }
        public string Name { get; set; }
        public RuleSet (List<IRule> rules)
        {
            Rules = rules;
        }
        public RuleSet(List<IRule> rules, List<additionalRule> additionalRules, string name)
        {
            ExtraRules = additionalRules;
            Rules = rules;
            Name = name;
        }
        public RuleSet()
        {

        }
    }
}
