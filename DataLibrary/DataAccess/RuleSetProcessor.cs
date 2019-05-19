using System;
using System.Collections.Generic;
using System.Text;
using DataLibrary.Enums;
using DataLibrary.Models;

namespace DataLibrary.DataAccess
{
    public  class RuleSetProcessor
    {
        public void CreateRuleSet (List<Rule> rules, User user, List<additionalRule>additionalRules, string name)
        {
            RuleSet ruleSet = new RuleSet(user, rules, additionalRules);

            int UserId = user.Id;
            string Name = name;

            string sql = @"INSERT INTO [RuleSet] (UserId, Name)
                             VALUES (@UserId, @Name)
                             SELECT SCOPE_IDENTITY()";

            int ruleSetId = SqlDataAccess.SaveData(sql, ruleSet);


            foreach(Rule rule in rules)
            {
                string Card = rule.Card.ToString();
                string RuleType = rule.RuleType.ToString();
                string Amount = rule.RuleAmount.ToString();

                string query = @"INSERT INTO [Rule] (Card,RuleType,Amount)
                                 VALUES (@Card, @RuleType, @Amount)
                                 SELECT SCOPE_IDENTITY()";
                int ruleId = SqlDataAccess.SaveData(query, rule);

                string query2 = @"INSERT INTO [Rule_RuleSet] (RuleSetId, RuleId)
                                  VALUES(@ruleSetId, @ruleId)";
                SqlDataAccess.SaveData(query2, rule);
            }

            foreach (additionalRule rule in additionalRules)
            {
                int AdditionalRuleId = (int)rule;

                string query = @"INSERT INTO [AdditionalRule_RuleSet] (RuleSetId, AdditionalRuleId)
                                 VALUES (@ruleSetId, @AdditonalRuleId";
            }

            //still wip
        }

    }
}
