using Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.Models
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Username { get; set; }

    }
}
