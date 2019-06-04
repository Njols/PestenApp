using DataLibrary.Dbo;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.DataAccess
{
    public interface IRuleProcessor
    {
        void AddRule(IRule rule, int ruleSetId);
        List<IRule> GetRulesByRuleSet(int ruleSetId);
        IRule GetRuleById(int id);
    }
}
