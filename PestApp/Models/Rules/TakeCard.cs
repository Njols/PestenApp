using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models.Rules
{
    public class TakeCard : RuleTypeWithAmount
    {
        public override string DisplayString { get { return "Next player takes | cards."; } }
        public override string BasicDescription { get { return "Next player takes cards"; } }
        public override string GetDisplayString()
        {
            if (RuleAmount > 1)
            {
                return string.Format($"Next player takes {RuleAmount} cards.");
            }
            else
            {
                return "Next player takes one extra card.";
            }
        }
    }
}
