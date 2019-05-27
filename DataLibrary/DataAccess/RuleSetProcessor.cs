using System;
using System.Collections.Generic;
using System.Text;
using Enums;
using DataLibrary.Models;

namespace DataLibrary.DataAccess
{
    public  class RuleSetProcessor : IRuleSetProcessor
    {
        SqlDataAccess _sqlDataAccess;
        public void AddRuleSet (RuleSet ruleSet)
        {

            string sql = @"INSERT INTO [RuleSet] (UserId, Name)
                             VALUES (@UserId, @Name)
                             SELECT SCOPE_IDENTITY()";

            int ruleSetId = _sqlDataAccess.SaveData(sql, ruleSet);


            foreach(Rule rule in ruleSet.Rules)
            {
                string Card = rule.Card.ToString();
                string RuleType = rule.RuleType.ToString();
                string Amount = rule.RuleAmount.ToString();

                string query = @"INSERT INTO [Rule] (Card,RuleType,Amount)
                                 VALUES (@Card, @RuleType, @Amount)
                                 SELECT SCOPE_IDENTITY()";
                int ruleId = _sqlDataAccess.SaveData(query, rule);

                string query2 = @"INSERT INTO [Rule_RuleSet] (RuleSetId, RuleId)
                                  VALUES(@ruleSetId, @ruleId)";
                _sqlDataAccess.SaveData(query2, rule);
            }

            foreach (additionalRule rule in ruleSet.ExtraRules)
            {
                int AdditionalRuleId = (int)rule;

                string query = @"INSERT INTO [AdditionalRule_RuleSet] (RuleSetId, AdditionalRuleId)
                                 VALUES (@ruleSetId, @AdditonalRuleId)";
                _sqlDataAccess.SaveData(query, rule);
            }

        }

        public List<RuleSet> GetRuleSets()
        {
            string sql = @"SELECT * FROM [RuleSet]";
            return _sqlDataAccess.LoadList<RuleSet>(sql);
        }

    }
}
