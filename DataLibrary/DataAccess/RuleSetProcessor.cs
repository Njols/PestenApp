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
        public RuleSetProcessor (string connectionString)
        {
            _connectionString = connectionString;
        }
        public int AddRuleSet (RuleSet ruleSet)
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

        public List<RuleSet> GetRuleSets()
        {
            string sql = @"SELECT * FROM [RuleSet]";
            List<RuleSet> ruleSets = new List<RuleSet>();
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

    }
}
