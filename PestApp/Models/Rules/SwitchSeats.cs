using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models.Rules
{
    public class SwitchSeats : RuleTypeWithAmount
    {
        public override string DisplayString { get { return "Switch seats clockwise | time(s)."; } }
        public override string BasicDescription { get { return "Switch seats"; } }
        public override string GetDisplayString()
        {
            if (RuleAmount > 0)
            {
                return string.Format($"Switch seats clockwise {RuleAmount} times.");
            }
            else
            {
                return "Switch seats once";
            }
        }
    }
}
