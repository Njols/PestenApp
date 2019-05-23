using DataLibrary.DataAccess;
using DataLibrary.Enums;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Logic
{
    class RuleSetLogic
    {
        public void CreateRuleSet(List<Rule> rules, string email, List<additionalRule> additionalRules, string name)
        {
            UserProcessor _userProcessor = new UserProcessor();
            User user = _userProcessor.GetUserByEmail(email);
            RuleSetProcessor _ruleSetProcessor = new RuleSetProcessor();
            _ruleSetProcessor.CreateRuleSet(rules, user, additionalRules, name);
        }
    }
}
