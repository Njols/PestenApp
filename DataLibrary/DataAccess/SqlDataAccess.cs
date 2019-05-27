using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace DataLibrary.DataAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private string _connectionString;
        public SqlDataAccess(string connectionstring)
        {
            _connectionString = connectionstring;
        }
        public List<T> LoadList<T>(string query)
        {
            using (IDbConnection cnn = new SqlConnection(_connectionString))
            {
                return cnn.Query<T>(query).AsList();
            }
        }

        public T LoadSingle<T> (string query)
        {
            using (IDbConnection cnn = new SqlConnection(_connectionString))
            {
                var result = cnn.QueryMultiple(query);
                T finalResult = result.Read<T>().FirstOrDefault();
                if (finalResult == null)
                {
                    return default(T);
                }
                return finalResult;
            }
        }
        public int SaveData<T> (string query, T data)
        {
            using (IDbConnection cnn = new SqlConnection(_connectionString))
            {
                return cnn.Execute(query, data);
            }
        }
    }
}
