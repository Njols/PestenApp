using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{
    public class RuleViewModel
    {
        public cardFace CardFace { get; set; }
        public cardSuit CardSuit { get; set; }
        public ruleType CardRule { get; set; }
        public List<Rule> ExistingRules = new List<Rule>();
    }
}
