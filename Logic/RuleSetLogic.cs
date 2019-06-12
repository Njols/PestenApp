using DataLibrary.DataAccess;
using Enums;
using DataLibrary.Dbo;
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
            int ruleSetId = _ruleSetProcessor.AddRuleSet(ruleSet);
            foreach (IRule rule in rules)
            {
                _ruleProcessor.AddRule(rule, ruleSetId);
            }
            foreach(additionalRule addRule in additionalRules)
            {
                _additionalRuleProcessor.AddAdditionalRule((int)addRule, ruleSetId);
            }


        }
        public List<IRuleSet> GetRuleSets()
        {
            List<IRuleSet> incompleteRuleSets = _ruleSetProcessor.GetRuleSets();
            foreach(RuleSet ruleSet in incompleteRuleSets)
            {
                ruleSet.Rules = _ruleProcessor.GetRulesByRuleSet(ruleSet.Id);
                ruleSet.ExtraRules = _additionalRuleProcessor.GetAdditionalRulesByRuleSet(ruleSet.Id);
            }
            return incompleteRuleSets;
        }

        public IRuleSet GetRuleSetById (int id)
        {
            RuleSet incompleteRuleSet = (RuleSet)_ruleSetProcessor.GetRuleSetById(id);
            RuleSet completeRuleSet = CompleteRuleSet(incompleteRuleSet);
            return completeRuleSet;
        }

        public List<IRuleSet> GetRuleSetsOrderedByRules ()
        {
            List<IRuleSet> incompleteRuleSets = _ruleSetProcessor.GetRuleSetsByAmountOfRules();
            List<IRuleSet> completeRuleSets = new List<IRuleSet>();
            foreach(IRuleSet ruleSet in incompleteRuleSets)
            {
                RuleSet completeRuleSet = CompleteRuleSet(ruleSet);
                completeRuleSets.Add(completeRuleSet);
            }
            return completeRuleSets;
        }

        public RuleSet CompleteRuleSet (IRuleSet incompleteRuleSet)
        {
            RuleSet ruleSet = (RuleSet)incompleteRuleSet;
                List<IRule> rules = _ruleProcessor.GetRulesByRuleSet(ruleSet.Id);
                List<additionalRule> additionalRules = _additionalRuleProcessor.GetAdditionalRulesByRuleSet(ruleSet.Id);
                RuleSet completeRuleSet = new RuleSet
                {
                    Id = ruleSet.Id,
                    UserId = ruleSet.UserId,
                    Rules = rules,
                    ExtraRules = additionalRules
                };
            return completeRuleSet;
        }

    }
}
