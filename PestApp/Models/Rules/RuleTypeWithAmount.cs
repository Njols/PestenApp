﻿using Enums;
using Interfaces;
using PestApp.Models.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{
    public abstract class RuleTypeWithAmount : Rules.RuleType
    {
        public int RuleAmount { get; set; }
        public abstract string GetDisplayString();
    }
}
