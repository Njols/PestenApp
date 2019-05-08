using System;
using System.Collections.Generic;
using System.Text;
using DataLibrary.Models;

namespace DataLibrary.DataAccess
{
    public  class RuleSetProcessor
    {
        public int CreateRuleSet (List<Rule> rules, User user)
        {
            RuleSet ruleSet = new RuleSet(user, rules);

            foreach(Rule rule in rules)
            {
                string query = @"INSERT INTO [Rule] (";
            }

            string sql = @"INSERT INTO [User] (Email, Username, Password) 
                           VALUES (@Email, @Username, @Password)";
            return 0;
        }

    }
}
