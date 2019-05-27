using DataLibrary.DataAccess;
using Enums;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    public class RuleSetLogic
    {
        private IUserProcessor _userProcessor;
        private IRuleSetProcessor _ruleSetProcessor;

        public RuleSetLogic (IUserProcessor userProcessor, IRuleSetProcessor ruleSetProcessor)
        {
            _userProcessor = userProcessor;
            _ruleSetProcessor = ruleSetProcessor;
        }

        public void CreateRuleSet(List<Rule> rules, string email, List<additionalRule> additionalRules, string name)
        {
            User user = _userProcessor.GetUserByEmail(email);

            RuleSet ruleSet = new RuleSet(user, rules, additionalRules, name);
            _ruleSetProcessor.AddRuleSet(ruleSet);
        }
        public List<RuleSet> GetRuleSets()
        {
            return _ruleSetProcessor.GetRuleSets();
        }
    }
}
