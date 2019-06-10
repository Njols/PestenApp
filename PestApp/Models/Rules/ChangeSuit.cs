using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models.Rules
{
    public class ChangeSuit : RuleTypeWithoutAmount
    {
        public new string DisplayString { get { return "Current player gets to decide the suit."; } }
        public new string BasicDescription { get { return "Player decides the suit"; } }
    }
}
