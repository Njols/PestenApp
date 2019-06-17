using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IUser
    {
        int Id { get; set; }
        string Username { get; set; }
        string Email { get; set; }
        byte[] PasswordHash { get; set; }
    }
}
