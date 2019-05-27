using System;
using System.Collections.Generic;
using System.Text;

namespace DataLibrary.DataAccess
{
    public interface ISqlDataAccess
    {
        List<T> LoadList<T>(string sql);
        T LoadSingle<T>(string sql);
        int SaveData<T>(string sql, T data);

    }
}
