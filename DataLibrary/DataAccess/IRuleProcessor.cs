using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.DataAccess
{
    public interface IRuleProcessor
    {
        void AddRule(Rule rule, int ruleSetId);
        List<Rule> GetRulesByRuleSet(int ruleSetId);
    }
}
