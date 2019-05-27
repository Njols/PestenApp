using Enums;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{

    public class Rule : IRule
    {
        public ICard Card { get; set; }
        public int RuleAmount { get; set; }
        public ruleType Type { get; set; }
        public Rule (ICard card, ruleType ruletype, int ruleAmount)
        {
            Card = card;
            Type = ruletype;
            RuleAmount = ruleAmount;
        }
    }
}
