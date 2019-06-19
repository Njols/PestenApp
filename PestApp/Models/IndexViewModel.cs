﻿using PestApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{
    public class IndexViewModel
    {
        public List<SimpleRuleSetViewModel> AllRuleSets;
        public List<SimpleRuleSetViewModel> RuleSetsByRuleAmount;
    }
}
