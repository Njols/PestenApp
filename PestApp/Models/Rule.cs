using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{

    public class Rule
    {
        public Card Card { get; set; }
        public int RuleAmount { get; set; }
        public ruleType RuleType { get; set; }
        public Rule (Card card, ruleType ruletype, int ruleAmount)
        {
            Card = card;
            RuleType = ruletype;
            RuleAmount = ruleAmount;
        }
    }
}
