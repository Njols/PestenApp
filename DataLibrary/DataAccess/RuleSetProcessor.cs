using System;
using System.Collections.Generic;
using System.Text;
using Enums;
using DataLibrary.Dbo;
using System.Data.SqlClient;
using Interfaces;

namespace DataLibrary.DataAccess
{
    public  class RuleSetProcessor : IRuleSetProcessor
    {

        string _connectionString;
        public RuleSetProcessor (string connectionString)
        {
            _connectionString = connectionString;
        }
        public int AddRuleSet (IRuleSet ruleSet)
        {
            string sql = @"INSERT INTO [RuleSet] (UserId, Name)
                             VALUES (@UserId, @Name)
                             SELECT SCOPE_IDENTITY()";
            int ruleSetId;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            using (SqlCommand cmd = new SqlCommand(sql, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@UserId", ruleSet.UserId);
                cmd.Parameters.AddWithValue("@Name", ruleSet.Name);
                ruleSetId = (int)cmd.ExecuteScalar();
            }
            return ruleSetId;
        }

        public List<IRuleSet> GetRuleSets()
        {
            string sql = @"SELECT * FROM [RuleSet]";
            List<IRuleSet> ruleSets = new List<IRuleSet>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        RuleSet ruleSet = new RuleSet
                        {
                            Id = (int)reader["Id"],
                            UserId = (int)reader["UserId"],
                            Name = (string)reader["Name"]
                        };
                    }
                }
            }
            return ruleSets;
        }

        public IRuleSet GetRuleSetById(int ruleSetId)
        {
            string sql = @"SELECT * FROM [RuleSet] WHERE Id = @RuleSetId";
            RuleSet ruleSet;
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        ruleSet = new RuleSet
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            UserId = (int)reader["UserId"]
                        };
                        return ruleSet;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }
        public List<IRuleSet> GetRuleSetsByAmountOfRules()
        {
            string sql = @"SELECT * FROM [RuleSet] 
                           FULL OUTER JOIN [Rule_RuleSet] ON [Rule_RuleSet].RuleSetId = [RuleSet].Id 
                           FULL OUTER JOIN [Rule] ON [Rule].Id = [Rule_RuleSet].RuleId 
                           ORDER BY COUNT(Rule)";
            List<IRuleSet> ruleSets = new List<IRuleSet>();
            using(SqlConnection conn = new SqlConnection(_connectionString))
            {
                using(SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while(reader.Read())
                    {
                        RuleSet ruleSet = new RuleSet
                        {
                            Id = (int)reader["Id"],
                            UserId = (int)reader["UserId"],
                            Name = (string)reader["Name"]
                        };
                        ruleSets.Add(ruleSet);
                    }
                }
            }
            return ruleSets;
        }

    }
}
