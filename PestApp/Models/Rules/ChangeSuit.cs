using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models.Rules
{
    public class ChangeSuit : RuleTypeWithoutAmount
    {
        public override string DisplayString { get { return "Current player gets to decide the suit."; } }
        public override string BasicDescription { get { return "Player decides the suit"; } }
    }
}
