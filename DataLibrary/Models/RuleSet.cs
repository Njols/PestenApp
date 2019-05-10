using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLibrary.Enums;

namespace DataLibrary.Models
{

    public class RuleSet
    {
        public int Id { get; }
        public List<additionalRule> ExtraRules { get; set; }
        public User User { get; set; }
        public List<Rule> Rules { get; set; }
        public RuleSet (User user, List<Rule> rules)
        {
            User = user;
            Rules = rules;
        }
        public RuleSet(User user, List<Rule> rules, List<additionalRule> additionalRules)
        {
            ExtraRules = additionalRules;
            User = user;
            Rules = rules;
        }
    }
}
