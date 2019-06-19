using Enums;
using Interfaces;
using PestApp.Models.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{

    public abstract class RuleTypeWithoutAmount : Rules.RuleType
    {
        public override string GetDisplayString()
        {
            return DisplayString;
        }
    }
}
