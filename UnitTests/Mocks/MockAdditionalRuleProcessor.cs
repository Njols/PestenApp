using DataLibrary.DataAccess;
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests.Mocks
{
    public class MockAdditionalRuleProcessor : IAdditionalRuleProcessor
    {
        List<AdditionalRuleWithRuleSetId> additionalRules = new List<AdditionalRuleWithRuleSetId>();
        public void AddAdditionalRule(int additionalRuleId, int ruleSetId)
        {
            additionalRules.Add(new AdditionalRuleWithRuleSetId {AdditionalRule = additionalRuleId, RuleSetId = ruleSetId });
        }

        public List<additionalRule> GetAdditionalRulesByRuleSet(int ruleSetId)
        {
            return additionalRules.Where(_ => _.RuleSetId == ruleSetId).Select(o => (additionalRule)o.AdditionalRule).ToList();
        }
    }
}
