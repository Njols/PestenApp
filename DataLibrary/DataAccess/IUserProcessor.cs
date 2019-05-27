using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.DataAccess
{
    public interface IUserProcessor
    {
        int AddUser(User user);
        List<User> GetUsers();
        User GetUserByEmail(string email);
        User GetUserById(int id);
    }
}
