using DataLibrary.DataAccess;
using DataLibrary.Dbo;
using Interfaces;
using System;
using System.Collections.Generic;

namespace Logic
{
    public class UserLogic
    {
        private IUserProcessor _userProcessor;

        public UserLogic(IUserProcessor userProcessor)
        {
            _userProcessor = userProcessor;
        }

        public IUser GetUserByEmail (string email)
        {
            return _userProcessor.GetUserByEmail(email); 
        }

        public IUser GetUserById(int id)
        {
            return _userProcessor.GetUserById(id);
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
        public bool PasswordMatches(string email, string password)
        {
            User user = (User)GetUserByEmail(email);
            if (user != null)
            {
                PasswordHasher hasher = new PasswordHasher(user.PasswordHash);
                return hasher.Verify(password);
            }
            else
            {
                return false;
            }
        }
        public List<IUser> GetUsers ()
        {
            return _userProcessor.GetUsers();
        }
    }
}
