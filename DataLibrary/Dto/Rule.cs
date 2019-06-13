using Enums;
using Interfaces;

namespace DataLibrary.Dbo
{
    public class Rule : IRule
    {
        public ICard Card { get; set; }
        public int RuleAmount { get; set; }
        public string RuleTypeString { get; set; }
        public Rule (ICard card, string ruletype, int ruleAmount)
        {
            Card = card;
            RuleTypeString = ruletype;
            RuleAmount = ruleAmount;
        }
        public Rule ()
        {

        }
    }
}
