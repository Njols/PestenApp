using DataLibrary.Dbo;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.DataAccess
{
    public interface IRuleSetProcessor
    {
        int AddRuleSet(IRuleSet ruleSet);
        List<IRuleSet> GetRuleSets();
        IRuleSet GetRuleSetById(int id);
        List<IRuleSet> GetRuleSetsByAmountOfRules();
    }
}
