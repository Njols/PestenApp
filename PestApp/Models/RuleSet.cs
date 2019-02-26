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
        private int id;
        public int Id { get { return id; } }
        private List<extraRules> extraRules;
        public List<extraRules> ExtraRules {  get { return extraRules; } }
        private User _user;
        public User User { get { return _user; } }
        private List<Rule> _rules;
        public List<Rule> Rules { get { return _rules; } }
        public RuleSet (User user, List<Rule> rules)
        {
            _user = user;
            _rules = rules;
        }
    }
}
