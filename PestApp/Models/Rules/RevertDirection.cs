using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models.Rules
{
    public class RevertDirection : RuleTypeWithoutAmount
    {
        public override string DisplayString { get { return "Revert the direction the turns take."; } }
        public override string BasicDescription { get { return "Change directions"; } }
    }
}
