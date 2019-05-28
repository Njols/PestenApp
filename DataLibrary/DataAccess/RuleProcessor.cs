using DataLibrary.Models;
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
        public void AddRule (Rule rule, int ruleSetId)
        {
            string query = @"INSERT INTO [Rule] (Card,RuleType,Amount)
                                 VALUES (@Card, @RuleType, @Amount)";
            int id;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@Card", rule.Card.GetCard());
                cmd.Parameters.AddWithValue("@RuleType", (int)rule.Type);
                cmd.Parameters.AddWithValue("@Amount", rule.RuleAmount);

                id = (int)cmd.ExecuteScalar();
            }
            string sql = @"INSERT INTO [Rule_RuleSet] (RuleSetId, RuleId)
                                  VALUES(@RuleSetId, @RuleId)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@RuleSetId", ruleSetId);
                cmd.Parameters.AddWithValue("@RuleId", id);
                cmd.ExecuteScalar();
            }

        }
        public List<Rule> GetRulesByRuleSet(int ruleSetId)
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
            List<Rule> rules = new List<Rule>();
            foreach (int id in ruleIds)
            {
                rules.Add(GetRuleById(id));
            }
            return rules;
        }
        public Rule GetRuleById(int ruleId)
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
                    Type = (ruleType)reader["RuleType"]
                };
                return rule;
            }
        }
    }
}
