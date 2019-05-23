using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;

namespace DataLibrary.DataAccess
{
    public class UserProcessor
    {
        private SqlDataAccess _sqlDataAccess;
        public int CreateUser (string Email, string Username, string Password)
        {
            User user = new User
            {
                Email = Email,
                Username = Username,
                Password = Password
            };
            string sql = @"INSERT INTO [User] (Email, Username, Password) 
                           VALUES (@Email, @Username, @Password)";
            return _sqlDataAccess.SaveData(sql, user);
        }
        public List<User> GetUsers ()
        {
            string sql = @"SELECT Id, Username, Email, Password FROM [User]";
            return _sqlDataAccess.LoadData<User>(sql);
        }
        public User GetUserByEmail (string email)
        {
            string sql = @"SELECT Id, Username, Email, Password FROM [User] WHERE Email = @email";
            return _sqlDataAccess.LoadData<User>(sql)[0];
        }
        
        public User GetUserById (int id)
        {
            string sql = @"SELECT Id, Username, Email, Password FROM [User] WHERE Id = @id";
            return _sqlDataAccess.LoadData<User>(sql)[0];
        }
    }
}
