using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Enums;
using Enums;
using Interfaces;

namespace DataLibrary.Models
{

    public class RuleSet : IRuleSet
    {
        public int Id { get; }
        public List<additionalRule> ExtraRules { get; set; }
        public User User { get; set; }
        public List<IRule> Rules { get; set; }
        public string Name { get; set; }
        public RuleSet (User user, List<Rule> rules)
        {
            User = user;
            Rules = rules;
        }
        public RuleSet(User user, List<Rule> rules, List<additionalRule> additionalRules, string name)
        {
            ExtraRules = additionalRules;
            User = user;
            Rules = rules;
            Name = name;
        }
    }
}
