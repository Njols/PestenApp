using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models.Rules
{
    public class SkipNextPlayer : RuleTypeWithAmount
    {
        public new string DisplayString { get { return "Skip the next | player(s)."; } }
        public new string BasicDescription { get { return "Skip next few players"; } }
        public override string GetDisplayString()
        {
            if (RuleAmount > 1)
            {
                return string.Format($"Skip the next {RuleAmount} players.");
            }
            else
            {
                return "Skip the next player";
            }
        }
    }
}
