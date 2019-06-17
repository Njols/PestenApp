using DataLibrary.DataAccess;
using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTests.Mocks
{
    class MockUserProcessor : IUserProcessor
    {
        List<IUser> users = new List<IUser>();
        public int AddUser(IUser user)
        {
            users.Add(user);
            return 1;
        }

        public IUser GetUserByEmail(string email)
        {
            return users.Where(_ => _.Email == email).FirstOrDefault();
        }

        public IUser GetUserById(int id)
        {
            return users.Where(_ => _.Id == id).FirstOrDefault();
        }

        public List<IUser> GetUsers()
        {
            return users;
        }
    }
}
