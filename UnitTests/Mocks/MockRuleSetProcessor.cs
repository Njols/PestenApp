using DataLibrary.DataAccess;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests.Mocks
{
    public class MockRuleSetProcessor : IRuleSetProcessor
    {
        List<IRuleSet> ruleSets = new List<IRuleSet>();
        public int AddRuleSet(IRuleSet ruleSet)
        {
            ruleSets.Add(ruleSet);
            return (ruleSets.Count-1);
        }

        public IRuleSet GetRuleSetById(int id)
        {
            return ruleSets[id];
        }

        public List<IRuleSet> GetRuleSets()
        {
            return ruleSets;
        }

        public List<IRuleSet> GetRuleSetsByAmountOfRules()
        {
            throw new NotImplementedException();
        }

        public List<IRuleSet> GetRuleSetsByUser()
        {
            throw new NotImplementedException();
        }
    }
}
