using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{
    public class Rule : IRule
    {
        public ICard Card { get; set; }
        public string RuleTypeString { get; set; }
        public int RuleAmount { get; set; }
    }
}
