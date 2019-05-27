using DataLibrary.DataAccess;
using DataLibrary.Models;
using System;

namespace Logic
{
    public class UserLogic
    {
        private IUserProcessor _userProcessor;
        private IRuleSetProcessor _ruleSetProcessor;

        public UserLogic(IUserProcessor userProcessor, IRuleSetProcessor ruleSetProcessor)
        {
            _userProcessor = userProcessor;
            _ruleSetProcessor = ruleSetProcessor;
        }

        public User GetUserByEmail (string email)
        {
            return _userProcessor.GetUserByEmail(email); 
        }

        public bool TryToCreateUser (string email, string username, string password)
        {
            if (GetUserByEmail(email) == null)
            {
                PasswordHasher passwordHasher = new PasswordHasher(password);

                User user = new User
                {
                    Email = email,
                    Username = username,
                    PasswordHash = passwordHasher.ToArray()
                };

                _userProcessor.AddUser(user);
                return true;
            }
            return false;
        }
    }
}
