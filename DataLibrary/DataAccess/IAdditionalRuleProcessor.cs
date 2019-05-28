using Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.DataAccess
{
    public interface IAdditionalRuleProcessor
    {
        void AddAdditionalRule(int additionalRuleId, int ruleSetId);
        List<additionalRule> GetAdditionalRulesByRuleSet(int ruleSetId);
    }
}
