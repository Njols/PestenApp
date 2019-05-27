using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IUser
    {
        string Username { get; set; }
        string Email { get; set; }
        byte[] PasswordHash { get; set; }
    }
}
