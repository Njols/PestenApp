using DataLibrary.Models;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.DataAccess
{
    public interface IUserProcessor
    {
        int AddUser(IUser user);
        List<User> GetUsers();
        User GetUserByEmail(string email);
        User GetUserById(int id);
    }
}
