using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{
    public enum extraRules
    {
        takeAndPut, takeStacks, noPlayAfterTaking, endOnPestCard, stackAfterTake
    }
    public class RuleSet
    {
        public int Id { get; }
        public List<extraRules> ExtraRules { get; set; }
        public User User { get; set; }
        public List<Rule> Rules { get; set; }
        public RuleSet (User user, List<Rule> rules)
        {
            User = user;
            Rules = rules;
        }
    }
}
