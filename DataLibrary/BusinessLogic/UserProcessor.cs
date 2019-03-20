using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.BusinessLogic
{
    public static class UserProcessor
    {
        public static int CreateUser (string Email, string Username, string Password)
        {
            User user = new User
            {
                Email = Email,
                Username = Username,
                Password = Password
            };
            string sql = @"INSERT INTO dbo.User (Email, Username, Password) VALUES @Email, @Username, @Password";
            return SqlDataAcces.SaveData(sql, user);
        }
        public static List<User> GetUsers ()
        {
            string sql = @"SELECT Id, Username, Email, Password FROM dbo.User";
            return SqlDataAcces.LoadData<User>(sql);
        }
    }
}
