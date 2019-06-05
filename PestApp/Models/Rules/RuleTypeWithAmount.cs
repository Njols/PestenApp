using Enums;
using Interfaces;
using PestApp.Models.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{
    public class RuleTypeWithAmount : Rules.RuleType
    {
        public int RuleAmount { get; set; }
    }
}
