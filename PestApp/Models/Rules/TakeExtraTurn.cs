using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models.Rules
{
    public class TakeExtraTurn : RuleTypeWithAmount
    {
        public new string DisplayString { get { return "Current player may take | extra turn(s)"; } }
        public new string BasicDescription { get { return "Take extra turns"; } }
        public override string GetDisplayString()
        {
            if (RuleAmount > 1)
            {
                return string.Format($"Current player may take {RuleAmount} extra turns.");
            }
            else
            {
                return "Current player may take one extra turn";
            }
        }
    }
}
