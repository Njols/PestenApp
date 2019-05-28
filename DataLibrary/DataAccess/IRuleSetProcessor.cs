﻿using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.DataAccess
{
    public interface IRuleSetProcessor
    {
        int AddRuleSet(RuleSet ruleSet, int userId);
        List<RuleSet> GetRuleSets();
    }
}