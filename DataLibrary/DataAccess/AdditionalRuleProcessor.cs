using Enums;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace DataLibrary.DataAccess
{
    public class AdditionalRuleProcessor : IAdditionalRuleProcessor
    {
        private string _connectionString;
        public AdditionalRuleProcessor (string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddAdditionalRule (int additionalRuleId, int ruleSetId)
        {
            string query = @"INSERT INTO [AdditionalRule_RuleSet] (RuleSetId,AdditionalRuleId)
                                 VALUES (@RuleSetId, @AdditionalRuleId)";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@RuleSetId", ruleSetId);
                cmd.Parameters.AddWithValue("@RuleType", additionalRuleId);
                cmd.ExecuteScalar();
            }
        }
        public List<additionalRule> GetAdditionalRulesByRuleSet (int ruleSetId)
        {
            string query = @"SELECT AdditionalRuleId
                             FROM [AdditionalRule_RuleSet]
                             WHERE RuleSetId = @RuleSetId";
            List<additionalRule> additionalRules = new List<additionalRule>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
                using(SqlCommand cmd = new SqlCommand(query, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@RuleSetId", ruleSetId);
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    additionalRule additionalRule = (additionalRule)reader["AdditionalRuleId"];
                    additionalRules.Add(additionalRule);
                }
            }
            return additionalRules;
        }
    }
}
