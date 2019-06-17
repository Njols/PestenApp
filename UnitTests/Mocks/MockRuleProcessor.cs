using DataLibrary.DataAccess;
using DataLibrary.Dbo;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests.Mocks
{
    public class MockRuleProcessor : IRuleProcessor
    {
        List<RuleWithRuleSetId> rules = new List<RuleWithRuleSetId>();
        public void AddRule(IRule rule, int ruleSetId)
        {
            rules.Add(new RuleWithRuleSetId { Rule = rule, RuleSetId = ruleSetId });
        }

        public IRule GetRuleById(int id)
        {
            return rules.Where(_=>_.Rule.Id == id).FirstOrDefault().Rule;
        }

        public List<IRule> GetRulesByRuleSet(int ruleSetId)
        {
            return rules.Where(_=>_.RuleSetId == ruleSetId).Select(_=>_.Rule).ToList();
        }
    }
}
