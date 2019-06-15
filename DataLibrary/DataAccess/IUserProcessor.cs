using DataLibrary.Dbo;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.DataAccess
{
    public interface IUserProcessor
    {
        int AddUser(IUser user);
        List<IUser> GetUsers();
        IUser GetUserByEmail(string email);
        IUser GetUserById(int id);
    }
}
