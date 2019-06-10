using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models.Rules
{
    public class RevertDirection : RuleTypeWithoutAmount
    {
        public new string DisplayString { get { return "Revert the direction the turns take."; } }
        public new string BasicDescription { get { return "Change directions"; } }
    }
}
