using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using Dapper;

namespace DataLibrary.DataAccess
{
    public static class SqlDataAcces
    {
        public static string ConnectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=PestDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        public static List<T> LoadData<T>(string query)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                return cnn.Query<T>(query).AsList();
            }
        }
        public static int SaveData<T> (string query, T data)
        {
            using (IDbConnection cnn = new SqlConnection(ConnectionString))
            {
                return cnn.Execute(query, data);
            }
        }
    }
}
