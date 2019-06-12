using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models.Rules
{
    public class RuleType
    {
        public virtual string DisplayString { get; set; }
        public virtual string BasicDescription { get; set; }
    }
}
