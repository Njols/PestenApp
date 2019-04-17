using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{
    public enum ruleType
    {
        take, revertTime, skipPlayer, extraTurns, switchSeats, changeSuits, stackAfter
    }
    public class Rule
    {
        public Card Card { get; set; }
        public int RuleAmount { get; set; }
        public ruleType RuleType { get; set; }
        public bool Select { get; set; }
        public Rule (Card card, ruleType ruletype, int ruleAmount)
        {
            Card = card;
            RuleType = ruletype;
            RuleAmount = ruleAmount;
        }
    }
}
