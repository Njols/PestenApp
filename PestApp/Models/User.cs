using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PestApp.Models
{
    public class User
    {
        private string _email;
        public string Email { get { return _email; } }
        private string _username;
        public string Username { get { return _username; } }
        private string _password;
        public string Password { get { return _password; } }
        public User (string email, string username, string password)
        {
            _email = email;
            _username = username;
            _password = password;
        }
    }
}
