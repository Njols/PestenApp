using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;

namespace Logic
{
    public class UserLogic
    {
        public User GetUserByEmail (string email)
        {
            UserProcessor _userProcessor = new UserProcessor();
            return _userProcessor.GetUserByEmail(email); 
        }

        public bool TryToCreateUser (string email, string username, string password)
        {
            if (GetUserByEmail(email) == null)
            {
                UserProcessor _userProcessor = new UserProcessor();
                _userProcessor.CreateUser(email, username, password);
                return true;
            }
            return false;
        }
    }
}
