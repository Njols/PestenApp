using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;

namespace DataLibrary.DataAccess
{
    public class UserProcessor : IUserProcessor
    {
        private ISqlDataAccess _sqlDataAccess;
        public UserProcessor (ISqlDataAccess sqlDataAccess)
        {
            _sqlDataAccess = sqlDataAccess;
        }
        public int AddUser (User user)
        {
            string sql = @"INSERT INTO [User] (Email, Username, Password) 
                           VALUES (@Email, @Username, @PasswordHash)";
            return _sqlDataAccess.SaveData(sql, user);
        }
        public List<User> GetUsers ()
        {
            string sql = @"SELECT Id, Username, Email, Password FROM [User]";
            return _sqlDataAccess.LoadList<User>(sql);
        }
        public User GetUserByEmail (string email)
        {
            string sql = @"SELECT Id, Username, Email, Password FROM [User] WHERE Email = '" + email + "'";
            if (_sqlDataAccess.LoadSingle<User>(sql) == null)
            {
                return null;
            }
            return _sqlDataAccess.LoadSingle<User>(sql);
        }
        
        public User GetUserById (int id)
        {
            string sql = @"SELECT Id, Username, Email, Password FROM [User] WHERE Id = @id";
            return _sqlDataAccess.LoadList<User>(sql)[0];
        }
    }
}
