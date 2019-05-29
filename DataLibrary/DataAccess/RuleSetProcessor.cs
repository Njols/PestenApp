using System;
using System.Collections.Generic;
using System.Text;
using Enums;
using DataLibrary.Models;
using System.Data.SqlClient;

namespace DataLibrary.DataAccess
{
    public  class RuleSetProcessor : IRuleSetProcessor
    {

        string _connectionString;
        IRuleProcessor _ruleProcessor;
        IAdditionalRuleProcessor _additionalRuleProcessor;
        public RuleSetProcessor (string connectionString)
        {
            _connectionString = connectionString;
            /*_ruleProcessor = ruleProcessor;
            _additionalRuleProcessor = additionalRuleProcessor;*/
        }
        public int AddRuleSet (RuleSet ruleSet, int userId)
        {
            string sql = @"INSERT INTO [RuleSet] (UserId, Name)
                             VALUES (@UserId, @Name)
                             SELECT SCOPE_IDENTITY()";
            int ruleSetId;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@UserId", userId);
                cmd.Parameters.AddWithValue("@Name", ruleSet.Name);
                ruleSetId = (int)cmd.ExecuteScalar();
            }
            return ruleSetId;
        }

        public List<RuleSet> GetRuleSets()
        {
            string sql = @"SELECT * FROM [RuleSet]";
            List<RuleSet> ruleSet = new List<RuleSet>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using(SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    
                }
            }
            return ruleSet;
        }

    }
}
