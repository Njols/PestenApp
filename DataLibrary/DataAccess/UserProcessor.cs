using DataLibrary.DataAccess;
using DataLibrary.Dbo;
using System;
using System.Collections.Generic;
using System.Text;
using Dapper;
using Interfaces;
using System.Data.SqlClient;
using System.Data;

namespace DataLibrary.DataAccess
{
    public class UserProcessor : IUserProcessor
    {
        private string _connectionString;
        public UserProcessor (string connectionString)
        {
            _connectionString = connectionString;
        }
        public int AddUser (IUser user)
        {
            string sql = @"INSERT INTO [User] (Email, Username, Password) 
                           VALUES (@Email, @Username, @PasswordHash)";
            int id = 0;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Email", user.Email);
                    cmd.Parameters.AddWithValue("@Username", user.Username);
                    SqlParameter parameter = cmd.Parameters.Add("@PasswordHash", SqlDbType.VarBinary);
                    parameter.Value = user.PasswordHash;


                    cmd.ExecuteScalar();
                }
            }
            return id;
        }
        public List<IUser> GetUsers ()
        {
            string sql = @"SELECT Id, Username, Email, Password FROM [User]";
            List<IUser> users = new List<IUser>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        User user = new User
                        {
                            Id = (int)reader["Id"],
                            Username = (string)reader["Username"],
                            Email = (string)reader["Email"],
                            PasswordHash = (byte[])reader["Password"]
                        };
                        users.Add(user);
                    }
                }
            }
            return users;
        }
        public IUser GetUserByEmail (string email)
        {
            string sql = @"SELECT Id, Username, Email, Password FROM dbo.[User] WHERE Email = @Email";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Email", email);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (!reader.Read())
                    {
                        return null;
                    }
                    int id = (int)reader["Id"];
                    string username = (string)reader["Username"];
                    string _email = (string)reader["Email"];
                    byte[] passwordHash = (byte[])reader["Password"];
                    User user = new User
                    {
                        Id = id,
                        Username = username,
                        Email = _email,
                        PasswordHash = passwordHash
                    };
                    return user;
                }
            }
        }
        
        public IUser GetUserById (int id)
        {
            string sql = @"SELECT Id, Username, Email, Password FROM [User] WHERE Id = @Id";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@Id", id);
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        int _id = (int)reader["Id"];
                        string username = (string)reader["Username"];
                        string email = (string)reader["Email"];
                        byte[] passwordHash = (byte[])reader["Password"];
                        User user = new User
                        {
                            Id = id,
                            Username = username,
                            Email = email,
                            PasswordHash = passwordHash
                        };
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

    }
}
