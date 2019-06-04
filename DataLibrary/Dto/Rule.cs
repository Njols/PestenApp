using Enums;
using Interfaces;

namespace DataLibrary.Dbo
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
        public Rule ()
        {

        }
    }
}
