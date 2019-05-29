using DataLibrary.DataAccess;
using Enums;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Interfaces;

namespace Logic
{
    public class RuleSetLogic
    {
        private IUserProcessor _userProcessor;
        private IRuleSetProcessor _ruleSetProcessor;
        private IRuleProcessor _ruleProcessor;
        private IAdditionalRuleProcessor _additionalRuleProcessor;

        public RuleSetLogic (IUserProcessor userProcessor, IRuleSetProcessor ruleSetProcessor, IRuleProcessor ruleProcessor, IAdditionalRuleProcessor additionalRuleProcessor)
        {
            _userProcessor = userProcessor;
            _ruleSetProcessor = ruleSetProcessor;
            _ruleProcessor = ruleProcessor;
            _additionalRuleProcessor = additionalRuleProcessor;
        }

        public void CreateRuleSet(List<IRule> rules, string email, List<additionalRule> additionalRules, string name)
        {
            User user = _userProcessor.GetUserByEmail(email);
            int id = user.Id;

            RuleSet ruleSet = new RuleSet
            {
                UserId = id,
                Name = name,
                Rules = rules,
                ExtraRules = additionalRules
            };
            _ruleSetProcessor.AddRuleSet(ruleSet);
        }
        public List<RuleSet> GetRuleSets()
        {
            List<RuleSet> incompleteRuleSets = _ruleSetProcessor.GetRuleSets();
            foreach(RuleSet ruleSet in incompleteRuleSets)
            {
                ruleSet.Rules = _ruleProcessor.GetRulesByRuleSet(ruleSet.Id);
                ruleSet.ExtraRules = _additionalRuleProcessor.GetAdditionalRulesByRuleSet(ruleSet.Id);
            }
            return incompleteRuleSets;
        }
    }
}
