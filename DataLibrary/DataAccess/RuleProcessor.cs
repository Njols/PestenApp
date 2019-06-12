using DataLibrary.Dbo;
using Enums;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataLibrary.DataAccess
{
    public class RuleProcessor : IRuleProcessor
    {
        string _connectionString;
        public RuleProcessor (string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddRule (IRule rule, int ruleSetId)
        {
            string query = "CreateRule";
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(query, conn)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                })
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@CardFace", (int)rule.Card.Face);
                    cmd.Parameters.AddWithValue("@RuleType", (string)rule.RuleType);
                    cmd.Parameters.AddWithValue("@Amount", (int)rule.RuleAmount);
                    cmd.Parameters.AddWithValue("@RuleSetId", (int)ruleSetId);
                    if (rule.Card is SuitedCard)
                    {
                        SuitedCard card = (SuitedCard)rule.Card;
                        cmd.Parameters.AddWithValue("@CardSuit", (int)card.Suit);
                    }
                    cmd.ExecuteNonQuery();
                }
            }

        }
        public List<IRule> GetRulesByRuleSet(int ruleSetId)
        {
            string query = @"SELECT RuleId
                                FROM [Rule_RuleSet]
                                WHERE RuleSetId = @RuleSetId";
            List<int> ruleIds = new List<int>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@RuleSetId", ruleSetId);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    int ruleId = (int)reader["RuleId"];
                    ruleIds.Add(ruleId);
                }
            }
            List<IRule> rules = new List<IRule>();
            foreach (int id in ruleIds)
            {
                rules.Add(GetRuleById(id));
            }
            return rules;
        }
        public IRule GetRuleById(int ruleId)
        {
            string query2 = @"SELECT CardFace,CardSuit, RuleType, RuleAmount
                                FROM [Rule]
                                WHERE RuleId = @RuleId";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query2, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@RuleId", ruleId);
                SqlDataReader reader = cmd.ExecuteReader();
                ICard card;
                if (reader["CardSuit"] != null)
                {
                    card = new SuitedCard
                    {
                        Face = (cardFace)reader["CardFace"],
                        Suit = (cardSuit)reader["CardSuit"]
                    };
                }
                else
                {
                    card = new Card
                    {
                        Face = (cardFace)reader["CardFace"]
                    };
                }
                Rule rule = new Rule
                {
                    Card = card,
                    RuleAmount = (int)reader["RuleAmount"],
                    RuleType = (string)reader["RuleType"]
                };
                return rule;
            }
        }
    }
}
