using Enums;
using Interfaces;

namespace DataLibrary.Dbo
{
    public class Rule : IRule
    {
        public ICard Card { get; set; }
        public int RuleAmount { get; set; }
        public string RuleType { get; set; }
        public Rule (ICard card, string ruletype, int ruleAmount)
        {
            Card = card;
            RuleType = ruletype;
            RuleAmount = ruleAmount;
        }
        public Rule ()
        {

        }
    }
}
